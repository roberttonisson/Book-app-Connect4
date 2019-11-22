# Books demo


Install tooling
~~~
dotnet tool install --global dotnet-aspnet-codegenerator
~~~

Install into WebApp via nuget
Microsoft.EntityFrameworkCore.Design


Migration
~~~
dotnet ef migrations add InitialDbCreation --project DAL --startup-project WebApp
~~~


Update database
~~~
dotnet ef database update --project DAL --startup-project WebApp
~~~

Remove migration
~~~

~~~


Kill database
~~~
~~~

Install via nuget to WebApp
Microsoft.VisualStudio.Web.CodeGeneration.Design 
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.SqlServer

Scaffold pages (cs into WebApp first)
~~~
dotnet aspnet-codegenerator razorpage -m Author -outDir Pages/Authors -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator razorpage -m Book -outDir Pages/Books -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator razorpage -m BookAuthor -outDir Pages/BookAuthors -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator razorpage -m Comment -outDir Pages/Comments -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator razorpage -m Language -outDir Pages/Languages -dc AppDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator razorpage -m Publisher -outDir Pages/Publishers -dc AppDbContext -udl --referenceScriptLibraries -f
~~~

