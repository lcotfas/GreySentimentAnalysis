using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE.GreySentimentAnalysis
{
	class GreyTweetAnalyser
	{
		private Dictionary<string, GreyNumber> greySentimentLexiconDictionary;

		public GreyTweetAnalyser()
		{
			greySentimentLexiconDictionary = new Dictionary<string, GreyNumber>();

			using (var reader = new StreamReader(File.OpenRead(@"Data/GreySentimentLexicon.csv")))
			{
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					var values = line.Split(',');

					var word = values[0];
					var low = double.Parse(values[4]);
					var high = double.Parse(values[5]);
					var greyNumber = new GreyNumber(low, high);

					greySentimentLexiconDictionary.Add(word, greyNumber);
				}
			}
		}

		public GreyNumber CalculateTweetPerception(string tweet)
		{
			var words = tweet.Split(' ');

			var tweetGreyScore = new GreyNumber(0, 0);
			var foundWords = 0;
			foreach (var word in words)
			{
				if (greySentimentLexiconDictionary.ContainsKey(word))
				{
					foundWords++;

					var wordGreyScore = greySentimentLexiconDictionary[word];
					tweetGreyScore.Low += wordGreyScore.Low;
					tweetGreyScore.High += wordGreyScore.High;
				}
			}

			return tweetGreyScore;
		}
	}
}
