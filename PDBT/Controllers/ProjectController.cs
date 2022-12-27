using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDBT_CompleteStack.Models;
using PDBT_CompleteStack.Repository.Interfaces;

namespace PDBT_CompleteStack.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public ProjectController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: api/Project
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {

        }

        // GET: api/Project/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {

        }

        // PUT: api/Project/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, ProjectDTO projectDto)
        {

        }

        // POST: api/Project
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(ProjectDTO projectDto)
        {

        }

        // DELETE: api/Project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {

        }

        private Project DtoToProject(ProjectDTO projectDto) =>
            new Project()
            {
                Id = projectDto.Id,
                Name = projectDto.Name,
                Description = projectDto.Description
            };

        private async Task<bool> verifyUser(Project project)
        {
            if (User?.Identity?.Name != null)
            {
                var authUserId = Int32.Parse(User.Identity.Name);
                var authUser = await _context.Users.GetByIdAsync(authUserId);

                if (project.Users.Contains(authUser))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
