﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherReal.Model
{
    public class SmtpSyncMessage :EtherRealEntity
    {
        public SmtpSync Sync { get; set; }
        public string EmailAddress { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
