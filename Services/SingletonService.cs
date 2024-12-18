
namespace OpenApiProject1.SingletonService{


public class MySingletonService
{
    private readonly ILogger<MySingletonService> _logger;
    public MySingletonService(ILogger<MySingletonService> logger)
    {
        _logger = logger;
    }
    public void LogMessage(string message)
    {
        _logger.LogInformation(message);
    }

       
    }
}