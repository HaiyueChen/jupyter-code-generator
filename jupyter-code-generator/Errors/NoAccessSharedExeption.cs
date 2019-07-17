using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jupyter_code_generator.Errors
{
public class NoAccessSharedExeption : Exception
    {
        public NoAccessSharedExeption() { }
        public NoAccessSharedExeption(string message) : base(message) { }
        public NoAccessSharedExeption(string message, Exception inner) : base(message, inner) { }
        protected NoAccessSharedExeption(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    
}
