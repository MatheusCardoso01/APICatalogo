
namespace APICatalogo.Logging;

public class CustomLogger : ILogger
{
    readonly string loggerName;
    readonly CustomLoggerProviderConfiguration loggerConfig;

    public CustomLogger(string name, CustomLoggerProviderConfiguration config)
    {
        loggerName = name;
        loggerConfig = config;
    }

    public IDisposable? BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == loggerConfig.LogLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

        EscreverTextoNoArquivo(mensagem);
    }

    private void EscreverTextoNoArquivo(string mensagem)
    {
        string logDiretorio = @"C:\Users\matheus.cardoso\projetos_dotnet\APICatalogo\APICatalogo\Logging\Logs\";
        Directory.CreateDirectory(logDiretorio);

        string caminhoArquivoLog = Path.Combine(logDiretorio, "Executions_Log.txt");

        using (StreamWriter writer = new StreamWriter(caminhoArquivoLog, true))
        {
            try
            {
                writer.WriteLine(mensagem);
                writer.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
