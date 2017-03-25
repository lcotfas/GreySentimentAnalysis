using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASE.GreySentimentAnalysis.Web.Models
{
	public class AnalysisViewModel
	{
		public string Tweet { get; set; }

		public GreyTweetAnalysisResult AnalysisResult { get; set; }
	}
}