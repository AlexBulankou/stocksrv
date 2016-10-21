namespace StockServer
{
    using Microsoft.ApplicationInsights.Channel;
    using Microsoft.ApplicationInsights.DataContracts;
    using Microsoft.ApplicationInsights.Extensibility;
    using System.Web;


    public class OperationIdTelemetryInitializer : ITelemetryInitializer
    {
        public void Initialize(ITelemetry telemetry)
        {
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                string requestIdFromHeader = HttpContext.Current.Request.Headers["x-ms-request-id"];
                telemetry.Context.Operation.Id = requestIdFromHeader;
                telemetry.Context.Operation.ParentId = requestIdFromHeader;

                var requestTelemetry = telemetry as RequestTelemetry;
                if (requestTelemetry != null)
                {
                    requestTelemetry.Id = requestIdFromHeader;
                }
            }
        }
    }
}