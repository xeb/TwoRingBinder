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

			if (!allTypes.Any())
			{
				return base.BindModel(controllerContext, bindingContext); ;
			}

			// Fire All PreBindModel Events
			allTypes.ForEach(type => type.InvokeMember("PreBindModel", BindingFlags.InvokeMethod, null,
				GetInstance(type, bindingContext.ModelName), new object[] { controllerContext, bindingContext }));

			// Call the base.BindModel Method
			var boundModel = base.BindModel(controllerContext, bindingContext);

			// Fire Bound Event in Model
			var onBound = bindingContext.ModelType.GetMethod("OnBound");
			if(onBound != null)
			{
				if (onBound.GetParameters().Any())
				{
					onBound.Invoke(boundModel, new object[] {controllerContext, bindingContext});
				}
				else
				{
					onBound.Invoke(boundModel, null);
				}
			}

			// Fire All PostBindModel Events
			allTypes.ForEach(type => type.InvokeMember("PostBindModel", BindingFlags.InvokeMethod, null, 
				GetInstance(type, bindingContext.ModelName), new[] { boundModel, controllerContext, bindingContext }));
			
			return boundModel;
		}
        
		private static readonly Dictionary<Type, object> Instances = new Dictionary<Type, object>();
		private static object GetInstance(Type type, string name)
		{
			if (!Instances.ContainsKey(type))
			{
				var instance = Activator.CreateInstance(type);
				if (instance == null) throw new Exception(String.Format("Cannot create instance of {0}", name));

				Instances.Add(type, instance);
			}

			return Instances[type];
		}
        
		private static List<Type> GetBindingExtensions(ModelBindingContext bindingContext)
		{
			return Assembly.GetAssembly(bindingContext.ModelType).GetTypes()
				.Where(t => IsImplementationOf(t, GetBindingExtension(bindingContext)))
				.ToList();
		}

		private static Type GetBindingExtension(ModelBindingContext bindingContext)
		{
			if (bindingContext == null) throw new ArgumentNullException("bindingContext");
			return typeof(IBindModelExtension<>).MakeGenericType(bindingContext.ModelType);
		}

		private static Boolean IsImplementationOf(Type type, Type interfaceType)
		{
			return type.GetInterfaces().Any(i => i.FullName == interfaceType.FullName);
		}
	}
}
