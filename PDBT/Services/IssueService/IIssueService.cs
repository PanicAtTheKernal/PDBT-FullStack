using PDBT_CompleteStack.Models;

namespace PDBT_CompleteStack.Services.IssueService;

public interface IIssueService
{ 
    Task<ServiceResponse<IEnumerable<Issue>>> GetAllIssue(int projectId);
    Task<Issue?> GetIssueById(int id, int projectId);
    Task<ServiceResponse<Issue>> ConvertDto(int id, IssueDTO issueDto,int projectId);
    Task<ServiceResponse<Issue>> UpdateIssue(IssueDTO issue);
    Task<Issue> AddIssue(IssueDTO issue);
    Task<bool> DeleteIssue(int id, int projectId);
    Task<Issue> AddLabelsToIssue(Issue issue, ICollection<Label> labels);
    Task<Issue> AddUsersToIssue(Issue issue, ICollection<User> users);
    Task<IEnumerable<Issue>> GetIssuesByList(ICollection<int> ids, int projectId);
    Task<Issue> AddIssuesToIssue(Issue issue, ICollection<Issue> linkedIssues);
    Task<ServiceResponse<Issue>> SaveChanges(int id);
}