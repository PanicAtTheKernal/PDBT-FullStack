namespace PDBT_CompleteStack.Repository.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IIssueRepository Issues { get; set; }
    ILabelRepository Labels { get; set; }
    IUserRepository Users { get; set; }
    IProjectRepository Projects { get; set; }

    int Complete();
    Task<int> CompleteAsync();
}