namespace TreeManager.Services.Api.Infrastructure.ExceptionHandling
{
    using System;
    public class SecureException : Exception
    {
        public SecureException(string message) : base($"{nameof(SecureException)}: {message}")
        {
        }
    }
}
