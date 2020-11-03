using SEBtask.Models.Entities;
using SEBtask.Models.Requests;

namespace SEBtask.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public Client GetClient(LoginRequest login)
        {
            return FindFirstByCondition(x => x.Email == login.Email && x.Password == login.Password);
        }
    }
}
