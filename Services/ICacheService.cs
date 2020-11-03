using SEBtask.Enums;
using SEBtask.Models.Dtos;
using SEBtask.Models.Entities;
using System.Collections.Generic;

namespace SEBtask.Services
{
    public interface ICacheService
    {
        public CacheDto<Client> GetClient(long clientPersonalId);
        public void SetClient(Client client);
        public CacheDto<AgreementsWrapDto> GetAgreements(long clientPersonalId);
        public void SetAgreements(AgreementsWrapDto agreements, long clientPersonalId);
        public void SetAgreements(IEnumerable<Agreement> agreements, long clientPersonalId);
        public CacheDto<Agreement> GetAgreement(long clientPersonalId, long agreementId);
        public void SetAgreement(Agreement agreement, long clientPersonalId);
        public CacheDto<decimal?> GetBaseRateValue(BaseRateCode baseRateCode);
        public void SetBaseRateValue(BaseRateCode baseRateCode, decimal baseRateValue);
    }
}
