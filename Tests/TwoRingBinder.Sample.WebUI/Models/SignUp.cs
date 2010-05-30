using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TwoRingBinder.Sample.WebUI.Models
{
	public class SignUp
	{
		[Required]
		public string FirstName { get; set; }
		
		[Required]
		public string LastName { get; set; }

		[Required]
		public string Origin { get; set; } //This is required and will be added PreBindModel
        
		public bool IsFemale { get; set; } //This is not required and will be added PostBindModel

		public bool FromRoot { get; set; }

		public void OnBound() // Invoked after model is bound
		{
			FromRoot = Origin == "/";	
		}
	}
}
