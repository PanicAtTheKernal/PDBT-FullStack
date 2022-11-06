using PDBT_CompleteStack.Data;
using PDBT_CompleteStack.Models;
using PDBT_CompleteStack.Repository.Interfaces;

namespace PDBT_CompleteStack.Repository.Classes;

public class UserRepository: GenericRepository<User>, IUserRepository
{
    public UserRepository(PdbtContext context):base(context)
    {
        
    }
}