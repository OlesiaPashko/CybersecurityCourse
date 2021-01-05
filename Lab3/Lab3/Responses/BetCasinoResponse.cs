using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3.Responses
{
    public class BetCasinoResponse
    {
        public string Message { get; set; }
        public Account Account { get; set; }
        public long RealNumber { get; set; }
    }
}
