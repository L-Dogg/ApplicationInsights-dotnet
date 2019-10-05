﻿using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;

namespace Microsoft.ApplicationInsights.TestFramework
{
    using System;

    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.ApplicationInsights.Extensibility.Implementation;

    internal class StubPlatform : IPlatform
    {
        public Func<IDebugOutput> OnGetDebugOutput = () => new StubDebugOutput();
        public Func<string> OnReadConfigurationXml = () => null;
        public Func<string> OnGetMachineName = () => null;

        public string ReadConfigurationXml()
        {
            return this.OnReadConfigurationXml();
        }

        public IDebugOutput GetDebugOutput()
        {
            return this.OnGetDebugOutput();
        }

        public bool TryGetEnvironmentVariable(string name, out string value)
        {
            try
            {
                value = Environment.GetEnvironmentVariable(name);
                return !string.IsNullOrEmpty(value);
            }
            catch (Exception e)
            {
                CoreEventSource.Log.FailedToLoadEnvironmentVariables(e.ToString());
                throw;
            }
        }

        public string GetMachineName()
        {
            return this.OnGetMachineName();
        }
    }
}
