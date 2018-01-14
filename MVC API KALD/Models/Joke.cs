using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API_KALD.Models
{
    public class Joke
    {
        public string ID { get; set; }
        public string JokeContent { get; set; }
        public int Votes { get; set; }
        public bool Type { get; set; }
    }
}