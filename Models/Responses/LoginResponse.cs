using SEBtask.Models.Dtos;

namespace SEBtask.Models.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public ClientDto Client { get; set; }
    }
}
