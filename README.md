# KissLog

[![Latest version](https://img.shields.io/nuget/v/KissLog.svg?style=flat-square&label=KissLog)](https://www.nuget.org/packages?q=kisslog) [![Downloads](https://img.shields.io/nuget/dt/KissLog.svg?style=flat-square&label=Downloads)](https://www.nuget.org/packages?q=kisslog)

KissLog is the built-in .NET integration for saving the logs to [logBee.net](https://logbee.net).

Check the [documentation](https://logbee.net/Docs/Integrations.KissLog-net.index.html) for a complete list of features.

## .NET support

- [.NET 6 Web App](https://logbee.net/Docs/Integrations.KissLog-net.install-instructions.dotnet6-webApp.html)
- [.NET 6 Console App](https://logbee.net/Docs/Integrations.KissLog-net.install-instructions.dotnet6-consoleApp.html)
- [.NET Core Web App](https://logbee.net/Docs/Integrations.KissLog-net.install-instructions.netcore-webApp.html)
- [.NET Core Console App](https://logbee.net/Docs/Integrations.KissLog-net.install-instructions.netcore-consoleApp.html)
- [ASP.NET MVC](https://logbee.net/Docs/Integrations.KissLog-net.install-instructions.aspnet-mvc.html)
- [ASP.NET WebApi](https://logbee.net/Docs/Integrations.KissLog-net.install-instructions.aspnet-webapi.html)
- [.NET Framework Console App](https://logbee.net/Docs/Integrations.KissLog-net.install-instructions.netframework-consoleApp.html)

## Why KissLog?

KissLog implements three main components: logging functionality, exceptions tracking and application insights.

For web applications, KissLog automatically captures all the HTTP properties.

KissLog keeps the log events in memory and sends them to the registered listeners all at once. This can help reduce the load of the persistence implementation (such as Disk I/O, database operations or network throughput).

Centralized logging using [logBee.net](https://logbee.net) or [logBee on-premises](https://github.com/logBee-net/logBee-app) app.

<table><tr><td>
    <img alt="logBee.net centralized logging" width="600" src="https://github.com/KissLog-net/KissLog.Sdk/assets/39127098/583ff625-d0ce-4ebc-b0d7-9a3b0257d3ef" />
</td></tr></table>

## Basic usage

```csharp
using KissLog;
using KissLog.Listeners.FileListener;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            KissLogConfiguration.Listeners
                .Add(new LocalTextFileListener("logs", FlushTrigger.OnFlush));

            var logger = new Logger();

            logger.Trace("Hey, I am a log message");

            Logger.NotifyListeners(logger);
        }
    }
}
```

## Saving the logs

KissLog saves the logs to multiple output locations by using log listeners.

Log listeners are registered at application startup using the `KissLogConfiguration.Listeners` container.

Custom log listeners can be [easily implemented](https://logbee.net/Docs/Integrations.KissLog-net.examples.MongoDB-listener.html).

Using [interceptors](https://logbee.net/Docs/Integrations.KissLog-net.advanced.Filtering-the-logs.html), log listeners can apply conditional filtering rules before saving the events.

```csharp
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            KissLogConfiguration.Listeners
                .Add(new LocalTextFileListener("logs", FlushTrigger.OnMessage))
                .Add(new CustomMongoDbListener("mongodb://localhost:27017", "Logs")
                {
                    Interceptor = new LogLevelInterceptor(LogLevel.Information)
                });

            var logger = new Logger();
            logger.Trace("Hey, I am a log message");

            Logger.NotifyListeners(logger);
        }
    }
}
```

## Configuration

KissLog supports various [configuration options](https://logbee.net/Docs/Integrations.KissLog-net.using-kisslog.Configuration.html) using the ``KissLogConfiguration.Options`` configuration object.

```csharp
private void ConfigureKissLog
{
    KissLogConfiguration.Options
        .AppendExceptionDetails((Exception ex) =>
        {
            if (ex is DivideByZeroException zeroDivisionEx)
                return ">>> Should check if the denominator is zero before dividing";

            return null;
        });
}
```

![AppendExceptionDetails](https://raw.githubusercontent.com/wiki/KissLog-net/KissLog.Sdk/images/AppendExceptionDetails.png)

## Samples

Check the [test applications](https://github.com/KissLog-net/KissLog.Sdk/tree/master/testApps) for more examples of using KissLog.

## Feedback

Please use the [issues](https://github.com/KissLog-net/KissLog.Sdk/issues) section to report bugs, suggestions and general feedback.

## Contributing

All contributions are very welcomed: code, documentation, samples, bug reports, feature requests.

## License

[Apache-2.0](LICENSE.md)
