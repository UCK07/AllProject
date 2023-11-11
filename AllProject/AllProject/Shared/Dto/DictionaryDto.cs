using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllProject.Shared.Models
{
    public class DictionaryDto
    {
  


        public class Result
        {
            public string madde { get; set; }
            public string tamAd { get; set; }
            public string ksaAd { get; set; }
            public string ornek { get; set; }
            public string yazar { get; set; }
            public string kelime_say { get; set; }
            public string kelime { get; set; }
            public string anlam { get; set; }
            public string ozellik { get; set; }
        }

        public class Root
        {
            public bool success { get; set; }
            public List<Result> result { get; set; }
        }

    }
}
