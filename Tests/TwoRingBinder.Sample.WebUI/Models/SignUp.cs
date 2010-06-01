using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace TwoRingBinder.Sample.WebUI.Models
{
	public class SignUp
	{
		[Required]
		public string FirstName { get; set; }
		
		[Required]
		public string LastName { get; set; }

		[Required]
		public string Origin { get; set; } //This is required and will be added PreBindModel via the Extension
        
		public bool IsFemale { get; set; } //This is not required and will be added PostBindModel via the Extension

		public bool FromRoot { get; set; }

		/// <summary>
		/// Will be Invoked first before the Extension
		/// </summary>
		/// <remarks>Any properties set on this Event will be lost after Binding</remarks>
		/// <param name="controllerContext"></param>
		/// <param name="bindingContext"></param>
		public void OnBinding(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var host = controllerContext.HttpContext.Request.Url.Host;

			bindingContext.ValueProvider.Add("Host",
				new ValueProviderResult(host, host, new System.Globalization.CultureInfo("en-US")));
		}

		/// <summary>
		/// Will be Invoked Last after the Extension
		/// </summary>
		public void OnBound() // Invoked after model is bound
		{
			FromRoot = Origin == "/";	
		}
	}
}
