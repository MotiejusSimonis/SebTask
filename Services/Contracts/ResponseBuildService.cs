using AutoMapper;
using SEBtask.Models.Dtos;
using SEBtask.Models.Entities;
using SEBtask.Models.Responses;
using System.Collections.Generic;
using System.Linq;

namespace SEBtask.Services
{
    public class ResponseBuildService : IResponseBuildService
    {
        private IMapper _mapper;
        public ResponseBuildService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public LoginResponse GetLoginRespose(Client client, string token)
        {
            return new LoginResponse
            {
                Client = _mapper.Map<ClientDto>(client),
                Token = token
            };
        }
        
        public AgreementsResponse GetAgreementsRespose(Client client, IEnumerable<Agreement> agreements)
        {
            var agreementDtos = agreements.Select(x => _mapper.Map<AgreementDto>(x));
            return new AgreementsResponse
            {
                Client = _mapper.Map<ClientDto>(client),
                Agreements = agreementDtos,
            };
        }
        
        public AgreementsResponse GetAgreementsRespose(Client client, Agreement agreement)
        {
            var agreementDto = _mapper.Map<AgreementDto>(agreement);
            return new AgreementsResponse
            {
                Client = _mapper.Map<ClientDto>(client),
                Agreements = new[] { agreementDto },
            };
        }
        
        public InterestsResponse GetInterestsRespose(Client client, Agreement agreement, InterestDto interestDto)
        {
            var agreementDto = _mapper.Map<AgreementDto>(agreement);
            var clientDto = _mapper.Map<ClientDto>(client);

            return new InterestsResponse
            {
                Client = clientDto,
                Agreement = agreementDto,
                Interests = interestDto,
            };
        }
    }
}
