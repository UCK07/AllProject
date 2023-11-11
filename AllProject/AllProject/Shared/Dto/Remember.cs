using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllProject.Shared.Models
{
	public  class Remember
	{

		public class Kelime
		{
			public string anlam { get; set; }
			public List<Ornek> ornek { get; set; }
			public List<Ozellik> ozellik { get; set; }
		}

		public class Madde
		{
			public string kelime_say { get; set; }
			public List<Kelime> kelime { get; set; }
		}

		public class Ornek
		{
			public string ornek { get; set; }
			public string yazar { get; set; }
		}

		public class Ozellik
		{
			public string tamAd { get; set; }
			public string ksaAd { get; set; }
		}

		public class Result
		{
			public Madde madde { get; set; }
		}

		public class Root
		{
			public bool success { get; set; }
			public List<Result> result { get; set; }
		}
	}
}
