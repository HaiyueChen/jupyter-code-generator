using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Diagnostics;
using jupyter_code_generator.Models;

namespace jupyter_code_generator.Repositories
{
    public class AzureblobStorageRepo
    {
        private HttpClient _httpClient;


        public AzureblobStorageRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<DirectoryTree> GetblobsInContainer(string sasKey)
        {
            CloudBlobContainer container = new CloudBlobContainer(new Uri(sasKey));
            BlobContinuationToken continuationToken = null;
            //List<IListBlobItem> blobItems = new List<IListBlobItem>();
            DirectoryTree tree = new DirectoryTree();
            do
            {
                BlobResultSegment blobSegment = await container.ListBlobsSegmentedAsync(continuationToken);
                continuationToken = blobSegment.ContinuationToken;
                foreach (IListBlobItem item in blobSegment.Results)
                {
                    Node newRoot = tree.AddNode(item);
                    if (item is CloudBlobDirectory)
                    {
                        Debug.WriteLine($"{((CloudBlobDirectory)item).Prefix} is a directory");
                        //directories.Add((CloudBlobDirectory)item);
                        await RecursiveFolderSearch(newRoot, (CloudBlobDirectory)item);
                    }
                }
            }
            while (continuationToken != null);

{}          return tree;
        }

        private async Task<bool> RecursiveFolderSearch(Node directoryNode, CloudBlobDirectory directory)
        {
            BlobContinuationToken continuationToken = null;
            do
            {
                Debug.WriteLine($"\n\nRecursive looking in: {((CloudBlobDirectory)directory).Prefix}\n\n");
                BlobResultSegment blobSegment = await directory.ListBlobsSegmentedAsync(continuationToken);
                continuationToken = blobSegment.ContinuationToken;
                //Debug.WriteLine("\n\n\n" + $"{directory.Prefix}    Items: {blobSegment.Results.ToList().Count}" + "\n\n\n");
                foreach (IListBlobItem item in blobSegment.Results)
                {
                    Node newNode = directoryNode.AddChild(item);
                    if (item is CloudBlobDirectory)
                    {
                        Debug.WriteLine($"\n\n{((CloudBlobDirectory)item).Prefix} is a directory\n\n");
                        await RecursiveFolderSearch(newNode, (CloudBlobDirectory)item);
                    }
                }
            } while (continuationToken != null);
            return true;
        }
    }
}
