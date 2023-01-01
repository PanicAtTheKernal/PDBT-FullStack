using Microsoft.AspNetCore.Mvc;
using PDBT_CompleteStack.Models;

namespace PDBT_CompleteStack.Services.ProjectService;

public interface IProjectService
{
    Task<ServiceResponse<IEnumerable<Project>>> GetAllProjects();
    Task<ServiceResponse<Project>> GetProjectById(int projectId);
    Task<ServiceResponse<Issue>> UpdateProject(Issue issue);
    Task<ServiceResponse<Project>> AddProject(Project project);
    Task<ServiceResponse<Project>> AddProject(ProjectDTO project);
    Task<ServiceResponse<Issue>> DeleteProject(int id, int projectId);
    Task<ServiceResponse<ActionResult>> ValidateUserAndProjectId(int projectId);
}