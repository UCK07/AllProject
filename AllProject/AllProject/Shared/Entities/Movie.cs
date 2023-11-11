using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllProject.Shared.Entities
{
    [BsonCollection("Movie")]
    public class Movie : Document
    {
        public string MovieName { get; set; }
        public int MovieYear { get; set; }
        //public MovieComment? MovieComment { get; set; }

        public string ImageUrl { get; set; }
        public string ImageBase64 { get; set; }

        public string CloudUrl { get; set; }




    }
}
