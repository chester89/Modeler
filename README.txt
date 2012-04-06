ViewModeler is simple MVVM framework for WPF, written in C#.
It allows you to write your ViewModel classes much cleaner because you don't need to write property
notification code on each property (it internally uses awesome PostSharp to do this kind of work for you under the covers).
It also has:
 - basic implementation of ICommand interface and supports declaring logic of CanExecute() method in terms of ViewModel properties, 
 and CanExecute() will know about any property notification, so you as a developer don't need to bother
 - support for automatic notifying a user if particular operation have failed (just use the right ActionBase subclass for it)
 - baked-in IoC and validation support (StructureMap and FluentValidation are used by default, DataAnnotations can be used easily, planned support for NInject/Unity).
It uses Convention over Configuration approach, so it allows you to override pretty much every aspect that you don't like.
It also includes simple WPF example app.
It is currently in pre-release mode, so please use code with caution. I haven't yet cleaned up some bits as I intended to, 
unit-test coverage is also not very high at the moment, so many refactorings will be done.
Of course, this little library is nowhere near frameworks like Caliburn, Cinch or Prism, but it's not my goal to compete with them
in terms of functionality. My goal is to learn WPF & MVVM pattern more thoroughly and provide basic and simple building blocks 
for developing WPF applications with ease.

Roadmap for nearest future is this:
	- unit-testing support for ViewModel classes and Commands
	- IoC support agnostic from a third part library
	- support for libraries like AutoMapper to allow to map data received from server to ViewModel classes using conventions
	- Silverlight support (I'm not sure about that as I don't know what it'll take to support SL)
	
I know code in its current state is far from perfect, but I believe I'm moving in right direction. 
I think first release will come in April 2012.
As always, your feedback is kindly appreciated, folks. Feel free to ping we on twitter @chester89 for any suggestions and questions
if you have any.  
 