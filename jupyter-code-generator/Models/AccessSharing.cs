using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jupyter_code_generator.Models
{

    public class AccessSharingResponse
    {
        public List<AccessShared> results { get; set; }
        public int page { get; set; }
        public int resultsPerPage { get; set; }
        public int totalPages { get; set; }
        public int totalResults { get; set; }
    }
    public class AccessShared
    {
        public string userId { get; set; }
        public string ownerId { get; set; }
        public string grantedById { get; set; }
        public string accessSharingId { get; set; }
        public bool keyCreated { get; set; }
        public bool autoRefreshed { get; set; }
        public DateTime keyCreatedTimeUTC { get; set; }
        public DateTime keyExpiryTimeUTC { get; set; }
        public string resourceType { get; set; }
        public int accessHours { get; set; }
        public string accessKeyTemplateId { get; set; }
        //read
        public bool attribute1 { get; set; }
        //write
        public bool attribute2 { get; set; }
        //Delete
        public bool attribute3 { get; set; }
        //List
        public bool attribute4 { get; set; }
        public string resourceId { get; set; }
        public IpRange ipRange { get; set; }
        public string comment { get; set; }
    }

    public class IpRange
    {
        public string startIp { get; set; }
        public string endIp { get; set; }
    }
}
