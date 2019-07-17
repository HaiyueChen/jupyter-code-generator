using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace jupyter_code_generator.Models
{
    public class DirectoryTree
    {
        public List<Node> _roots { get; set; }

        public DirectoryTree()
        {
            _roots = new List<Node>();
        }

        public Node AddNode(IListBlobItem content)
        {
            Node newNode = new Node(content);
            _roots.Add(newNode);
            return newNode;
        }
    }


    public class Node
    {
        public List<Node> _children { get; set; }
        public IListBlobItem _content { get; set; }


        public Node(IListBlobItem content)
        {
            _content = content;
            _children = new List<Node>();
        }

        public Node AddChild(IListBlobItem childContent)
        {

            Node newNode = new Node(childContent);
            this._children.Add(newNode);
            //if (childContent  is ICloudBlob)
            //{
            //    Debug.WriteLine("\n\n\n\n" + $"Adding {((ICloudBlob)childContent).Name} to {((CloudBlobDirectory)_content).Prefix}" + "\n\n\n");
            //}
            //Debug.WriteLine("\n\n\n" + $"Adding {((ICloudBlob)childContent).Name} to {((CloudBlobDirectory)_content).Prefix}" + "\n\n\n");
            return newNode;
        }


    }


}
