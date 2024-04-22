using AutoMapper.Execution;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json.Linq;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Transactions;
using System;
using System.Net.Http;
using Azure.Core;
using System.Data.Common;
using System.Security.Cryptography.Xml;

namespace ClubWebsite.Models.Payment
{
    public class BkashSearchTransactionReturnModel
    {
        public string amount { get; set; }
        public string completedTime { get; set; }
        public string currency { get; set; }
        public string customerMsisdn { get; set; }
        public string initiationTime { get; set; }
        public string organizationShortCode { get; set; }
        public string transactionReference { get; set; }
        public string transactionStatus { get; set; }
        public string transactionType { get; set; }
        public string trxID { get; set; }
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
    }
}
