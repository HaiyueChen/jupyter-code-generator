using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jupyter_code_generator.Models
{
    public class GenerateFileRequest
    {

        public string containerReferrence { get; set; }
        public List<string> files { get; set; }
    }
}
