using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GoldAggregator.Parser.Services
{
    public class HttpProxyClient : IHttpClient
    {
        public async Task<string> GetAsync(string url)
        {
            // https://hidemy.name/ru/proxy-list/?type=s#list

            var proxyHost = "181.176.211.168";
            var proxyPort = "8080";

            var proxyUserName = "";
            var proxyPassword = "";

            // First create a proxy object
            var proxy = new WebProxy
            {
                Address = new Uri($"http://185.46.212.41:10587"),
                BypassProxyOnLocal = false,
                UseDefaultCredentials = false,

                // *** These creds are given to the proxy server, not the web server ***
                Credentials = CredentialCache.DefaultCredentials
                //Credentials = new NetworkCredential(
                //    userName: proxyUserName,
                //    password: proxyPassword)
            };

            // Now create a client handler which uses that proxy
            var httpClientHandler = new HttpClientHandler
            {
                Proxy = proxy,
                UseProxy = true,
                UseDefaultCredentials = true,
            };

            // Omit this part if you don't need to authenticate with the web server:
            var needServerAuthentication = false;
            if (needServerAuthentication)
            {
                var serverUserName = "";
                var serverPassword = "";

                httpClientHandler.PreAuthenticate = true;
                httpClientHandler.UseDefaultCredentials = false;

                // *** These creds are given to the web server, not the proxy server ***
                httpClientHandler.Credentials = new NetworkCredential(
                    userName: serverUserName,
                    password: serverPassword);
            }

            // Finally, create the HTTP client object
            var client = new HttpClient(handler: httpClientHandler, disposeHandler: true);

            var timeout = Task.Delay(300);
            var request = client.GetStringAsync(url);

            var task = await Task.WhenAny(timeout, request);
            if (task == request)
            {
                // TODO
                return request.Result;
            }
            else
            {
                // TimeOut
            }

            return await client.GetStringAsync(url);
        }
    }
}
