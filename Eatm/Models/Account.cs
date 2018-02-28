using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eatm.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int CardNumber { get; set; }
        public int PinNumber { get; set; }
        public int Balance { get; set; }
    }
}