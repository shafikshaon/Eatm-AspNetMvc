using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eatm.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public int AccountId { get; set; }
        public int Amount { get; set; }
        public DateTime DateTime { get; set; }
    }
}