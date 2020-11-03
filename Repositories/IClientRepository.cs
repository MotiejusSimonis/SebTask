using SEBtask.Models.Entities;
using SEBtask.Models.Requests;

namespace SEBtask.Repositories
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        public Client GetClient(LoginRequest loginRequest);
    }
}