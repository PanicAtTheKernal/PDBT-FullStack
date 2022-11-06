using Microsoft.EntityFrameworkCore;
using PDBT_CompleteStack.Models;

namespace PDBT_CompleteStack.Data;

public interface IPdbtContext
{
    public DbSet<Issue> Issues { get; set; }
    public DbSet<Label> Labels { get; set; }
    public DbSet<LinkedIssue> LinkedIssues { get; set; }
    
    public Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> Entry<TEntity> (TEntity entity) where TEntity : class;
    public Task<int> SaveChangesAsync (CancellationToken cancellationToken = default);
}