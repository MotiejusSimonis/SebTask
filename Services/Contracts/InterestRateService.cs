using SEBtask.Models.Dtos;
using SEBtask.Models.Entities;

namespace SEBtask.Services
{
    public class InterestRateService : IInterestRateService
    {
        public InterestDto GetInterestRates(Agreement agreement, decimal? baseRate, decimal? newBaseRate)
        {
            var interestRate = baseRate == null ? (decimal?)null : CountInterestRate(baseRate ?? 0, agreement.Margin);
            var newInterestRate = newBaseRate == null ? (decimal?)null : CountInterestRate(newBaseRate ?? 0, agreement.Margin);
            return new InterestDto
            {
                InterestRate = interestRate,
                NewInterestRate = newInterestRate,
            };
        }

        private decimal CountInterestRate(decimal baseRateValue, decimal margin)
        {
            return baseRateValue + margin;
        }
    }
}
