using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.WPF
{
    public class LogService
    {
        private readonly HttpRestClient client;
        public LogService(HttpRestClient _client)
        {
            client= _client;
        }

        public void TestFunc()
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.GET;
            request.Route = $"api/Test/TestFunc";
            var r= client.ExecuteAsync(request).Result;
        }
    }
}
