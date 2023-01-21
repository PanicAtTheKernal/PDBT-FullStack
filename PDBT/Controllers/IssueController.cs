using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDBT_CompleteStack.Managers;
using PDBT_CompleteStack.Models;
using PDBT_CompleteStack.Services.IssueService;
using PDBT_CompleteStack.Services.LabelService;
using PDBT_CompleteStack.Services.ProjectService;

namespace PDBT_CompleteStack.Controllers
{
    [Route("api/{projectId}/[controller]")]
    [ApiController, Authorize]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService _issueService;
        private readonly IIssueManager _issueManager;
        
        public IssueController(IssueService issueService, IIssueManager issueManager)
        {
            _issueService = issueService;
            _issueManager = issueManager;
        }

        // GET: api/Issue
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Issue>>> GetIssues(int projectId)
        {
            var issuesResponse = await _issueService.GetAllIssue(projectId);

            return issuesResponse.Result;
        }

        // GET: api/Issue/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Issue>> GetIssue(int id, int projectId)
        {
            var issueResponse = await _issueService.GetIssueById(id, projectId);

            return issueResponse.Result;
        }

        // PUT: api/Issue/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Issue>> PutIssue(int id, IssueDTO issueDto, int projectId)
        {
            // Returns 404 or 400 depending on the problem
            /*var response = await _projectService.ValidateUserAndProjectId(projectId);
            if (!response.Success) return response.Result;*/

            var issueResponse = await _issueService.ConvertDto(id, issueDto, projectId);
            // if (!issueResponse.Success) return issueResponse.Result;
            //
            // if (issueDto.Labels != null)
            //     issueResponse = await _labelService.UpdateLabelsInIssue(issueResponse.Data!, issueDto.Labels);
            //
            // await _issueService.UpdateIssue(issueResponse.Data!);
            // issueResponse = await _issueService.SaveChanges(id);
            return issueResponse.Result;
        }

        // POST: api/Issue
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Issue>> PostIssue(IssueDTO issueDto)
        {
            var issueResponse = await _issueManager.AddIssue(issueDto);

            return new OkObjectResult(issueResponse);
        }

        // DELETE: api/Issue/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Issue>> DeleteIssue(int id, int projectId)
        {
            // Returns 404 or 400 depending on the problem
            /*var response = await _projectService.ValidateUserAndProjectId(projectId);
            if (!response.Success) return response.Data!;*/

            var results = await _issueService.DeleteIssue(id, projectId);
            return results.Result;
        }
    }
}
