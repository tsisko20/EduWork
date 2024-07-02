using EduWork.Common.DTO;

namespace EduWork.Domain.Contracts
{
    public interface IWorkTimeService
    {
        Task<List<WorkTimePart>> GetWorkTimePartsForUserAsync(int userId, int? month, int? year);
        Task SetWorkTimeRecordAsync(int userId, TimeOnly startTime, TimeOnly endTime, DateOnly day);
        Task PutWorkTimeRecordAsync(WorkTimePart workTimePart);
        Task DeleteWorkTimeRecordAsync(int id);
        
    }
}
