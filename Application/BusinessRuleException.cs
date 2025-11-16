namespace api_biblioteca.Middleware
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string message) : base(message) {}
    }
}
