using EduWork.Common.DTO;
using EduWork.Data;
using EduWork.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using EduWork.Data.Entities;
using System.Linq;
using AutoMapper;
using System.Xml.Linq;
using System.Collections.Generic;

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
                query = query.Where(wdt => wdt.WorkDay.WorkDate == date);
            }
            else if (getWorkTimeParts.Day == null && getWorkTimeParts.Month > 0 && getWorkTimeParts.Year > 0)
            {
                query = query.Where(wdt => wdt.WorkDay.WorkDate.Year == getWorkTimeParts.Year && wdt.WorkDay.WorkDate.Month == getWorkTimeParts.Month);
            }
            var workDayTimeRecords = await query.AsNoTracking().ToListAsync();

            

            var workDayTimeParts = mapper.Map<List<WorkTimePartDTO>>(workDayTimeRecords);

            return workDayTimeParts;
        }

        public async Task SetWorkTimeRecordAsync(SetWorkDayTimeDTO setWorkDayTimeDTO)
        {
            var workDay = await context.WorkDays
                .Include(wd => wd.WorkDayTimes)
                .FirstOrDefaultAsync(wd => wd.UserId == setWorkDayTimeDTO.UserId && wd.WorkDate == setWorkDayTimeDTO.WorkDate);

            if (setWorkDayTimeDTO.StartTime >= setWorkDayTimeDTO.EndTime)
            {
                throw new ArgumentException($"Početno vrijeme ne smije biti veće ili jednako završnom.");
            }

            if (workDay != null) {
                RequestWorkTimePartsDTO getWorkTimeParts = new RequestWorkTimePartsDTO { 
                UserId = setWorkDayTimeDTO.UserId,
                Day = setWorkDayTimeDTO.WorkDate.Day,
                Month = setWorkDayTimeDTO.WorkDate.Month,
                Year = setWorkDayTimeDTO.WorkDate.Year,
                };
                List<WorkTimePartDTO> list = await GetWorkTimePartsForUserAsync(getWorkTimeParts);
                foreach(WorkTimePartDTO element in list)
                {
                    if((setWorkDayTimeDTO.StartTime>element.StartTime && setWorkDayTimeDTO.StartTime < element.EndTime) ||
                       (setWorkDayTimeDTO.EndTime > element.StartTime && setWorkDayTimeDTO.EndTime < element.EndTime) ||
                       (setWorkDayTimeDTO.StartTime < element.StartTime && setWorkDayTimeDTO.EndTime > element.EndTime) ||
                       (setWorkDayTimeDTO.StartTime == element.StartTime || setWorkDayTimeDTO.EndTime == element.EndTime))
                    {
                        throw new ArgumentException($"Uneseno vrijeme se preklapa s već postojećim odsječkom: {element.StartTime}-{element.EndTime}.");
                    }
                }
                
            }
            else if (workDay == null) {
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

        public async Task DeleteWorkTimePartsAsync(List<int> deleteWorkTimeParts)
        {
            var workDayTimeRecords = await context.WorkDayTimeRecords
                .ToListAsync();

            foreach (var workTimePart in deleteWorkTimeParts)
            {
                    // Delete the record if Delete is set to true
                    var recordToDelete = workDayTimeRecords.First(r => r.Id == workTimePart);
                    if (recordToDelete != null)
                    {
                        context.WorkDayTimeRecords.Remove(recordToDelete);
                    }
                
            }
            await context.SaveChangesAsync();
        }

        public async Task PutWorkTimePartsAsync(List<UpdateWorkTimePartsDTO> updateWorkTimeParts)
        {
            var workDayTimeRecords = await context.WorkDayTimeRecords
                .ToListAsync();

            foreach (var workTimePart in updateWorkTimeParts)
            {
                if (!workTimePart.Delete)
                {
                    // Update the record if Delete is set to false
                    var recordToUpdate = workDayTimeRecords.First(r => r.Id == workTimePart.Id);
                    if (recordToUpdate != null)
                    {
                        recordToUpdate.StartTime = workTimePart.StartTime;
                        recordToUpdate.EndTime = workTimePart.EndTime;
                    }
                }

            }
            await context.SaveChangesAsync();
        }
        
    }

    
}
