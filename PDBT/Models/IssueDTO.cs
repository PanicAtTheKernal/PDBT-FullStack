namespace PDBT_CompleteStack.Models;

public class IssueDTO
{
    public int Id { get; set; }
    public string IssueName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IssueType Type { get; set; } = IssueType.Bug;
    public IssuePriority Priority { get; set; } = IssuePriority.Medium;
    public DateTime? TimeForCompletion { get; set; }
    public DateTime? DueDate { get; set; }
    public ICollection<int>? Labels { get; set; }
    public ICollection<int> Assignees { get; set; }
    public ICollection<int>? LinkedIssues { get; set; }
    public int RootProjectID { get; set; }
}