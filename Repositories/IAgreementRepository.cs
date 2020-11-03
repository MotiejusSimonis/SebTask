using SEBtask.Models.Entities;
using System.Linq;

namespace SEBtask.Repositories
{
    public interface IAgreementRepository : IBaseRepository<Agreement>
    {
        public IQueryable<Agreement> GetAgreements(long clientPersonalId);
        public Agreement GetAgreement(long clientPersonalId, long agreementId);
    }
}