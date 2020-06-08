

[![NuGet](https://img.shields.io/nuget/v/SilkierQuartz.svg)](https://www.nuget.org/packages/SilkierQuartz)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![Build status](https://ci.appveyor.com/api/projects/status/0ojmooqvycks11kw?svg=true)](https://ci.appveyor.com/project/MaiKeBing/silkierquartz)

SilkierQuartz is a new after merging  [SilkierQuartz](https://github.com/jlucansky/SilkierQuartz) and  [QuartzHostedService](https://github.com/mukmyash/QuartzHostedService)!

> [Quartz.NET](https://www.quartz-scheduler.net) is a full-featured, open source job scheduling system that can be used from smallest apps to large scale enterprise systems.


> [SilkierQuartz](https://github.com/jlucansky/SilkierQuartz)  SilkierQuartz is powerful, easy to use web management tool for Quartz.NET

>  [QuartzHostedService](https://github.com/mukmyash/QuartzHostedService) QuartzHostedService is easy to host Quartz as service in .Net Core !


So  

SilkierQuartz can be used within your existing application with minimum effort as a Quartz.NET plugin when it automatically creates embedded web server. Or it can be plugged into your existing OWIN-based web application as a middleware.


![Demo](https://raw.githubusercontent.com/jlucansky/public-assets/master/SilkierQuartz/demo.gif)

The goal of this project is to provide convenient tool to utilize most of the functionality that Quartz.NET enables. The biggest challenge was to create a simple yet effective editor of job data map which is heart of Quartz.NET. Every job data map item is strongly typed and SilkierQuartz can be easily extended with a custom editor for your specific type beside standard supported types such as String, Integer, DateTime and so on. 

SilkierQuartz was created with **Semantic UI** and **Handlebars.Net** as the template engine.

## Features
- Add, modify jobs and triggers
- Add, modify calendars (Annual, Cron, Daily, Holiday, Monthly, Weekly)
- Change trigger type to Cron, Simple, Calendar Interval or Daily Time Interval
- Set typed job data map values (bool, DateTime, int, float, long, double, decimal, string, byte[])
- Create custom type editor for complex type in job data map
- Manage scheduler state (standby, shutdown)
- Pause and resume job and trigger groups
- Pause and resume triggers individually
- Pause and resume all triggers for specific job
- Trigger specific job immediately
- Watch currently executing jobs
- Interrupt executing job
- See next scheduled dates for Cron
- See recent job history, state and error messages

## Install
SilkierQuartz is available on [nuget.org](https://www.nuget.org/packages/SilkierQuartz)

To install SilkierQuartz, run the following command in the Package Manager Console
```powershell
PM> Install-Package SilkierQuartz
```
## Minimum requirements
 
- .NET Core 3.1
  

### OWIN middleware
Add to your `Startup.cs` file:
```csharp
public void Configuration(IAppBuilder app)
{
    app.UseSilkierQuartz(new SilkierQuartzOptions()
    {
        Scheduler = StdSchedulerFactory.GetDefaultScheduler().Result
    });
}
```

### ASP.NET Core middleware
Add to your `Startup.cs` file:
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSilkierQuartz();
}

public void Configure(IApplicationBuilder app)
{
    app.UseSilkierQuartz(new SilkierQuartzOptions()
    {
        Scheduler = StdSchedulerFactory.GetDefaultScheduler().Result
    });
}
```

## Notes
In clustered environment, it make more sense to host SilkierQuartz on single dedicated Quartz.NET node in standby mode and implement own `IExecutionHistoryStore` depending on database or ORM framework you typically incorporate. Every clustered Quarz.NET node should be configured with `ExecutionHistoryPlugin` and only dedicated node for management may have `SilkierQuartzPlugin`.


## License
This project is made available under the MIT license. See [LICENSE](LICENSE) for details.
