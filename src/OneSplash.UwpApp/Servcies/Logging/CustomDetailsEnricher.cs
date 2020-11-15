using Microsoft.Toolkit.Uwp.Helpers;
using Serilog.Core;
using Serilog.Events;
using System.Threading;

namespace OneSplash.UwpApp.Servcies.Logging
{
    public class CustomDetailsEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("ThreadId", Thread.CurrentThread.ManagedThreadId, true));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("OperatingSystem", SystemInformation.Instance.OperatingSystem,true));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("OperatingSystemArchitecture", SystemInformation.Instance.OperatingSystemArchitecture));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("OperatingSystemVersion", SystemInformation.Instance.OperatingSystemVersion));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("DeviceFamily", SystemInformation.Instance.DeviceFamily));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Version", $"{SystemInformation.Instance.ApplicationVersion.Major}.{SystemInformation.Instance.ApplicationVersion.Minor}.{SystemInformation.Instance.ApplicationVersion.Build}.{SystemInformation.Instance.ApplicationVersion.Revision}"));
        }
    }
}
