namespace SistemaAPI.Dominio.Exceptions;

public class ExceptionValidacaoDados : Exception
{
    public ExceptionValidacaoDados(string message, Exception innerException) : base(message, innerException)
    {

    }

    public ExceptionValidacaoDados(string message) : base(message)
    {

    }
}
