namespace RapidPayAPI.Features.Cards.Exceptions
{
    public class NoCardException : Exception
    {
        public NoCardException(string number) : base($"Card number {number} doesn't exist.") { }
    }
}
