using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jupyter_code_generator.Models;
using jupyter_code_generator.Utils;
using System.Net.Http;
using Microsoft.Extensions.Options;
using jupyter_code_generator.Errors;


namespace jupyter_code_generator.Repositories
{
    public class DataApiRepo
    {

        private AzureAdB2COptions _azureAdB2COptions;
        private HttpClient _httpClient;
        public DataApiRepo(IOptions<AzureAdB2COptions> azureB2COptionsInjection, HttpClient httpClient)
        {
            this._azureAdB2COptions = azureB2COptionsInjection.Value;
            this._httpClient = httpClient;
        }

        public async Task<List<Container>> GetContainers(string accessToken)
        {

            string endpoint = "/resources";
            string subscriptionKey = _azureAdB2COptions.DataApiSubscriptionKey;
            Uri request_uri = new Uri(_azureAdB2COptions.DataApiUrl + endpoint);

            HttpRequestMessage request =
                new HttpRequestMessage(
                    method: HttpMethod.Get,
                    requestUri: request_uri
                );
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            HttpContent content = response.Content;
            List<Container> containers = await ResponseParser.parseContainers(content);

            return containers;
        }

        public async Task<string> GetSasKey(string accessToken, string resourceId)
        {
            string accessSharingId = await GetAccessSharingId(accessToken, resourceId);
            string endpoint = $"/resources/{resourceId}/accesses/{accessSharingId}/key";
            string subscriptionKey = _azureAdB2COptions.DataApiSubscriptionKey;
            Uri request_uri = new Uri(_azureAdB2COptions.DataApiUrl + endpoint);

            HttpRequestMessage request =
                new HttpRequestMessage(
                    method: HttpMethod.Put,
                    requestUri: request_uri
                );
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            HttpContent content = response.Content;
            string sasKey = await ResponseParser.parseSasKey(content);
            return sasKey;
        }


        public async Task<string> GetAccessSharingId(string accessToken, string resourceId)
        {
            Task<string> taskUserId = GetUserId(accessToken);
            Task<List<AccessShared>> taskAccessesShared = GetAccessSharedToResource(accessToken, resourceId);

            string userId = await taskUserId;
            List<AccessShared> accessesShared = await taskAccessesShared;

            if (accessesShared.Count < 1)
            {
                throw new NoAccessSharedExeption("No access was shared for the current user");
            }

            List<AccessShared> readAccessesSharedWithMe =
                accessesShared.Where(
                    access =>
                        access.userId.Equals(userId)
                        && access.attribute1).ToList();
            List<AccessShared> accessSortedByDate = readAccessesSharedWithMe.OrderByDescending(access => access.keyExpiryTimeUTC).ToList();
            string accessSharingId = accessSortedByDate.ElementAt(0).accessSharingId;
            return accessSharingId;
        }

        public async Task<List<AccessShared>> GetAccessSharedToResource(string accessToken, string resourceId)
        {
            string endpoint = $"/resources/{resourceId}/accesses";
            string subscriptionKey = _azureAdB2COptions.DataApiSubscriptionKey;
            Uri request_uri = new Uri(_azureAdB2COptions.DataApiUrl + endpoint);

            HttpRequestMessage request = new HttpRequestMessage(
                    method: HttpMethod.Get,
                    requestUri: request_uri
                );
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            HttpContent content = response.Content;
            List<AccessShared> accessesShared = await ResponseParser.parseAccessesShared(content);
            return accessesShared;
        }



        public async Task<string> GetUserId(string accessToken)
        {
            string endpoint = "/users/me";
            string subscriptionKey = _azureAdB2COptions.DataApiSubscriptionKey;
            Uri request_uri = new Uri(_azureAdB2COptions.DataApiUrl + endpoint);

            HttpRequestMessage request = new HttpRequestMessage(
                    method: HttpMethod.Get,
                    requestUri: request_uri
                );
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            HttpContent content = response.Content;
            string userId = await ResponseParser.parseUserId(content);
            return userId;
        }

    }



}
