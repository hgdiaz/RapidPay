
using Microsoft.EntityFrameworkCore;
using RapidPayAPI.Data;
using RapidPayAPI.Domain;
using RapidPayAPI.Features.Payments;

namespace RapidPayAPI.Services
{
    public class FeeService : IFeeService
    {

        private double Fee { get; set; }
        private DateTime NextCalculation { get; set; }
        private Random rnd { get; set; }

        public FeeService()
        {
            Fee = 1;
            NextCalculation = DateTime.Now.AddMinutes(-1);
            rnd = new Random();
        }

        public double GetFee()
        {
            if (NextCalculation <= DateTime.Now)
            {
                //Every hour, the Universal Fees Exchange (UFE) randomly selects a decimal between 0 and 2.
                double value = rnd.NextDouble() * 2;
                NextCalculation = DateTime.Now.AddHours(1);
                //The new fee price is the last fee amount multiplied by the recent random decimal.
                Fee *= value;
            }
            return Fee;
        }


    }
}
