namespace ASE.GreySentimentAnalysis
{
	public class GreyNumber
	{
		public double Low { get; set; }
		public double High { get; set; }

		public double Mean => (Low + High)/2;

		public GreyNumber(double low, double high)
		{
			Low = low;
			High = high;
		}

		public override string ToString()
		{
			return $"[{Low}, {High}]";
		}
	}
}
