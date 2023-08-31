namespace RapidPayAPI.Features.Cards.Exceptions
{
    public class CardExistsException : Exception
    {
        public CardExistsException(string number) : base($"Card number {number} already exist.") { }
    }
}
