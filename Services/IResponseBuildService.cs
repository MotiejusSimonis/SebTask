using SEBtask.Models.Dtos;
using SEBtask.Models.Entities;
using SEBtask.Models.Responses;
using System.Collections.Generic;

namespace SEBtask.Services
{
    public interface IResponseBuildService
    {
        public LoginResponse GetLoginRespose(Client client, string token);
        public AgreementsResponse GetAgreementsRespose(Client client, IEnumerable<Agreement> agreements);
        public AgreementsResponse GetAgreementsRespose(Client client, Agreement agreement);
        public InterestsResponse GetInterestsRespose(Client client, Agreement agreement, InterestDto interestDto);
    }
}
