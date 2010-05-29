using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Microsoft.Web.Mvc.DataAnnotations;

namespace TwoRingBinder.Core
{
	public class ModelBinder : DataAnnotationsModelBinder
	{
		/// <summary>
		/// Will Search for IBindModelExtension[T] and call it
		/// </summary>
		/// <param name="controllerContext"></param>
		/// <param name="bindingContext"></param>
		/// <returns></returns>
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var allTypes = GetBindingExtensions(bindingContext);

			if (allTypes.Count == 0)
			{
				return base.BindModel(controllerContext, bindingContext); ;
			}

			// Fire All PreBindModel Events
			allTypes.ForEach(type => type.InvokeMember("PreBindModel", BindingFlags.InvokeMethod, null,
				GetInstance(type, bindingContext.ModelName), new object[] { controllerContext, bindingContext }));

			// Call the base.BindModel Method
			var boundModel = base.BindModel(controllerContext, bindingContext);

			// Fire All PostBindModel Events
			allTypes.ForEach(type => type.InvokeMember("PostBindModel", BindingFlags.InvokeMethod, null, 
				GetInstance(type, bindingContext.ModelName), new[] { boundModel, controllerContext, bindingContext }));
			
			return boundModel;
		}
        
		private readonly Dictionary<Type, object> _instances = new Dictionary<Type, object>();
		private object GetInstance(Type type, string name)
		{
			if (!_instances.ContainsKey(type))
			{
				var instance = Activator.CreateInstance(type);
				if (instance == null) throw new Exception(String.Format("Cannot create instance of {0}", name));

				_instances.Add(type, instance);
			}

			return _instances[type];
		}

		private static Type GetBindingExtension(ModelBindingContext bindingContext)
		{
			if (bindingContext == null) throw new ArgumentNullException("bindingContext");
			return typeof(IBindModelExtension<>).MakeGenericType(bindingContext.ModelType);
		}

		private static List<Type> GetBindingExtensions(ModelBindingContext bindingContext)
		{
			return Assembly.GetAssembly(bindingContext.ModelType).GetTypes()
				.Where(t => t.IsImplementationOf(GetBindingExtension(bindingContext)))
				.ToList();
		}
	}
}
