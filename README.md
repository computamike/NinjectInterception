# NinjectInterception
Proof of concept tests for AOP (Ninject)


This is a bit ropey.  

The idea is that 
```
+---------------------+
|                     |
|  View model         |
|                     |
|                     |
|                     |
+---------^-----------+
          |            
+---------+-----------+
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
