namespace SEBtask.Models.Dtos
{
    public class AgreementDto
    {
        public int Id { get; set; }
        public long ClientPersonalId { get; set; }
        public decimal Amount { get; set; }
        public string BaseRateCode { get; set; }
        public decimal Margin { get; set; }
        public long Duration { get; set; }
    }
}
