TwoRingBinder is a Custom ModelBinder for ASP.NET MVC

Installation & Usage
------------------------
1) Add the ModelBinder to your Application_Start event, via:
   ModelBinders.Binders.DefaultBinder = new TwoRingBinder.Core.ModelBinder();

2) Create your Models

3) Create one or more implementations of IBindModelExtension for your Models

4) Your implementation Pre & Post Model Bind events will now be invoked by the ModelBinder.  The default binding will still occur.


Notes 
------------------------
If no implementation of IBindModelExtension is found for your Model, it will be bound normally.  
The two methods of IBindModelExtension are void so feel free to just define the method's signature if you don't want to use it.


Let me know if you have any questions or suggestions!

mark@kockerbeck.com