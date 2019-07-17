using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jupyter_code_generator.Models;
using System.Net.Http;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace jupyter_code_generator.Utils
{
    public class ResponseParser
    {
        public ResponseParser()
        {

        }
        public static async Task<List<Container>> parseContainers(HttpContent containerResponseContent)
        {
            //List<Container> containers = new List<Container>();
            //var rawContentString = await containerResponseContent.ReadAsStringAsync();
            var contentStream = await containerResponseContent.ReadAsStreamAsync();
            var streamReader = new StreamReader(contentStream);
            var jsonReader = new JsonTextReader(streamReader);
            var serializer = new JsonSerializer();
            List<Container> containers = serializer.Deserialize<List<Container>>(jsonReader);
            Debug.WriteLine(containers + "\n\n\n", "\n\n\nContainer response\n");

            return containers;
        }

        public static async Task<List<AccessShared>> parseAccessesShared(HttpContent accessResponseContent)
        {
            string contentString = await accessResponseContent.ReadAsStringAsync();
            AccessSharingResponse responseObj = JsonConvert.DeserializeObject<AccessSharingResponse>(contentString);
            List<AccessShared> accessesShared = responseObj.results;
            return accessesShared;
        }

        public static async Task<string> parseUserId(HttpContent userIdResponseContent)
        {
            string contentString = await userIdResponseContent.ReadAsStringAsync();
            dynamic parsed = JsonConvert.DeserializeObject(contentString);
            string userId = (string)parsed.userId;
            return userId;
        }

        public static async Task<string> parseSasKey(HttpContent sasKeyResponseContent)
        {
            string contentString = await sasKeyResponseContent.ReadAsStringAsync();
            Debug.WriteLine("\n\n\n" + contentString + "\n\n\n");
            dynamic parsed = JsonConvert.DeserializeObject(contentString);
            string sasKey = (string)parsed.fullKey;
            return sasKey;
        }
    }
}
