using PDBT_CompleteStack.Models;
using PDBT_CompleteStack.Services.IssueService;
using PDBT_CompleteStack.Services.LabelService;
using PDBT_CompleteStack.Services.ProjectService;
using PDBT_CompleteStack.Services.UserService;

namespace PDBT_CompleteStack.Managers;

public class IssueManager: IIssueManager
{
    private readonly IIssueService _issueService;
    private readonly IProjectService _projectService;
    private readonly IUserService _userService;
    private readonly ILabelService _labelService;
    
    public IssueManager (IIssueService issueService, IProjectService projectService, IUserService userService
        , ILabelService labelService)
    {
        _issueService = issueService;
        _projectService = projectService;
        _userService = userService;
        _labelService = labelService;
    }
    
    public async Task<bool> AddIssue(IssueDTO issueDto)
    {
        if (!await _projectService.ValidateUserAndProjectId(issueDto.RootProjectID))
            return false;

        var issue = await _issueService.AddIssue(issueDto);
        
        _projectService.AddIssueToProject(issue);

        if (issueDto.Labels is not null)
        {
            var labels = await _labelService.GetLabelsByList(issueDto.Labels, issue.RootProjectID);
            issue = await _issueService.AddLabelsToIssue(issue, labels.ToList());
        }

        if (issueDto.Assignees is not null)
        {
            var users = await _userService.GetUsersByList(issueDto.Assignees, issue.RootProjectID);
            issue = await _issueService.AddUsersToIssue(issue, users.ToList());
        }

        return true;
    }
}