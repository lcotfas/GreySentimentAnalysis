using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE.GreySentimentAnalysis
{
	class Program
	{
		private static void Main(string[] args)
		{
			var lexiconNames = new List<string>();

			var wordDictionary = new SortedDictionary<string, List<EmotionWord>>();

			const string lexiconMaxDiff = "Maxdiff-Twitter-Lexicon_-1to1";
			lexiconNames.Add(lexiconMaxDiff);
			using (var reader = new StreamReader(File.OpenRead(@"Data/Maxdiff-Twitter-Lexicon_-1to1.txt")))
			{
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					var values = line.Split('\t');

					var word = values[1].Replace(",", string.Empty);
					var valence = double.Parse(values[0]);

					var emotionWord = new EmotionWord(word, valence, lexiconMaxDiff);

					if (wordDictionary.ContainsKey(word))
					{
						wordDictionary[word].Add(emotionWord);
					}
					else
					{
						wordDictionary.Add(word, new List<EmotionWord> { emotionWord });
					}

				}
			}

			const string lexiconSentiment140 = "unigrams-pmilexicon";
			lexiconNames.Add(lexiconSentiment140);
			//using (var reader = new StreamReader(File.OpenRead(@"Data/unigrams-pmilexicon.txt")))
			//{
			//	while (!reader.EndOfStream)
			//	{
			//		var line = reader.ReadLine();
			//		var values = line.Split('\t');

			//		var word = values[0].Replace(",", string.Empty);
			//		var valence = double.Parse(values[1]) / 5.0;

			//		var emotionWord = new EmotionWord(word, valence, lexiconSentiment140);

			//		if (wordDictionary.ContainsKey(word))
			//		{
			//			wordDictionary[word].Add(emotionWord);
			//		}
			//		else
			//		{
			//			wordDictionary.Add(word, new List<EmotionWord> { emotionWord });
			//		}

			//	}
			//}

			const string lexiconVader = "vader_sentiment_lexicon";
			lexiconNames.Add(lexiconVader);
			using (var reader = new StreamReader(File.OpenRead(@"Data/vader_sentiment_lexicon.txt")))
			{
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					var values = line.Split('\t');

					var word = values[0].Replace(",", string.Empty);
					var valence = double.Parse(values[1]) / 4.0;

					var emotionWord = new EmotionWord(word, valence, lexiconVader);

					if (wordDictionary.ContainsKey(word))
					{
						wordDictionary[word].Add(emotionWord);
					}
					else
					{
						wordDictionary.Add(word, new List<EmotionWord> { emotionWord });
					}

				}
			}

			using (var w = new StreamWriter("GreySentimentLexicon.csv"))
			{
				var line = "Word";
				//foreach (var lexiconName in lexiconNames)
				//{
				//	line += ",'" + lexiconName + "'";
				//}
				//line += ",'Grey Sentiment Lexicon'";

				//w.WriteLine(line);
				//w.Flush();

				foreach (var wordDictionaryEntry in wordDictionary)
				{
					var word = wordDictionaryEntry.Value[0].Word;

					line  = word;

					foreach (var lexiconName in lexiconNames)
					{
						var emotionWord = wordDictionaryEntry.Value.FirstOrDefault(x => x.Lexicon == lexiconName);

						if(emotionWord != null)
							line += "," + emotionWord.Valence + "";
						else
							line += ",";
					}

					var min = wordDictionaryEntry.Value.Min(x => x.Valence);
					var max = wordDictionaryEntry.Value.Max(x => x.Valence);
					line += "," + min + "";
					line += "," + max + "";

					w.WriteLine(line);
					w.Flush();
				}
			}
		}
	}
}
