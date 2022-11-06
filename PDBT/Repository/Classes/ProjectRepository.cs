using Microsoft.EntityFrameworkCore;
using PDBT_CompleteStack.Data;
using PDBT_CompleteStack.Models;
using PDBT_CompleteStack.Repository.Interfaces;

namespace PDBT_CompleteStack.Repository.Classes;

public class ProjectRepository: GenericRepository<Project>, IProjectRepository
{
    public ProjectRepository(PdbtContext context):base(context)
    {
        
    }
    
    public override Project GetById(int id)
    {
        return _context.Projects.Where(p => p.Id == id)
            .Include(p => p.Issues)
            .Include(p => p.Users)
            .FirstOrDefault()!;
    }
    
    public override async Task<Project> GetByIdAsync(int id)
    {
        return (await _context.Projects.Where(p => p.Id == id)
            .Include(p => p.Issues)
            .Include(p => p.Users)
            .FirstOrDefaultAsync())!;
    }

    public override IEnumerable<Project> GetAll()
    {
        return _context.Projects
            .Include(p => p.Issues)
            .Include(p => p.Users)
            .ToList();
    }

    public override async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _context.Projects
            .Include(p => p.Issues)
            .Include(p => p.Users)
            .ToListAsync();
    }
}