using System.Collections.Generic;

namespace ASE.GreySentimentAnalysis
{
	public class GreyTweetAnalysisResult
	{
		#region Attributes
		public string Tweet { get; set; }
		public GreyNumber GreyPerception { get; set; }
		public List<Annotation> Annotations { get; set; }
		#endregion

		public GreyTweetAnalysisResult(string tweet, GreyNumber greyPerception, List<Annotation> annotations)
		{
			Tweet = tweet;
			GreyPerception = greyPerception;
			Annotations = annotations;
		}
	}
}
