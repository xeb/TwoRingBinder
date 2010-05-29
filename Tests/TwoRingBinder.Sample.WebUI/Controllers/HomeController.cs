using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwoRingBinder.Sample.WebUI.Models;

namespace TwoRingBinder.Sample.WebUI.Controllers
{
	[HandleError]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View(new SignUp());
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Index(SignUp signUp)
		{
			if(!ModelState.IsValid)
			{
				return View(signUp);
			}

			return View("ThankYou", signUp);
		}
	}
}
