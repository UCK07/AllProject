using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllProject.Shared.Models
{
    public class PharmacyDto
    {
        public class Result
        {
            public string name { get; set; }
            public string dist { get; set; }
            public string address { get; set; }
            public string phone { get; set; }
            public string loc { get; set; }
        }

        public class Root
        {
            public bool success { get; set; }
            public List<Result> result { get; set; }
        }
    }
}
