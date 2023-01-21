using PDBT_CompleteStack.Models;

namespace PDBT_CompleteStack.Services.IssueService;

public interface IIssueService
{ 
    Task<ServiceResponse<IEnumerable<Issue>>> GetAllIssue(int projectId);
    Task<ServiceResponse<Issue>> GetIssueById(int id, int projectId);
    Task<ServiceResponse<Issue>> ConvertDto(int id, IssueDTO issueDto,int projectId);
    Task<ServiceResponse<Issue>> UpdateIssue(IssueDTO issue);
    Task<Issue> AddIssue(IssueDTO issue);
    Task<ServiceResponse<Issue>> DeleteIssue(int id, int projectId);
    Task<ServiceResponse<Issue>> SaveChanges(int id);
}