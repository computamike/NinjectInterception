# NinjectInterception
Proof of concept tests for AOP (Ninject)


This is a bit ropey.  

The idea is that 
```
+---------------------+                                                
|                     |                                                
|  View model         |                                                
|                     |                                                
|                     |    Our Page Object class inherits from the     
|                     |    underlying view model.  This means that     
+---------^-----------+    as soon as properties are added to the      
          |                view model they become part of the Page     
+---------+-----------+    Object.                                     
|                     |                                                
|  Page Object        |    
|                     |     
|                     |     
| +-----------------+ |     
+-+                 +-+     
  | Intercept All   |                                                  
  | Property Get/Set|       
  | Calls           |       
  | (Read from web) |       
  |                 |       
  +-----------------+                          
```
Whenever a call is made to set/get a property, an interceptor gets in the way and rather than setting a property - this allows the developer the opportunity to look up the value from the web page.  

This interception has been hardcoded for this example.  A Page Object has a method called "NowTime" - which returns the current DateTime.  It also has a property - the "MyProperty" property.  

The page object has access to all of the properties in the view model - so things like display labels, control types (based on the underlying type) are available within the interception.

There is even the possibility of making the PageObject a partial class or moving the interception straight into the ViewModel.
