using SEBtask.Models.Dtos;
using SEBtask.Models.Entities;

namespace SEBtask.Services
{
    public interface IInterestRateService
    {
        public InterestDto GetInterestRates(Agreement agreement, decimal? baseRate, decimal? newBaseRate);
    }
}