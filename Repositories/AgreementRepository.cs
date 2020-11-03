using SEBtask.Models.Entities;
using SEBtask.Models.Requests;
using System.Linq;

namespace SEBtask.Repositories
{
    public class AgreementRepository : BaseRepository<Agreement>, IAgreementRepository
    {
        public AgreementRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IQueryable<Agreement> GetAgreements(long clientPersonalId)
        {
            return FindAllByCondition(x => x.ClientPersonalId == clientPersonalId);
        }
        
        public Agreement GetAgreement(long clientPersonalId, long agreementId)
        {
            return FindFirstByCondition(x => x.Id == agreementId && x.ClientPersonalId == clientPersonalId);
        }
    }
}
