namespace RapidPayAPI.Constants
{
    public static class ValidationConstants
    {
        public const string CardNumberRegex = "(\\d{15})$"; // --> exactly 15 digits

        public const string CardCVCRegex = "(\\d{3})$"; // --> exactly 3 digits
    }
}
