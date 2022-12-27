using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using PDBT_CompleteStack.Models;
using PDBT_CompleteStack.Repository.Interfaces;
using PDBT_CompleteStack.Services.IssueService;
using Xunit;

namespace PDBT.Tests;

public class IssueServiceTests
{
    private readonly IssueService _sut;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
    private readonly IEnumerable<Issue> SampleIssueData = new List<Issue>()
    {
        new Issue()
        {
            Id = 1,
            IssueName = "Example",
            Description = "This is an example",
            Type = IssueType.Bug,
            Priority = IssuePriority.Medium,
            TimeForCompletion = DateTime.Now,
            DueDate = DateTime.Now,
            LinkedIssues = null,
            Labels = new List<Label>()
            {
                new Label()
                {
                    Id = 1,
                    Name = "Test"
                }
            },
            Assignees = 
        },
        new Issue()
        {
            Id = 2,
            IssueName = "Example 2",
            Description = "This is an example",
            DueDate = DateTime.Now,
            Labels = new List<Label>()
            {
                new Label()
                {
                    Id = 1,
                    Name = "Test"
                }
            },
            LinkedIssues = null,
            Priority = IssuePriority.Low,
            TimeForCompletion = DateTime.Now,
            Type = IssueType.NewFeature
        }
    }.AsEnumerable();
    
    public IssueServiceTests()
    {
        _sut = new IssueService(_unitOfWorkMock.Object);
    }
    
    [Fact]
    public async Task GetIssues_ShouldReturnAListOfIssues_WhenIssuesExistInDB()
    {
        // Arrange
        const int projectId = 0;
        _unitOfWorkMock.Setup(x => x.)
        
        // Act
        var issues = await _sut.GetAllIssue(projectId);

        // Assert
        Assert.Equal(SampleIssueData, issues.Data);
    }
}