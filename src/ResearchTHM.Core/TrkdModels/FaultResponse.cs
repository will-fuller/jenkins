using System;
using System.Collections.Generic;
using System.Text;

namespace ResearchTHM.Core.TrkdModels
{
    public class FaultResponse
    {
        public Fault Fault { get; set; }
    }
    public class Subcode
    {
        public string Value { get; set; }
    }

    public class Code
    {
        public string Value { get; set; }
        public Subcode Subcode { get; set; }
    }

    public class Text
    {
        public string lang { get; set; }
        public string Value { get; set; }
    }

    public class Reason
    {
        public Text Text { get; set; }
    }

    public class ClientErrorReference
    {
        public DateTime Timestamp { get; set; }
        public string ErrorReference { get; set; }
        public string ServerReference { get; set; }
    }

    public class Detail
    {
        public ClientErrorReference ClientErrorReference { get; set; }
    }

    public class Fault
    {
        public Code Code { get; set; }
        public Reason Reason { get; set; }
        public Detail Detail { get; set; }
    }

}
