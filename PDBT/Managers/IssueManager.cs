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
        {
            return false;
        }

        var issue = _issueService.AddIssue(issueDto);
        
        
        
        
        return true;
    }
}