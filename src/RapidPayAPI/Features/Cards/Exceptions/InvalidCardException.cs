namespace RapidPayAPI.Features.Cards.Exceptions
{
    public class InvalidCardException : Exception
    {
        public InvalidCardException() : base($"The values for the card are incorrect.") { }
    }
}
