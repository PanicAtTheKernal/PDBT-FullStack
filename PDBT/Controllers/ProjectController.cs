using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDBT_CompleteStack.Models;
using PDBT_CompleteStack.Repository.Interfaces;
using PDBT_CompleteStack.Services.ProjectService;

namespace PDBT_CompleteStack.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: api/Project
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var results = await _projectService.GetAllProjects();
            return results.Result;
        }

        // GET: api/Project/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var results = await _projectService.GetProjectById(id);
            return results.Result;
        }

        // PUT: api/Project/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, ProjectDTO projectDto)
        {
            return null;
        }

        // POST: api/Project
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(ProjectDTO projectDto)
        {
            var results = await _projectService.AddProject(projectDto);
            
            return results.Result;
        }

        // DELETE: api/Project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            return null;
        }

    }
}
