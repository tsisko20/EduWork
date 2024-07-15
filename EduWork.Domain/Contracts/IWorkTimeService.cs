using EduWork.Common.DTO;

namespace EduWork.Domain.Contracts
{
    public interface IWorkTimeService
    {
        Task<List<WorkTimePartDTO>> GetWorkTimePartsForUserAsync(RequestWorkTimePartsDTO getWorkTimeParts);
        Task SetWorkTimeRecordAsync(SetWorkDayTimeDTO workDayTimeDTO);
        Task PutWorkTimePartsAsync(List<UpdateWorkTimePartsDTO> updateWorkTimeParts);
        Task DeleteWorkTimePartsAsync(List<int> deleteWorkTimeParts);
        
    }
}
