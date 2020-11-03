using SEBtask.Models.Dtos;

namespace SEBtask.Models.Responses
{
    public class InterestsResponse
    {
        public ClientDto Client { get; set; }
        public AgreementDto Agreement { get; set; }
        public InterestDto Interests { get; set; }
    }
}
