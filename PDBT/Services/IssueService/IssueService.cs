using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDBT_CompleteStack.Models;
using PDBT_CompleteStack.Repository.Interfaces;
using PDBT_CompleteStack.Services.LabelService;
using PDBT_CompleteStack.Services.ProjectService;

namespace PDBT_CompleteStack.Services.IssueService;

public class IssueService : IIssueService
{
    private readonly IUnitOfWork _context;
    private readonly ILabelService _labelService;
    private readonly IProjectService _projectService;
    
    public IssueService(IUnitOfWork context, ILabelService labelService, IProjectService projectService)
    {
        _context = context;
        _labelService = labelService;
    }

    public async Task<ServiceResponse<IEnumerable<Issue>>> GetAllIssue(int projectId)
    {
        var enumerable = await _context.Issues.GetAllAsync();

        var issues = enumerable.Where(i => i.RootProjectID == projectId).ToList();
        var response = new ServiceResponse<IEnumerable<Issue>>();

        if (!issues.Any())
        {
            response.Success = false;
            response.Result = new NotFoundResult();
            return response;
        }

        response.Data = issues;
        response.Result = new OkObjectResult(response.Data);
        
        return response;
    }
    
    public async Task<Issue?> GetIssueById(int id, int projectId)
    {
        var issue = await _context.Issues.GetByIdAsync(id);

        if (issue == null || issue.RootProjectID != projectId)
        {
            return null;
        }
        
        return issue;
    }

    public async Task<ServiceResponse<Issue>> ConvertDto(int id, IssueDTO issueDto,int projectId)
    {
        var response = new ServiceResponse<Issue>();
        
        // Prevents the Id being updated
        if (id != issueDto.Id)
        {
            response.Success = false;
            response.Result = new BadRequestObjectResult("Invalid Id");
            return response;
        }
            
        // Prevent the root project Id from being updated
        if (projectId != issueDto.RootProjectID) 
        {
            response.Success = false;
            response.Result = new BadRequestObjectResult("Invalid Project Id");
            return response;
        }
        
        var issue = DtoToIssue(issueDto);

        if (IssueExists(issue.Id))
        {
            // Retrieve the fully updated issue as the dto is only partially updated. Used for the put method
            response.Data = await _context.Issues.GetByIdAsync(issue.Id);
        }
        else
        {
            response.Data = issue;
        }
        response.Result = new OkObjectResult(response.Data);

        return response;
    }
    
    public async Task<ServiceResponse<Issue>> UpdateIssue(IssueDTO issue)
    {
        var response = new ServiceResponse<Issue>();
        // await _context.Issues.Update(issue);
        //
        // response.Data = await _context.Issues.GetByIdAsync(issue.Id);
        // response.Result = new OkObjectResult(response.Data);

        return response;
    }

    public async Task<Issue> AddIssue(IssueDTO issueDto)
    {
        var issue = DtoToIssue(issueDto);
        _context.Issues.AddAsync(issue);

        await _context.CompleteAsync();

        return issue;
    }

    public async Task<bool> DeleteIssue(int id, int projectId)
    {
        var issueToDel = await GetIssueById(id, projectId);
        if (issueToDel is null) return false;

        _context.Issues.Remove(issueToDel);
        await SaveChanges(issueToDel.Id);
        return true;
    }

    public async Task<ServiceResponse<Issue>> SaveChanges(int id)
    {
        try
        {
            await _context.CompleteAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!IssueExists(id))
            {
                return new ServiceResponse<Issue>
                {
                    Result = new NotFoundResult(),
                    Success = false
                };
            }
            throw;
        }

        return new ServiceResponse<Issue>
        {
            Result = new NoContentResult(),
            Success = false
        };
    }

    public async Task<Issue> AddLabelsToIssue(Issue issue, ICollection<Label> labels)
    {
        if (issue.Labels is null)
            issue.Labels = new List<Label>();

        foreach (var label in labels)
        {
            //Check if the label is currently not in the 
            if (issue.Labels.All(x => x != label))
            {
                issue.Labels.Add(label);
            }
        }

        await _context.CompleteAsync();
        return issue;
    }
    
    public async Task<Issue> AddUsersToIssue(Issue issue, ICollection<User> users)
    {
        if (issue.Assignees is null)
            issue.Assignees = new List<User>();
        
        foreach (var user in users)
        {
            //Check if the label is currently not in the 
            if (issue.Assignees.All(x => x != user))
            {
                issue.Assignees.Add(user);
            }
        }

        await _context.CompleteAsync();
        return issue;
    }

    public async Task<Issue> AddIssuesToIssue(Issue issue, ICollection<Issue> linkedIssues)
    {
        // foreach (var linkedIssue in linkedIssues)
        // {
        //     if (issue.LinkedIssues.All(x => x != linkedIssue))
        //     {
        //         
        //     }
        // }
        throw new NotImplementedException();
    }
    
    public async Task<IEnumerable<Issue>> GetIssuesByList(ICollection<int> ids, int projectId)
    {
        List<Issue> linkedIssues = new List<Issue>();

        foreach (var id in ids)
        {
            var issue = await GetIssueById(id, projectId);
            if (issue is not null)
                linkedIssues.Add(issue);    
        }

        return linkedIssues;
    }

    private Issue DtoToIssue(IssueDTO issueDto) =>
        new()
        {
            Id = issueDto.Id,
            IssueName = issueDto.IssueName,
            Description = issueDto.Description,
            Type = issueDto.Type,
            Priority = issueDto.Priority,
            DueDate = issueDto.DueDate,
            TimeForCompletion = issueDto.TimeForCompletion,
            RootProjectID = issueDto.RootProjectID
        };
    
    private bool IssueExists(int id)
    {
        return _context.Issues.GetAll().Any(e => e.Id == id);
    }
}