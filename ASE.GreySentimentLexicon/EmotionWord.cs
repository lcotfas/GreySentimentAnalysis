
namespace ASE.GreySentimentLexicon
{
	class EmotionWord
	{
		public string Word { get; set; }
		public double Valence { get; set; }
		public string Lexicon { get; set; }

		public EmotionWord(string word, double valence, string lexicon)
		{
			Word = word;
			Valence = valence;
			Lexicon = lexicon;
		}
	}
}
