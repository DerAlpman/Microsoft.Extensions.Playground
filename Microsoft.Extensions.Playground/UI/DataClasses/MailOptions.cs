using System.Collections.Generic;

namespace DataClasses
{
    public class MailOptions
    {
        public string Sender { get; set; }
        public IEnumerable<string> Recipients { get; set; }
    }
}
