namespace RapidPayAPI.Features.Cards.Exceptions
{
    public class InsuficientBalanceException : Exception
    {
        public InsuficientBalanceException() : base($"The card doesn't have enough balance for this payment.") { }
    }
}
