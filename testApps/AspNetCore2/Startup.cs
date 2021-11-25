﻿using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using KissLog.Formatters;
using KissLog.Listeners.FileListener;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AspNetCore2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddLogging(logging =>
            {
                logging.AddKissLog(options =>
                {
                    options.Formatter = (FormatterArgs args) =>
                    {
                        if (args.Exception == null)
                            return args.DefaultValue;

                        string exceptionStr = new ExceptionFormatter().Format(args.Exception, args.Logger);

                        return string.Join(Environment.NewLine, new[] { args.DefaultValue, exceptionStr });
                    };
                });
            });

            services.AddSession();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseSession();

            app.UseKissLogMiddleware(options => ConfigureKissLog(options));

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void ConfigureKissLog(IOptionsBuilder options)
        {
            // optional KissLog configuration
            options.Options
                .AppendExceptionDetails((Exception ex) =>
                {
                    StringBuilder sb = new StringBuilder();

                    if (ex is NullReferenceException nullRefException)
                    {
                        sb.AppendLine("Important: check for null references");
                    }

                    return sb.ToString();
                })
                .GenerateSearchKeywords((FlushLogArgs args) =>
                {
                    var service = new GenerateSearchKeywordsService();
                    IEnumerable<string> keywords = service.GenerateKeywords(args);

                    return keywords;
                });

            // KissLog internal logs
            options.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };

            // register listeners
            KissLogConfiguration.Listeners
                .Add(new RequestLogsApiListener(new Application(Configuration["KissLog.OrganizationId"], Configuration["KissLog.ApplicationId"]))
                {
                    ApiUrl = Configuration["KissLog.ApiUrl"],
                    Interceptor = new StatusCodeInterceptor
                    {
                        MinimumLogMessageLevel = LogLevel.Trace,
                        MinimumResponseHttpStatusCode = 200
                    }
                })
                .Add(new LocalTextFileListener("Logs_onFlush", FlushTrigger.OnFlush))
                .Add(new LocalTextFileListener("Logs_onMessage", FlushTrigger.OnMessage));
        }
    }
}