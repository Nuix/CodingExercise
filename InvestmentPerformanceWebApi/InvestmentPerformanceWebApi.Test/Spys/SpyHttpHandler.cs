using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace InvestmentPerformanceWebApi.Test.Spys;

internal class SpyHttpHandler : HttpMessageHandler
{
    public List<HttpResponse> ResponseMap { get; set; } = new List<HttpResponse>();
    public List<HttpRequestMessage> Requests { get; set; } = new List<HttpRequestMessage>();

    public void ClearSetup()
    {
        ResponseMap.Clear();
    }

    public void Setup(string url, string response, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        ResponseMap.Add(new HttpResponse
        {
            Url = url,
            Response = response,
            StatusCode = statusCode
        });
    }

    public string DefaultResponse { get; set; } = "";
    public HttpStatusCode DefaultStatusCode { get; set; } = HttpStatusCode.OK;


    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Requests.Add(request);

        var response = new HttpResponseMessage();
        foreach (var map in ResponseMap)
        {
            if (map.Url == request.RequestUri?.ToString())
            {
                response.Content = new StringContent(map.Response);
                response.StatusCode = map.StatusCode;
                return Task.FromResult(response);
            }
        }

        // Else we return the default
        response.Content = new StringContent(DefaultResponse);
        response.StatusCode = DefaultStatusCode;
        return Task.FromResult(response);
    }
}

internal class HttpResponse
{
    public string Url { get; set; } = "";
    public string Response { get; set; } = "";
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
}
