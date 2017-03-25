using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASE.GreySentimentAnalysis.UnitTest
{
	[TestClass]
	public class GreyTweetAnalyserUnitTest
	{
		private readonly GreyTweetAnalyser _greyTweetAnalyser;

		public GreyTweetAnalyserUnitTest()
		{
			_greyTweetAnalyser = new GreyTweetAnalyser();
		}

		[TestMethod]
		public void PositiveTweetTest()
		{
			var greyPerception = _greyTweetAnalyser.CalculateTweetPerception("The new iPhone is good");
			Assert.IsTrue(greyPerception.GreyPerception.Low > 0);
		}

		[TestMethod]
		public void NegativeTweetTest()
		{
			var greyPerception = _greyTweetAnalyser.CalculateTweetPerception("I am very upset and disappointed by this iphone update failed backup. @iphone");
			Assert.IsTrue(greyPerception.GreyPerception.Low > 0);
		}
	}
}
