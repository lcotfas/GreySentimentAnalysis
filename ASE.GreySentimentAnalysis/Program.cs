using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ASE.GreySentimentAnalysis
{
	class Program
	{
		static void Main(string[] args)
		{
			var valenceDictionary = new Dictionary<string, string>();

			using (var reader = new StreamReader(File.OpenRead(@"Data/corpus.csv")))
			{
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					var values = line.Split(',');

					var valence = values[1];
					var tweetId = values[2];

					valenceDictionary.Add(tweetId, valence);
				}
			}

			var tweetDictionary = new Dictionary<string, string>();

			using (var reader = new StreamReader(File.OpenRead(@"Data/emo_tweets_annotated.csv")))
			{
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					var values = line.Split('\t');

					var tweetId = values[0].Substring(values[0].LastIndexOf("/")+1);
					var text = values[2];

					tweetDictionary.Add(tweetId, text);
				}
			}

			var greyTweetAnalyser = new GreyTweetAnalyser();

			var truePositive = 0;
			var falsePositive = 0;
			var trueNegative = 0;
			var falseNegative = 0;

			foreach (var tweet in tweetDictionary)
			{
				var id = tweet.Key;
				var text = tweet.Value;

				var tweetGreyScore = greyTweetAnalyser.CalculateTweetPerception(text);
				var average = (tweetGreyScore.Low + tweetGreyScore.High)/2;

				var valence = valenceDictionary["\"" + id + "\""];

				switch (valence)
				{
					case "\"positive\"":
						if (average >= 0)
							truePositive += 1;
						else
							falseNegative += 1;
						break;
					case "\"negative\"":
						if (average <= 0)
							trueNegative += 1;
						else
							falsePositive += 1;
						break;
				}
			}

			Console.WriteLine("TruePositive: " + truePositive);
			Console.WriteLine("TrueNegative: " + trueNegative);
			Console.WriteLine("FalsePositive: " + falsePositive);
			Console.WriteLine("FalseNegative: " + falseNegative);

			var sentence = "This is the first time I've been unhappy with any @apple product...";
			greyTweetAnalyser.CalculateTweetPerception(sentence);
		}
	}
}
