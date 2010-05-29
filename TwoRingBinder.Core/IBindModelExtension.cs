using System;
using System.Linq;
using System.Web.Mvc;

namespace TwoRingBinder.Core
{
	public interface IBindModelExtension<T>
	{
		void PreBindModel(ControllerContext controllerContext, ModelBindingContext bindingContext);
		void PostBindModel(T boundModel, ControllerContext controllerContext, ModelBindingContext bindingContext);
	}
}
