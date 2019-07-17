using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace jupyter_code_generator.Models
{
    public class Container
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "reference")]
        public string reference { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string url { get; set; }

        [JsonProperty(PropertyName = "metaData")]
        public Metadata metaData { get; set; }

        [JsonProperty(PropertyName = "lastModifiedUTC")]
        public DateTime lastModifiedUTC { get; set; }

        [JsonProperty(PropertyName = "creationDateTimeUTC")]
        public DateTime creationDateTimeUTC { get; set; }

        [JsonProperty(PropertyName = "ownerId")]
        public string ownerId { get; set; }

        [JsonProperty(PropertyName = "accessLevel")]
        public string accessLevel { get; set; }

        [JsonProperty(PropertyName = "region")]
        public string region { get; set; }

        [JsonProperty(PropertyName = "keyStatus")]
        public string keyStatus { get; set; }

        [JsonProperty(PropertyName = "mayContainPersonalData")]
        public string mayContainPersonalData { get; set; }


        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\n\n");
            builder.Append($"id: {id}\n");
            builder.Append($"reference: {reference}\n");
            builder.Append($"url: {url}\n");
            builder.Append($"metaData: {metaData}\n");
            builder.Append($"lastModifiedUtc: {lastModifiedUTC}\n");
            builder.Append($"creationDateTimeUtc: {creationDateTimeUTC}\n");
            builder.Append($"ownerId: {ownerId}\n");
            builder.Append($"accessLevel: {accessLevel}\n");
            builder.Append($"region: {region}\n");
            builder.Append($"keyStatus: {keyStatus}\n");
            builder.Append($"personalData: {mayContainPersonalData}\n");
            builder.Append("\n\n");
            return builder.ToString();
        }
    }

    public class Metadata
    {
        [JsonProperty(PropertyName = "title")]
        public string title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string description { get; set; }

        [JsonProperty(PropertyName = "icon")]
        public Icon icon { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public List<Tag> tags { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{\n");
            builder.Append($"title: {title}, \n");
            builder.Append($"desc: {description}, \n");
            builder.Append($"icon: {icon}, \n");
            builder.Append($"tags: [\n");
            tags.ForEach(t =>
           {
               builder.Append($"{t}, ");
           });
            builder.Append($"]\n");
            builder.Append("}");
            return builder.ToString();
        }
    }

    public class Icon
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        [JsonProperty(PropertyName = "backgroundColor")]
        public string backgroundColor { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\n{");
            builder.Append($"id: {id}, ");
            builder.Append($"backgroundColor: {backgroundColor}");
            builder.Append("}");
            return builder.ToString();
        }
    }
    
    public class Tag
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string title { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\n{");
            builder.Append($"id: {id}, ");
            builder.Append($"title: {title}");
            builder.Append("}");
            return builder.ToString();
        }
    }
}
