using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GlickoRank.Helpers
{
    public class ApiHelper
    {
        public static string GetRequest(string requestUri)
        {
            //string baseUri = new Uri($"https://www.bungie.net/Platform/Destiny2/{character.MembershipType}/Account/{character.MembershipId}/Character/{character.CharacterId}/Stats/Activities/?mode=19&count=10").ToString();
            WebRequest request = WebRequest.Create(requestUri);
            request.Headers.Add("x-api-key", "182e7c2874274c45a694c1e8f26744d1");
            request.Headers.Add(HttpRequestHeader.UserAgent, "GlickoRank/0.1 AppId/36382 (+www.example.com;majkpascal@outlook.com)");
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    return responseFromServer;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }
    }
}
