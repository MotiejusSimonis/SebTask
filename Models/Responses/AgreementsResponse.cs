using SEBtask.Models.Dtos;
using System.Collections.Generic;

namespace SEBtask.Models.Responses
{
    public class AgreementsResponse
    {
        public ClientDto Client { get; set; }
        public  IEnumerable<AgreementDto> Agreements { get; set; }
    }
}
