using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwoRingBinder.Core;
using TwoRingBinder.Sample.WebUI.Models;

namespace TwoRingBinder.Sample.WebUI.Libraries
{
	public class SignUpBindModelExtension : IBindModelExtension<SignUp>
	{
		/// <summary>
		/// Set the LastName to "Binder" before ModelBinding
		/// </summary>
		/// <param name="controllerContext"></param>
		/// <param name="bindingContext"></param>
		public void PreBindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var origin = controllerContext.HttpContext.Request.RawUrl;

			bindingContext.ValueProvider.Add("Origin", 
				new ValueProviderResult(origin, origin, new System.Globalization.CultureInfo("en-US")));

		}

		private readonly string[] _girlsNames = new[] { "Elaine", "Stephanie", "Jennifer" };
		
		/// <summary>
		/// Set the IsFemale Property of the Model
		/// </summary>
		/// <param name="boundModel"></param>
		/// <param name="controllerContext"></param>
		/// <param name="bindingContext"></param>
		public void PostBindModel(SignUp boundModel, ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			boundModel.IsFemale = _girlsNames.Contains(boundModel.FirstName);
		}
	}
}
