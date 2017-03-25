using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASE.GreySentimentAnalysis.Web.Models;

namespace ASE.GreySentimentAnalysis.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public ActionResult Index(AnalysisViewModel viewModel)
		{
			if (viewModel.Tweet != null)
			{
				var greyTweetAnalyser = new GreyTweetAnalyser();
				var analysisResult = greyTweetAnalyser.CalculateTweetPerception(viewModel.Tweet);
				viewModel.AnalysisResult = analysisResult;
			}

			return View(viewModel);
		}
	}
}