using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PDBT_CompleteStack.Models;
using PDBT_CompleteStack.Repository.Interfaces;
using PDBT_CompleteStack.Services.UserService;

namespace PDBT_CompleteStack.Services.ProjectService;

public class ProjectService: IProjectService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _context;
    private readonly IUserService _userService;

    public ProjectService(IHttpContextAccessor httpContextAccessor, IUnitOfWork context, IUserService userService)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
        _userService = userService;
    }

    public async Task<ServiceResponse<IEnumerable<Project>>> GetAllProjects()
    {
        var allprojects = await _context.Projects.GetAllAsync();
        var projectsWithUser = allprojects.Where(UserBelongInProject).ToList();
        var response = new ServiceResponse<IEnumerable<Project>>();
        
        if (!projectsWithUser.Any())
        {
            response.Success = false;
            response.Result = new NotFoundResult();
            return response;
        }
        
        response.Data = projectsWithUser;
        response.Result = new OkObjectResult(response.Data);
        
        return response;
    }
    
    public async Task<Project?> GetProjectById(int projectId)
    {
        // Verify if the project exists and if the user has access to it
        var verify = await ValidateUserAndProjectId(projectId);
        if (!verify)
            return null;

        var project = await _context.Projects.GetByIdAsync(projectId);
        
        return project;
    }

    public Task<ServiceResponse<Issue>> UpdateProject(Issue issue)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<Project>> AddProject(ProjectDTO project)
    {
        Project _project = DtoToProject(project);
        _project.Users = new List<User>();
        _project.Issues = new List<Issue>();
        _project.Labels = new List<Label>();
        return AddProject(_project);
    }
    
    public async Task<ServiceResponse<Project>> AddProject(Project project)
    {
        _context.Projects.AddAsync(project);
        
        // Add the user who created the project to the project user list
        var userIdStr = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        int userId = Int32.Parse(userIdStr);
        var user = await _userService.GetUserById(userId);
        project.Users.Add(user.Data);

        await _context.CompleteAsync();
        
        return new ServiceResponse<Project>
        {
            Data = project,
            Result = new OkObjectResult(project),
            Success = true
        };
        
    }
    


    public Task<ServiceResponse<Issue>> DeleteProject(int id, int projectId)
    {
        throw new NotImplementedException();
    }

    public async void AddIssueToProject(Issue issue)
    {
        var project = await GetProjectById(issue.RootProjectID);

        if (project is null)
        {
            // If this exception is thrown then the id of the project listed in the issue is incorrect. This could mean 
            // that there is an error in ValidateUserAndProjectId or an error in the insertion of the issue 
            throw new NullReferenceException();
        }
        
        project.Issues.Add(issue);
    }

    public async Task<bool> ValidateUserAndProjectId(int projectId)
    {

        if (!await ProjectExist(projectId))
        {
            return false;
        }

        if (!await UserBelongInProjectAsync(projectId))
        {
            return false;
        }

        return true;
    }
    
    private async Task<bool> ProjectExist(int projectId)
    {
        return (await _context.Projects.GetAllAsync()).Any(e => e.Id == projectId);
    }

    private async Task<bool> UserBelongInProjectAsync(int projectId)
    {
        var project = await _context.Projects.GetByIdAsync(projectId);
        return UserBelongInProject(project);
    }

    private bool UserBelongInProject(Project project)
    {
        // Retrieve the user id that was just used for login
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);    
        
        if (userId != null)
        {
            var authUserId = Int32.Parse(userId);

            // If the currently signed user is present in the project assigned users 
            if (project.Users.Any(user => user.Id == authUserId))
            {
                return true;
            }
        }
        return false;
    }
    
    private Project DtoToProject(ProjectDTO projectDto) =>
        new Project()
        {
            Id = projectDto.Id,
            Name = projectDto.Name,
            Description = projectDto.Description
        };
}