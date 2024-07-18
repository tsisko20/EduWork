using EduWork.Common.DTO;
using EduWork.Data;
using EduWork.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using EduWork.Data.Entities;
using System.Linq;
using AutoMapper;
using System.Xml.Linq;
using System.Collections.Generic;
using System;

namespace EduWork.Domain.Services
{
    public class WorkTimeService(AppDbContext context, IMapper mapper) : IWorkTimeService
    {
        public async Task<List<WorkTimePartDTO>> GetWorkTimePartsForUserAsync(RequestWorkTimePartsDTO getWorkTimeParts)
        {
            var query = context.WorkDayTimeRecords
                .Include(wdt => wdt.WorkDay)
                .Where(wdt => wdt.WorkDay.UserId == getWorkTimeParts.UserId);

            if (getWorkTimeParts.Day > 0 && getWorkTimeParts.Month > 0 && getWorkTimeParts.Year > 0)
            {
                var date = new DateOnly(getWorkTimeParts.Year ?? default(int), getWorkTimeParts.Month ?? default(int), getWorkTimeParts.Day ?? default(int));
                query = query.Where(wdt => wdt.WorkDay.WorkDate == date).OrderBy(wdt => wdt.StartTime);
            }
            else if (getWorkTimeParts.Day == null && getWorkTimeParts.Month > 0 && getWorkTimeParts.Year > 0)
            {
                query = query.Where(wdt => wdt.WorkDay.WorkDate.Year == getWorkTimeParts.Year && wdt.WorkDay.WorkDate.Month == getWorkTimeParts.Month).OrderBy(wdt => wdt.StartTime);
            }
            else if (getWorkTimeParts.Day == null && getWorkTimeParts.Month == null && getWorkTimeParts.Year > 0)
            {
                query = query.Where(wdt => wdt.WorkDay.WorkDate.Year == getWorkTimeParts.Year).OrderBy(wdt => wdt.StartTime);
            }
            var workDayTimeRecords = await query.AsNoTracking().ToListAsync();



            var workDayTimeParts = mapper.Map<List<WorkTimePartDTO>>(workDayTimeRecords);

            return workDayTimeParts;
        }

        public async Task SetWorkTimeRecordAsync(SetWorkDayTimeDTO setWorkDayTimeDTO)
        {
            if (setWorkDayTimeDTO.StartTime >= setWorkDayTimeDTO.EndTime)
            {
                throw new ArgumentException($"Početno vrijeme ne smije biti veće ili jednako završnom.");
            }

            if (await IsHolidayAsync(setWorkDayTimeDTO.WorkDate))
            {
                throw new ArgumentException($"Nije moguće unijeti radno vrijeme na praznik.");
            }

            DayOfWeek dayOfWeek = setWorkDayTimeDTO.WorkDate.DayOfWeek;
            if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
            {
                throw new ArgumentException("Nije moguće unijeti radno vrijeme za vikend.");
            }

            var workDay = await context.WorkDays
                .Include(wd => wd.WorkDayTimes)
                .FirstOrDefaultAsync(wd => wd.UserId == setWorkDayTimeDTO.UserId && wd.WorkDate == setWorkDayTimeDTO.WorkDate);

            if (workDay != null)
            {
                RequestWorkTimePartsDTO getWorkTimeParts = new RequestWorkTimePartsDTO
                {
                    UserId = setWorkDayTimeDTO.UserId,
                    Day = setWorkDayTimeDTO.WorkDate.Day,
                    Month = setWorkDayTimeDTO.WorkDate.Month,
                    Year = setWorkDayTimeDTO.WorkDate.Year,
                };
                List<WorkTimePartDTO> list = await GetWorkTimePartsForUserAsync(getWorkTimeParts);
                foreach (WorkTimePartDTO element in list)
                {
                    if ((setWorkDayTimeDTO.StartTime > element.StartTime && setWorkDayTimeDTO.StartTime < element.EndTime) ||
                       (setWorkDayTimeDTO.EndTime > element.StartTime && setWorkDayTimeDTO.EndTime < element.EndTime) ||
                       (setWorkDayTimeDTO.StartTime < element.StartTime && setWorkDayTimeDTO.EndTime > element.EndTime) ||
                       (setWorkDayTimeDTO.StartTime == element.StartTime || setWorkDayTimeDTO.EndTime == element.EndTime))
                    {
                        throw new ArgumentException($"Uneseno vrijeme se preklapa s već postojećim odsječkom: {element.StartTime}-{element.EndTime}.");
                    }
                }

            }
            else if (workDay == null)
            {
                workDay = mapper.Map<WorkDay>(setWorkDayTimeDTO);
                workDay.WorkDayTimes = new List<WorkDayTime>();
                await context.WorkDays.AddAsync(workDay);
            }

            var workDayTime = new WorkDayTime
            {
                WorkDay = workDay,
                StartTime = setWorkDayTimeDTO.StartTime,
                EndTime = setWorkDayTimeDTO.EndTime
            };
            workDay.WorkDayTimes?.Add(workDayTime);
            await context.SaveChangesAsync();
        }

        private async Task<bool> IsHolidayAsync(DateOnly date)
        {
            var holidays = await GetNonWorkingDays();
            return holidays.Any(holiday => holiday.NonWorkingDate == date);
        }

        public async Task DeleteWorkTimePartAsync(int id)
        {
            var workDayTimeRecords = await context.WorkDayTimeRecords
                .ToListAsync();

            var recordToDelete = workDayTimeRecords.First(r => r.Id == id);
            if (recordToDelete != null)
            {
                context.WorkDayTimeRecords.Remove(recordToDelete);
            }
            await context.SaveChangesAsync();
        }

        public async Task UpdateWorkTimePartAsync(UpdateWorkTimePartDTO update)
        {
            RequestWorkTimePartsDTO getWorkTimeParts = new RequestWorkTimePartsDTO
            {
                UserId = update.UserId,
                Day = update.WorkDate.Day,
                Month = update.WorkDate.Month,
                Year = update.WorkDate.Year,
            };
            List<WorkTimePartDTO> list = await GetWorkTimePartsForUserAsync(getWorkTimeParts);
            foreach (WorkTimePartDTO element in list)
            {
                if ((element.Id != update.Id) && (
                   (update.StartTime > element.StartTime && update.StartTime < element.EndTime) ||
                   (update.EndTime > element.StartTime && update.EndTime < element.EndTime) ||
                   (update.StartTime < element.StartTime && update.EndTime > element.EndTime) ||
                   (update.StartTime == element.StartTime || update.EndTime == element.EndTime)))
                {
                    throw new ArgumentException($"Uneseno vrijeme se preklapa s već postojećim odsječkom: {element.StartTime}-{element.EndTime}.");
                }
                
            }
            var workTimePart = await context.WorkDayTimeRecords
                    .FirstAsync(wtp => wtp.Id == update.Id);

            if (workTimePart == null)
            {
                throw new ArgumentException("WorkTimePart not found.");
            }

            workTimePart.StartTime = update.StartTime;
            workTimePart.EndTime = update.EndTime;

            context.WorkDayTimeRecords.Update(workTimePart);
            await context.SaveChangesAsync();

        }

        public async Task<List<MonthlyWorkHoursDTO>> GetWorkTimePartsMonthlyAsync(RequestWorkTimePartsDTO request)
        {
            if(request.Day != null || request.Month != null)
            {
                throw new ArgumentException("You should provide only year parameter");
            }
            var response = await GetWorkTimePartsForUserAsync(request);

            // Group work time parts by month and sum the hours
            var monthlyWorkHours = response
                .GroupBy(part => part.WorkDate.Month)
                .Select(g => new MonthlyWorkHoursDTO
                {
                    Month = g.Key,
                    TotalHours = Math.Round(g.Sum(part => (part.EndTime.ToTimeSpan() - part.StartTime.ToTimeSpan()).TotalHours), 3)
                })
                .ToList();

            return monthlyWorkHours;
        }

        public async Task<List<WeeklyWorkHoursDTO>> GetWorkTimePartsWeeklyAsync(RequestWorkTimePartsDTO request)
        {
            if (request.Day != null)
            {
                throw new ArgumentException("You should provide only month and year parameters");
            }
            var response = await GetWorkTimePartsForUserAsync(request);

            var weeklyWorkHours = response
            .GroupBy(part => GetWeekOfMonth(part.WorkDate))
            .Select(g => new WeeklyWorkHoursDTO
            {
                WeekNumber = g.Key,
                TotalHours = Math.Round(g.Sum(part => (part.EndTime.ToTimeSpan() - part.StartTime.ToTimeSpan()).TotalHours), 3)
            })
            .ToList();

                return weeklyWorkHours;
            }

        private int GetWeekOfMonth(DateOnly date)
        {
            var startOfMonth = new DateOnly(date.Year, date.Month, 1);
            var startOfWeek = startOfMonth.DayOfWeek == DayOfWeek.Sunday ? DayOfWeek.Monday : startOfMonth.DayOfWeek;

            int daysOffset = (7 + (startOfWeek - DayOfWeek.Monday)) % 7;

            var firstMonday = startOfMonth.AddDays(daysOffset);
            var daysSinceFirstMonday = (date.DayNumber - firstMonday.DayNumber);

            var weekNumber = (daysSinceFirstMonday / 7) + 1;
            return weekNumber;
        }

        public async Task<List<NonWorkingDay>> GetNonWorkingDays()
        {
            var response = await context.NonWorkingDays.ToListAsync();
            return response;
        }
    }

}
