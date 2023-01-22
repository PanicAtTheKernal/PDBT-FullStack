using Microsoft.AspNetCore.Mvc;
using PDBT_CompleteStack.Models;
using PDBT_CompleteStack.Repository.Interfaces;

namespace PDBT_CompleteStack.Services.LabelService;

public class LabelService : ILabelService
{
    private readonly IUnitOfWork _context;

    public LabelService(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<IEnumerable<Label>>> GetAllLabel(int projectId)
    {
        throw new NotImplementedException();
    }

    public async Task<Label?> GetLabelById(int id, int projectId)
    {
        var label = await _context.Labels.GetByIdAsync(id);
        var response = new ServiceResponse<Label>();

        if (label is null)
        {
            return null;
        }
        
        if (label.RootProject.Id != projectId)
        {
            return null;
        }
        
        return label;
    }

    public async Task<IEnumerable<Label>> GetLabelsByList(ICollection<int> ids, int projectId)
    {
        List<Label> labelsResult = new List<Label>();

        foreach (var id in ids)
        {
            var label = await GetLabelById(id, projectId);
            if (label is not null)
            {
                labelsResult.Add(label);
            }
        }

        return labelsResult;
    }

    public async Task<ServiceResponse<Label>> UpdateLabel(int id, LabelDTO labelDto, int projectId)
    {
        throw new NotImplementedException();
    }
}