namespace ASE.GreySentimentAnalysis
{
	public class Annotation
	{
		#region Attributes
		public string Word { get; set; }
		#endregion

		public Annotation(string word)
		{
			Word = word;
		}
	}
	public class BoosterAnnotation : Annotation
	{
		public BoosterAnnotation(string word) : base(word)
		{
		}
	}

	public class SentimentAnnotation: Annotation
	{
		#region Attributes
		public GreyNumber GreyPerception { get; set; }
		#endregion

		public SentimentAnnotation(string word, GreyNumber greyPerception)
			:base(word)
		{
			GreyPerception = greyPerception;
		}
	}
}
