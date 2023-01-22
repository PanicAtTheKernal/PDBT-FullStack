using Microsoft.EntityFrameworkCore;
using PDBT_CompleteStack.Data;
using PDBT_CompleteStack.Models;
using PDBT_CompleteStack.Repository.Interfaces;

namespace PDBT_CompleteStack.Repository.Classes;

public class IssueRepository : GenericRepository<Issue>, IIssueRepository
{
    public IssueRepository(PdbtContext context):base(context)
    {
        
    }

    public override Issue GetById(int id)
    {
        return _context.Issues.Where(i => i.Id == id)
            .Include(i => i.Labels)
            .FirstOrDefault()!;
    }
    
    public override async Task<Issue?> GetByIdAsync(int id)
    {
        return (await _context.Issues.Where(i => i.Id == id)
            .Include(i => i.Labels)
            .FirstOrDefaultAsync())!;
    }

    public override IEnumerable<Issue> GetAll()
    {
        return _context.Issues
            .Include(i => i.Labels)
            .ToList();
    }

    public override async Task<IEnumerable<Issue>> GetAllAsync()
    {
        return await _context.Issues
            .Include(i => i.Labels)
            .ToListAsync();
    }
}