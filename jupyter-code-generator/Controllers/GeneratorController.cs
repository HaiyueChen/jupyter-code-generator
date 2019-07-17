using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using System.Diagnostics;
using jupyter_code_generator.Repositories;
using Microsoft.WindowsAzure.Storage.Blob;
using jupyter_code_generator.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace jupyter_code_generator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneratorController : ControllerBase
    {

        private DataApiRepo _dataApiRepo;
        private AzureblobStorageRepo _azureblobStorageRepo;
        public GeneratorController(DataApiRepo dataApiRepo, AzureblobStorageRepo azureblobStorageRepo)
        {
            _dataApiRepo = dataApiRepo;
            _azureblobStorageRepo = azureblobStorageRepo;
        }
        // GET: api/Generator
        [HttpGet]
        [Route("")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Generator/5
        [HttpGet("{resourceReferrence}", Name = "Get")]
        //[Route("api/Generator")]
        [Authorize]
        public async Task<string> Get(string resourceReferrence)
        {
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId").Value;
            string oAuthToken = Startup.userTokenCache[userId];
            string resourceId = resourceReferrence.Substring(resourceReferrence.Length - Guid.NewGuid().ToString().Length);
            string sasKey = await _dataApiRepo.GetSasKey(oAuthToken, resourceId);
            DirectoryTree tree = await _azureblobStorageRepo.GetblobsInContainer(sasKey);


            string treeInStringFormat = JsonConvert.SerializeObject(tree);
            Debug.WriteLine("\n\n\n\n"+treeInStringFormat+"\n\n\n\n");
            return treeInStringFormat;
        }

        //POST: api/Generator
        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<string> Post([FromBody] GenerateFileRequest request)
        {
            string fileName = "jupyter_template.ipynb";
            string userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId").Value;
            string oAuthToken = Startup.userTokenCache[userId];
            string containerReferrence = request.containerReferrence;
            string resourceId = containerReferrence.Substring(containerReferrence.Length - Guid.NewGuid().ToString().Length);
            //string containerName = containerReferrence.Substring(0, containerReferrence.Length - resourceId.Length);
            string containerName = containerReferrence;

            Task<string> sasKeyPromise = _dataApiRepo.GetSasKey(oAuthToken, resourceId);

            //Build python list string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < request.files.Count; i++)
            {
                builder.Append("\\");
                builder.Append("\"");
                builder.Append(request.files[i]);
                builder.Append("\\");
                builder.Append("\"");
                builder.Append(",");
            }
            builder.Append("\\");
            builder.Append("\"");
            builder.Append(request.files[request.files.Count - 1]);
            builder.Append("\\");
            builder.Append("\"");
            string azurePaths = builder.ToString();

            using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8))
            {

                string fileContent = reader.ReadToEnd();

                string fullSasKey = await sasKeyPromise;
                Debug.WriteLine($"\n\n{fullSasKey}\n\n");

                string justTheSasKey = fullSasKey.Split('?')[1];
                Debug.WriteLine($"\n\n{justTheSasKey}\n\n");

                string url = fullSasKey.Split('?')[0];
                Debug.WriteLine($"\n\n{url}\n\n");

                string domain = url.Split('.')[0];
                Debug.WriteLine($"\n\n{domain}\n\n");

                string accountName = domain.Split('/')[2];
                Debug.WriteLine($"\n\n{accountName}\n\n");

                fileContent = Regex.Replace(fileContent, "{account_name}", accountName);
                fileContent = Regex.Replace(fileContent, "{container_name}", containerName);
                fileContent = Regex.Replace(fileContent, "{sas_token}", justTheSasKey);
                fileContent = Regex.Replace(fileContent, "{file_paths}", azurePaths);
                string fileResponse = JsonConvert.SerializeObject(
                            new
                            {
                                fileName = fileName,
                                fileContent = fileContent
                            });
                return fileResponse;
            }
        }


           

        //// PUT: api/Generator/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/GeneratorWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
