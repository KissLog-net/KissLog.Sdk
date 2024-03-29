[![KissLog.net](https://kisslog.net/cdn/KissLog/logos/kisslog-logo-64.png)](https://kisslog.net/)

[![Latest version](https://img.shields.io/nuget/v/KissLog.svg?style=flat-square&label=KissLog)](https://www.nuget.org/packages?q=kisslog) [![Downloads](https://img.shields.io/nuget/dt/KissLog.svg?style=flat-square&label=Downloads)](https://www.nuget.org/packages?q=kisslog)

KissLog is a lightweight and highly customizable logging and monitoring framework for .NET applications.

Some of the main features of KissLog are:

- Automatically captures and logs all the exceptions

- Monitors all the HTTP traffic

- Object oriented implementation

- Centralized logging using [kisslog.net](https://kisslog.net) or [KissLog server](https://github.com/KissLog-net/KissLog-server)

Check the [documentation](https://kisslog.net/Docs/SDK.index.html) for a complete list of features.

![KissLog.net centralized logging](https://user-images.githubusercontent.com/39127098/221180824-f132600a-f846-4319-8f7d-13e317c66433.png)

## .NET support

- [.NET 6 Web App](https://kisslog.net/Docs/SDK.install-instructions.dotnet6-webApp.html)
- [.NET 6 Console App](https://kisslog.net/Docs/SDK.install-instructions.dotnet6-consoleApp.html)
- [.NET Core Web App](https://kisslog.net/Docs/SDK.install-instructions.netcore-webApp.html)
- [.NET Core Console App](https://kisslog.net/Docs/SDK.install-instructions.netcore-consoleApp.html)
- [ASP.NET MVC](https://kisslog.net/Docs/SDK.install-instructions.aspnet-mvc.html)
- [ASP.NET WebApi](https://kisslog.net/Docs/SDK.install-instructions.aspnet-webapi.html)
- [.NET Framework Console App](https://kisslog.net/Docs/SDK.install-instructions.netframework-consoleApp.html)

## Why KissLog?

KissLog implements three main components: logging functionality, exceptions tracking and application insights.

For web applications, KissLog automatically captures all the HTTP properties.

KissLog keeps the log events in memory and sends them to the registered listeners all at once. This can help reduce the load of the persistence implementation (such as Disk I/O, database operations or network throughput).

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

Custom log listeners can be [easily implemented](https://kisslog.net/Docs/SDK.examples.MongoDB-listener.html).

Using [interceptors](https://kisslog.net/Docs/SDK.advanced.Filtering-the-logs.html), log listeners can apply conditional filtering rules before saving the events.

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

KissLog supports various [configuration options](https://kisslog.net/Docs/SDK.using-kisslog.Configuration.html) using the ``KissLogConfiguration.Options`` configuration object.

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
