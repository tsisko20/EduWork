using EduWork.Common.DTO;
using EduWork.Data;
using EduWork.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using EduWork.Data.Entities;
using System.Linq;
using AutoMapper;

namespace EduWork.Domain.Services
{
    public class WorkTimeService(AppDbContext context, IMapper mapper) : IWorkTimeService
    {   
        public async Task<List<WorkTimePartDTO>> GetWorkTimePartsForUserAsync(int userId, int? day = null, int? month = null, int? year = null)
        {
            var query = context.WorkDayTimeRecords
                .Include(wdt => wdt.WorkDay)
                .Where(wdt => wdt.WorkDay.UserId == userId);

            if (day != 0 && month != 0 && year != 0)
            {
                var date = new DateOnly(year ?? default(int), month ?? default(int), day ?? default(int));
                query = query.Where(wdt => wdt.WorkDay.WorkDate == date);
            }
            else if (day == 0 && month != 0 && year != 0)
            {
                query = query.Where(wdt => wdt.WorkDay.WorkDate.Year == year && wdt.WorkDay.WorkDate.Month == month);
            }
            var workDayTimeRecords = await query.AsNoTracking().ToListAsync();

            if(workDayTimeRecords.Count == 0)
            {
                throw new ArgumentException("There is no WorkTimeRecord with provided userId and time.");
            }

            var workDayTimeParts = mapper.Map<List<WorkTimePartDTO>>(workDayTimeRecords);

            return workDayTimeParts;
        }

        public async Task SetWorkTimeRecordAsync(SetWorkDayTimeDTO workDayTimeDTO)
        {
            var workDay = await context.WorkDays
                .Include(wd => wd.WorkDayTimes)
                .FirstOrDefaultAsync(wd => wd.UserId == workDayTimeDTO.UserId && wd.WorkDate == workDayTimeDTO.WorkDate);

            if (workDay == null) {
                workDay = mapper.Map<WorkDay>(workDayTimeDTO);
                workDay.WorkDayTimes = new List<WorkDayTime>();
                await context.WorkDays.AddAsync(workDay);
            }
            
            var workDayTime = new WorkDayTime
            {
                WorkDay = workDay,
                StartTime = workDayTimeDTO.StartTime,
                EndTime = workDayTimeDTO.EndTime
            };
            workDay.WorkDayTimes?.Add(workDayTime);
            await context.SaveChangesAsync();
        }

        public async Task PutWorkTimeRecordAsync(WorkTimePartDTO workTimePart)
        {
            var workDayTimeRecord = await context.WorkDayTimeRecords
                .Include(wd => wd.WorkDay)
                .FirstAsync();

            if (workDayTimeRecord == null)
            {
                throw new ArgumentException($"WorkDayTimeRecord with ID {workTimePart.Id} not found.");
            }

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
                throw new ArgumentException("WorkDayTimeRecord with provided id doesn't exist!");
            }
        }
    }
}
