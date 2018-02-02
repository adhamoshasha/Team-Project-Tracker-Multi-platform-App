using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    class Api_Connector
    {
        private static CookieContainer cookieContainer = new CookieContainer();
        private static HttpClientHandler clienthandler =
                    new HttpClientHandler
                    {
                        AllowAutoRedirect = true,
                        UseCookies = true,
                        CookieContainer = cookieContainer
                    };
        public static HttpClient client = new HttpClient(clienthandler);

        public static async Task<string> Connect(string url)
        {
            client = new HttpClient(clienthandler)
            {
                //HttpClient client = LoginPage1.client;
                Timeout = new TimeSpan(0, 0, 3)//10 s timeout 
            };
            HttpResponseMessage Api_respons = new HttpResponseMessage();
            try
            {
                //Task t =  Task.Run(() =>//starts a new thread
                //{
               Api_respons =  await client.GetAsync(url);//.Result;

                //});
                //t.Wait();//waits for the thread to finish of times out

                if (Api_respons.IsSuccessStatusCode)//check if the api got input correctly
                {
                    //Respons_text = Api_respons.Content.ReadAsStringAsync().Result.ToString();

                    return await Api_respons.Content.ReadAsStringAsync();
                }
                else
                {

                    return "666";
                }

            }
            catch
            {
                return "999";
            }
        }


    }
}
