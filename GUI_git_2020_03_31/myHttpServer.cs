using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SimpleHttpServer;
using System.Threading;
using SimpleHttpServer.Models;

namespace myHttpServer
{
    static class Routes
    {
        public static List<Route> GET
        {
            get
            {
                return new List<Route>()
                {
                    new Route()
                    {
                        Callable = HomeIndex,
                        UrlRegex = "^\\/$",
                        Method = "GET"
                    },new Route()
                    {
                        Callable = TxIndex,
                        UrlRegex = "^\\/get_from_Tx_data",
                        Method = "GET"
                    },new Route()
                    {
                        Callable = RxIndex,
                        UrlRegex = "^\\/get_from_Rx_data",
                        Method = "GET"
                    }
                };

            }
        }

        private static HttpResponse HomeIndex(HttpRequest request)
        {
            return new HttpResponse()
            {
                ContentAsUTF8 = "Hello",
                ReasonPhrase = "OK Hello",
                StatusCode = "200"
            };
        }
        private static HttpResponse TxIndex(HttpRequest request)
        {
            return new HttpResponse()
            {
                ContentAsUTF8 = "",
                ReasonPhrase = "OK. Information from Tx is sended.",
                StatusCode = "200"
            };
        }
        private static HttpResponse RxIndex(HttpRequest request)
        {
            return new HttpResponse()
            {
                ContentAsUTF8 = "TEST",
                ReasonPhrase = "OK. Information from Rx is sended.",
                StatusCode = "200"
            };
        }

    }
}
