using EduWork.Common.DTO;

namespace EduWork.Domain.Contracts
{
    public interface IWorkTimeService
    {
        Task<List<WorkTimePartDTO>> GetWorkTimePartsForUserAsync(GetWorkTimePartsDTO getWorkTimeParts);
        Task SetWorkTimeRecordAsync(SetWorkDayTimeDTO workDayTimeDTO);
        Task PutWorkTimeRecordAsync(WorkTimePartDTO workTimePart);
        Task DeleteWorkTimeRecordAsync(int id);
        
    }
}
