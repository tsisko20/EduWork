using EduWork.Common.DTO;

namespace EduWork.Domain.Contracts
{
    public interface IWorkTimeService
    {
        Task<List<WorkTimePartDTO>> GetWorkTimePartsForUserAsync(int userId, int? day, int? month, int? year);
        Task SetWorkTimeRecordAsync(SetWorkDayTimeDTO workDayTimeDTO);
        Task PutWorkTimeRecordAsync(WorkTimePartDTO workTimePart);
        Task DeleteWorkTimeRecordAsync(int id);
        
    }
}
