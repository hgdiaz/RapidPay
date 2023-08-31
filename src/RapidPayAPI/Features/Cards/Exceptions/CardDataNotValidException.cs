namespace RapidPayAPI.Features.Cards.Exceptions
{
    public class CardDataNotValidException : Exception
    {
        public CardDataNotValidException(string messages) : base($"Please correct these errors: {messages}") { }
    }
}
