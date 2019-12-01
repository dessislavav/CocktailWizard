[![Build status](https://ci.appveyor.com/api/projects/status/n5m3rut1wsx5j1xx?svg=true)](https://ci.appveyor.com/project/RadkoStanev/cocktailwizard)
# CocktailWizard
Web application that allows creation of cocktails and follows their distribution and success in amazing bars.

![Alt text](https://github.com/radkostanev/CocktailWizard/blob/master/Images/HomeScreen.jpg)

## Team Members
* Radko Stanev - [GitHub](https://github.com/radkostanev)
* Dessislava Valchanova - [GitHub](https://github.com/dessislavav)

## Project Description
### Areas
* **Public part** -  accessible without authentication
* **Private part** available for registered users only
* **Administrative part** available for administrators only

#### Public Part
* The public part consists of a home page displaying top-rated bars and cocktails in separate sections on the page as well as login page and register page.

![Alt text](https://github.com/radkostanev/CocktailWizard/blob/master/Images/HomeScreen_2.jpg)
* Upon clicking a bar, the visitor can see details for the bar and the cocktails it offers

![Alt text](https://github.com/radkostanev/CocktailWizard/blob/master/Images/BarView.jpg)
* Upon clicking a cocktail, the visitor can see details for the cocktail and the bars this cocktail is offered in

![Alt text](https://github.com/radkostanev/CocktailWizard/blob/master/Images/CocktailDetails.jpg)
* It also includes **searching possibility** for: 
     * Bars: by Name, Address and Rating	as well as for 
     * Cocktails: by Name, Ingredient/s and Rating
     
![Alt text](https://github.com/radkostanev/CocktailWizard/blob/master/Images/Search.jpg)

#### Private Part

* After login, users see everything visible to website visitors and additionally they can:
     * Rate bars
     * Rate cocktails (regardless of which bar offers them)
     * Leave a comment for a bar
     * Leave a comment for a cocktail
     
     ![Alt text](https://github.com/radkostanev/CocktailWizard/blob/master/Images/LoginView.jpg)
     
     ![Alt text](addRating.jpg)
     
     ![Alt text](addComment.jpg)

#### Administrative Part
* Admins can:
     * Manage ingredients – CRUD operations for ingredients for cocktails
     * Manage cocktails – CRUD operations for cocktails
     * Manage bars – CRUD operations for bars
     * Set cocktails as available in particular bars 
     
* Create Cocktail Modal
![Alt text](https://github.com/radkostanev/CocktailWizard/blob/master/Images/CreateModal.jpg)

* Edit Bar Modal
![Alt text](https://github.com/radkostanev/CocktailWizard/blob/master/Images/EditModal.jpg)

## Technologies
* ASP.NET Core
* ASP.NET Identity
* Entity Framework Core
* MS SQL Server
* Razor
* AJAX
* JavaScript / jQuery
* HTML
* CSS
* Bootstrap
* AppVeyor

## Notes
* Moq, InMemory and MSTest for testing
* Caching for performance optimization
* DTO (data transfer objects) used
* Server-side pagination present
* Used branches durring development ot features
* Used Azure DevOps to manage personal development tasks
* 90% Unit test code coverage of the business logic

## Database Diagram
![Alt text](https://github.com/radkostanev/CocktailWizard/blob/master/Images/DatabaseDiagram.png)
