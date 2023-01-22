using PDBT_CompleteStack.Models;

namespace PDBT_CompleteStack.Services.LabelService;

public interface ILabelService
{
    Task<ServiceResponse<IEnumerable<Label>>> GetAllLabel(int projectId);
    Task<Label?> GetLabelById(int id, int projectId);
    Task<IEnumerable<Label>> GetLabelsByList(ICollection<int> ids, int projectId);
    Task<ServiceResponse<Label>> UpdateLabel(int id, LabelDTO labelDto,int projectId);
}