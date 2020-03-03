using BungieAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GlickoRank.Helpers
{
    public class FetchDatabase
    {
        public static void UpdateDatabase()
        {
            foreach(Models.Character character in )
            {

            }
        }

        public static void GetCharacter()
        {
            string baseUri = new Uri("https://www.bungie.net/Platform/Destiny2/1/Profile/4611686018470345232/?components=200").ToString();
            WebRequest request = WebRequest.Create(baseUri);
            request.Headers.Add("x-api-key", "182e7c2874274c45a694c1e8f26744d1");
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();

                    JObject jsonResponse = JObject.Parse(responseFromServer);


                    JEnumerable<JToken> results = jsonResponse["Response"]["characters"]["data"].Children();
                    Dictionary<string, JToken> results2 = results.ToDictionary(k => ((JProperty)k).Name);
                    Character result3 = ((JProperty)results2.First().Value).Value.ToObject<Character>();
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}
