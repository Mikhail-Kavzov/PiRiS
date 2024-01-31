namespace PiRiS.Business.Exceptions;

public class ServiceException : Exception
{
    public ServiceException(string message) : base(message)
    {
    }
}