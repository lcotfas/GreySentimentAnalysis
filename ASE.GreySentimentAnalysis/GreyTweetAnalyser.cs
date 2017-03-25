using System.Collections.Generic;
using System.IO;

namespace ASE.GreySentimentAnalysis
{
	public class GreyTweetAnalyser
	{
		private readonly Dictionary<string, GreyNumber> _greySentimentLexiconDictionary;

		public GreyTweetAnalyser()
		{
			_greySentimentLexiconDictionary = new Dictionary<string, GreyNumber>();

			//using (var reader = new StreamReader(File.OpenRead(@"Data/GreySentimentLexicon.csv")))
			//{

			using (var reader = new StreamReader(typeof(GreyTweetAnalyser).Assembly.GetManifestResourceStream("ASE.GreySentimentAnalysis.Data.GreySentimentLexicon.csv")))
			{
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					var values = line.Split(',');

					var word = values[0];
					var low = double.Parse(values[4]);
					var high = double.Parse(values[5]);
					var greyNumber = new GreyNumber(low, high);

					_greySentimentLexiconDictionary.Add(word, greyNumber);
				}
			}
		}

		public GreyTweetAnalysisResult CalculateTweetPerception(string tweet)
		{
			var annotations = new List<Annotation>();
			var boosterWords = new List<string> {"amazingly", "amaizing", "very"};

			var words = tweet.Split(' ');

			var tweetGreyScore = new GreyNumber(0, 0);
			foreach (var word in words)
			{
				if (_greySentimentLexiconDictionary.ContainsKey(word))
				{
					var wordGreyScore = _greySentimentLexiconDictionary[word];
					tweetGreyScore.Low += wordGreyScore.Low;
					tweetGreyScore.High += wordGreyScore.High;

					annotations.Add(new SentimentAnnotation(word, wordGreyScore));
				}
				if (boosterWords.Contains(word))
				{
					annotations.Add(new BoosterAnnotation(word));
				}
				else
				{
					annotations.Add(new Annotation(word));
				}
			}

			return new GreyTweetAnalysisResult(tweet, tweetGreyScore, annotations);
		}
	}
}
