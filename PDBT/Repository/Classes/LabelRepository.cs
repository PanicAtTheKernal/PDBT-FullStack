using PDBT_CompleteStack.Data;
using PDBT_CompleteStack.Models;
using PDBT_CompleteStack.Repository.Interfaces;

namespace PDBT_CompleteStack.Repository.Classes;

public class LabelRepository: GenericRepository<Label>, ILabelRepository
{
    public LabelRepository(PdbtContext context):base(context)
    {
    }
}