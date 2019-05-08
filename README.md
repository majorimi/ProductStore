# ProductStore
Test project for ASP.NET MVC/Web API implemented basic WebStore

## The problem 
 
After many sessions with our client, it was decided to develop a Product store as a Web application with the following key attributes: 
  
- Product Name 
- Product Image 
- Product Category 
- Price 
- Product Rating (1 to 5 stars) 

The application has the following user roles:
1.	End User:  
•	Browse through the products and rate them. 
•	Browse through the products and sort the products by price or best rating with in the product category. 
2.	Administrators:  
•	Only Admins can create/update/delete products from the catalogue.

## High level architecture

![Architecture](https://github.com/majorimi/ProductStore/raw/master/doc/Architecture.PNG "High lever architecture")

The project contains a Web API backend with EF6 Code first DB wrapped into Repository pattern objects. Also has ASP.NET MVC project for frontend. It is calling IDP to acquire OAuth2 Tokens via OAuth Code flow. In order to access data it must call API endpoint and passing a correct Bearer Token.

- IDP: Identity provider (used IdentityServer 3). Which is a company "wide" application for managing all Users. So we can implement secure authentication and authorization in all our projects with SSO experience.
- Backend: ASP.NET WEB API project with EF6 Code first
- Frontent: ASP.NET MVC project but also can be any SPA as well since users handled in IDP and all data access happens via API endpoint calls.

## Install DB

![EF](https://github.com/majorimi/ProductStore/raw/master/doc/EF_DB_Install.png "EF Install")

- Select "*ProductStore.Domain.Data*" project a start up
- Check the app.config in that project for SQL connection string and the Test data install settings.
- Open "Package Manager Console"
- Select out "*ProductStore.Domain.Data*" project
- Run command: "update-database -verbose"

## How to run the project
In order to run the project you have to set up multiple start up.

- Set the below 3 projects as stratup
- Check the API web.config for SQL connection string

![Run](https://github.com/majorimi/ProductStore/raw/master/doc/start.png "Run")

## Swagger usage

![Swagger](https://github.com/majorimi/ProductStore/raw/master/doc/swagger.png "Swagger")

Swagger is an Open sourced API "self" documentation project https://swagger.io/. Also it can be used for testing the API. 
To try it run the API project and type in "/swagger/ui/index" after the port number.

## Project status, future improvements

- Write unit tests
- Use caching on API side
- Implement frontend with SPA framework
- IDP should use DB and other 3rd party providers (Google, Facebook)