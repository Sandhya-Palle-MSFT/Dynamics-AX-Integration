using AuthenticationUtility;
using Microsoft.OData.Client;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ODataUtility.Microsoft.Dynamics.DataEntities;
using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.IO;
using System.Xml.Linq;



namespace ODataConsoleApplication
{
    class Program
    {
        public static string ODataEntityPath = ClientConfiguration.Default.UriString + "data";

        static void Main(string[] args)
        {
            HttpClient httpClient = GetHttpClient();
            RunSalesInvoiceReport(httpClient);
            RunFreeTextInvoiceReport(httpClient);
            RunProjectInvoiceReport(httpClient);
            RunCustomerStatementReport(httpClient);
        }

        private static void RunSalesInvoiceReport(HttpClient httpClient)
        {
            Uri requestUri = new Uri(new Uri("https://usnconeboxax1aos.cloud.onebox.dynamics.com"), "data/SrsFinanceCopilots/Microsoft.Dynamics.DataEntities.RunCopilotReport");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUri.AbsoluteUri);
            string reportParameterValue = JsonConvert.SerializeObject(new
            {
                parmRecordId = "5637144580"
            });
            string argParameterValue = JsonConvert.SerializeObject(
                new
                {
                    DataTableName = "CustInvoiceJour",
                    DataTableFieldName = "RECID",
                    DataTableFieldValue = "5637144580"
                });
            request.Content = new StringContent(
                            JsonConvert.SerializeObject(
                                new
                                {
                                    _legalEntityName = "USMF",
                                    _controllerName = "SalesInvoiceController",
                                    _controllerArgsJson = argParameterValue,
                                    _contractName = "SalesInvoiceContract",
                                    _reportParameterJson = reportParameterValue
                                }), Encoding.UTF8, "application/json");

            HttpResponseMessage response = httpClient.SendAsync(request).GetAwaiter().GetResult();
            var reportPdf = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            var pdfBytes = Convert.FromBase64String(reportPdf?["value"]?.ToString());
            File.WriteAllBytes(@"C:\temp\SalesInvoice.pdf", pdfBytes);
        }

        private static void RunFreeTextInvoiceReport(HttpClient httpClient)
        {
            Uri requestUri = new Uri(new Uri("https://usnconeboxax1aos.cloud.onebox.dynamics.com"), "data/SrsFinanceCopilots/Microsoft.Dynamics.DataEntities.RunCopilotReport");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUri.AbsoluteUri);
            string reportParameterValue = JsonConvert.SerializeObject(new
            {
                parmCustInvoiceJourRecId = "68719491600"
            });
            string argParameterValue = JsonConvert.SerializeObject(
                new
                {
                    DataTableName = "CustInvoiceJour",
                    DataTableFieldName = "RECID",
                    DataTableFieldValue = "68719491600"
                });
            request.Content = new StringContent(
                            JsonConvert.SerializeObject(
                                new
                                {
                                    _legalEntityName = "USMF",
                                    _controllerName = "FreeTextinvoiceController",
                                    _controllerArgsJson = argParameterValue,
                                    _contractName = "FreeTextInvoiceContract",
                                    _reportParameterJson = reportParameterValue
                                }), Encoding.UTF8, "application/json");

            HttpResponseMessage response = httpClient.SendAsync(request).GetAwaiter().GetResult();
            var reportPdf = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            var pdfBytes = Convert.FromBase64String(reportPdf?["value"]?.ToString());
            File.WriteAllBytes(@"C:\temp\FreeTextInvoice.pdf", pdfBytes);
        }

        private static void RunProjectInvoiceReport(HttpClient httpClient)
        {
            Uri requestUri = new Uri(new Uri("https://usnconeboxax1aos.cloud.onebox.dynamics.com"), "data/SrsFinanceCopilots/Microsoft.Dynamics.DataEntities.RunCopilotReport");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUri.AbsoluteUri);
            string reportParameterValue = JsonConvert.SerializeObject(new
            {
                parmProjInvoiceJourRecId = "5637146253"
            });
            string argParameterValue = JsonConvert.SerializeObject(
                new
                {
                    DataTableName = "ProjInvoiceJour",
                    DataTableFieldName = "RECID",
                    DataTableFieldValue = "5637146253"
                });
            request.Content = new StringContent(
                            JsonConvert.SerializeObject(
                                new
                                {
                                    _legalEntityName = "USMF",
                                    _controllerName = "PSAProjAndContractInvoiceController",
                                    _controllerArgsJson = argParameterValue,
                                    _contractName = "PSAProjInvoiceContract",
                                    _reportParameterJson = reportParameterValue
                                }), Encoding.UTF8, "application/json");

            HttpResponseMessage response = httpClient.SendAsync(request).GetAwaiter().GetResult();
            var reportPdf = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            var pdfBytes = Convert.FromBase64String(reportPdf?["value"]?.ToString());
            File.WriteAllBytes(@"C:\temp\ProjectInvoice.pdf", pdfBytes);
        }

        private static void RunCustomerStatementReport(HttpClient httpClient)
        {
            Uri requestUri = new Uri(new Uri("https://usnconeboxax1aos.cloud.onebox.dynamics.com"), "data/SrsFinanceCopilots/Microsoft.Dynamics.DataEntities.RunCopilotReport");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUri.AbsoluteUri);
            string reportParameterValue = JsonConvert.SerializeObject(new
            {
                parmCustAccount = "US-008"
            });
            string argParameterValue = JsonConvert.SerializeObject(
                new
                {
                    DataTableName = "CustTable",
                    DataTableFieldName = "ACCOUNTNUM",
                    DataTableFieldValue = "US-008"
                });
            request.Content = new StringContent(
                            JsonConvert.SerializeObject(
                                new
                                {
                                    _legalEntityName = "USMF",
                                    _controllerName = "CustAccountStatementExtController",
                                    _controllerArgsJson = argParameterValue,
                                    _contractName = "CustAccountStatementExtContract",
                                    _reportParameterJson = reportParameterValue
                                }), Encoding.UTF8, "application/json");

            HttpResponseMessage response = httpClient.SendAsync(request).GetAwaiter().GetResult();
            var reportPdf = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            var pdfBytes = Convert.FromBase64String(reportPdf?["value"]?.ToString());
            File.WriteAllBytes(@"C:\temp\CustomerStatement.pdf", pdfBytes);
        }
        private static HttpClient GetHttpClient()
        {
            HttpClient httpClient = new HttpClient();
            var authenticationHeader = OAuthHelper.GetAuthenticationHeader(useWebAppAuthentication: false);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authenticationHeader);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }
    }
}
