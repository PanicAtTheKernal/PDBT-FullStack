using PDBT_CompleteStack.Models;

namespace PDBT_CompleteStack.Services.IssueService;

public interface IIssueService
{ 
    Task<ServiceResponse<IEnumerable<Issue>>> GetAllIssue(int projectId);
    Task<ServiceResponse<Issue>> GetIssueById(int id, int projectId);
    Task<ServiceResponse<Issue>> ConvertDto(int id, IssueDTO issueDto,int projectId);
    Task<ServiceResponse<Issue>> UpdateIssue(Issue issue);
    Task<ServiceResponse<Issue>> AddIssue(Issue issue);
    Task<ServiceResponse<Issue>> DeleteIssue(int id, int projectId);
    Task<ServiceResponse<Issue>> SaveChanges(int id);
}