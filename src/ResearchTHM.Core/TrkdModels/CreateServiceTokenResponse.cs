using System;
using System.Collections.Generic;
using System.Text;

namespace ResearchTHM.Core.TrkdModels
{
    public class CreateServiceTokenResponse1
    {
        public DateTime Expiration { get; set; }
        public string Token { get; set; }
    }

    public class CreateServiceTokenResponse
    {
        public CreateServiceTokenResponse1 CreateServiceToken_Response_1 { get; set; }
    }
}
