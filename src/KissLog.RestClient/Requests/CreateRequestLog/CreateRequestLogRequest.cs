﻿using KissLog.RestClient.Requests.CreateRequestLog.Http;
using System;
using System.Collections.Generic;

namespace KissLog.RestClient.Requests.CreateRequestLog
{
    public class CreateRequestLogRequest
    {
        public string SdkName { get; set; }
        public string SdkVersion { get; set; }

        public string ClientId { get; set; }
        public string OrganizationId { get; set; }
        public string ApplicationId { get; set; }
        public DateTime StartDateTime { get; set; }
        public double DurationInMilliseconds { get; set; }
        public bool IsNewSession { get; set; }
        public string SessionId { get; set; }
        public string MachineName { get; set; }
        public bool IsAuthenticated { get; set; }
        public User User { get; set; }
        public HttpProperties WebRequest { get; set; }
        public IEnumerable<LogMessage> LogMessages { get; set; }
        public IEnumerable<CapturedException> Exceptions { get; set; }
        public IEnumerable<string> Keywords { get; set; }
        public List<KeyValuePair<string, object>> CustomProperties { get; set; }

        public CreateRequestLogRequest()
        {
            ClientId = Guid.NewGuid().ToString();
            LogMessages = new List<LogMessage>();
            Exceptions = new List<CapturedException>();
            Keywords = new List<string>();
            CustomProperties = new List<KeyValuePair<string, object>>();
        }
    }
}
