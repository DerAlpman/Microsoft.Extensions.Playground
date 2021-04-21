using System;
using System.ComponentModel.DataAnnotations;

namespace DataClasses
{
    public class TransientFaultHandlingOptions
    {
        public bool Enabled { get; set; }

        [RegularExpression(@"^\d{2}:\d{2}:\d{2}$")]
        public TimeSpan AutoRetryDelay { get; set; }
    }
}
