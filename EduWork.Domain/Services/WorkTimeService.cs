using EduWork.Common.DTO;
using EduWork.Data;
using EduWork.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using EduWork.Data.Entities;

namespace EduWork.Domain.Services
{
    public class WorkTimeService(AppDbContext context) : IWorkTimeService
    {

        public async Task<List<WorkTimePart>> GetWorkTimePartsForUserAsync(int userId, int? month = null, int? year = null)
        {
            var query = context.WorkDayTimeRecords
                .Include(wdt => wdt.WorkDay)
                .Where(wdt => wdt.WorkDay.UserId == userId);

            if (month.HasValue && year.HasValue)
            {
                query = query.Where(wdt => wdt.WorkDay.WorkDate.Year == year && wdt.WorkDay.WorkDate.Month == month);
            }

            var workDayTimeRecords = await query.ToListAsync();

            var workDayTimeParts = workDayTimeRecords.Select(wdt => new WorkTimePart
            {
                Id = wdt.Id,
                WorkDate = wdt.WorkDay.WorkDate,
                StartTime = wdt.StartTime,
                EndTime = wdt.EndTime
            }).ToList();

            return workDayTimeParts;
        }

        public async Task<List<WorkTimePart>> GetWorkTimePartsForUsersAsync()
        {
            var workDayTimeRecords = await context.WorkDayTimeRecords
                .Include(wdt => wdt.WorkDay)
                .ToListAsync();

            var workDayTimeParts = workDayTimeRecords.Select(wdt => new WorkTimePart
            {
                Id = wdt.Id,
                WorkDate = wdt.WorkDay.WorkDate,
                StartTime = wdt.StartTime,
                EndTime = wdt.EndTime
            }).ToList();

            return workDayTimeParts;
        }



        public async Task SetWorkTimeRecordAsync(int userId, TimeOnly startTime, TimeOnly endTime, DateOnly day)
        {
            var workDay = await context.WorkDays
                .Include(wd => wd.WorkDayTimes)
                .FirstOrDefaultAsync(wd => wd.UserId == userId && wd.WorkDate == day);
            if (workDay == null) {
                workDay = new WorkDay
                {
                    UserId = userId,
                    WorkDate = day,
                    WorkDayTimes = new List<WorkDayTime>()
                };
                await context.WorkDays.AddAsync(workDay);
            }
            var workDayTime = new WorkDayTime
            {
                WorkDay = workDay,
                StartTime = startTime,
                EndTime = endTime
            };
            workDay.WorkDayTimes?.Add(workDayTime);
            await context.SaveChangesAsync();
        }
        public async Task PutWorkTimeRecordAsync(WorkTimePart workTimePart)
        {
            var workDayTimeRecord = await context.WorkDayTimeRecords
                .Include(wd => wd.WorkDay)
                .FirstAsync();

            if (workDayTimeRecord == null)
            {
                throw new ArgumentException($"WorkDayTimeRecord with ID {workTimePart.Id} not found.");
            }

            var workDay = await context.WorkDays
                .FirstOrDefaultAsync(wd => wd.UserId == workDayTimeRecord.WorkDay.UserId &&                  
                wd.WorkDate == workTimePart.WorkDate);

            if (workDay == null)
            {
                throw new ArgumentException($"WorkDay not found for UserId {workDayTimeRecord.WorkDay.UserId} and WorkDate {workTimePart.WorkDate}.");
            }

            //existingWorkDayTimeRecord.WorkDayId = workDay.Id;
            workDayTimeRecord.StartTime = workTimePart.StartTime;
            workDayTimeRecord.EndTime = workTimePart.EndTime;
            await context.SaveChangesAsync();
        }
        public async Task DeleteWorkTimeRecordAsync(int id)
        {
            var workDayTimeRecord = await context.WorkDayTimeRecords.FindAsync(id);
            if (workDayTimeRecord != null)
            {
                context.WorkDayTimeRecords.Remove(workDayTimeRecord);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Work");
            }
        }
    }
}
