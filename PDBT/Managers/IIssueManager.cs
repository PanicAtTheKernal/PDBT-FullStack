using PDBT_CompleteStack.Models;

namespace PDBT_CompleteStack.Managers;

public interface IIssueManager
{
    Task<bool> AddIssue(IssueDTO issueDto);
}