using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jupyter_code_generator
{
    public class AzureAdB2COptions
    {
        public const string PolicyAuthenticationProperty = "Policy";

        public AzureAdB2COptions()
        {
        }

        public string AzureAdB2CInstance { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Tenant { get; set; }
        public string SignUpSignInPolicyId { get; set; }
        public string RedirectUri { get; set; }

        public string DefaultPolicy => SignUpSignInPolicyId;
        public string Authority => $"{AzureAdB2CInstance}/{Tenant}/{DefaultPolicy}/v2.0/";

        public string DataApiUrl { get; set; }
        public string DataApiScope { get; set; }
        public string DataApiSubscriptionKey { get; set; }
    }
}
