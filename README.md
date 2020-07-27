This repository is started to notice by some developer so I decided to write a readme file to explain the purpose of this approach.

Normally there is a post exist for this project but this project a bit changed and improved after that post. I might update the medium post soon. Here is the [link](https://medium.com/@berkayyerdelen/building-restful-api-with-dapper-and-asp-net-core-37e6d9d1bdda) of the medium post

## Technologies
* ASP.NET Core 3.1
* Dapper

## Functionalities
- Basic CRUD Operations 
- Dapper Imp with best practises

## Contributes
This part has come from https://github.com/workcontrolgit (S)He has contributed this part and changed application structure more reliable and async-await approach

### Async/Await (to support high volumn webapi calls)
- Updated Services\IProductRepository.cs with keyword Task
- Updated Services\ProductRepository.cs with async/await

### Base class to manage DB connection (cleaner code)
- Added Services\BaseRepository.cs 
- Inherited BaseRepository in ProductRepository.cs
- Used WithConnection method to simplify database connection open/close

### Minor code clean up (development friendly)
- Updated connection string in asppsettings.json to use common Server=(localdb)\\mssqllocaldb (vs desktop name)
- Updated launhUrl to "swagger" (in the Properties\launchSettings.json)
- Referereced NSwag.AspNetCore package via Nuget Management and Updated Startup.cs to use UseSwaggerUi3

#Credit
The base class to manage the connection string is from https://www.joesauve.com/async-dapper-and-async-sql-connection-management/

This part has come from https://github.com/Arnab-Developer He has contributed this part.


## Unit Tests
- Added Unit tests for ProductController and Product repository

## DapperHelper Imp
- Added Dapper implementation for seperate the dapper dependency for database accessing.
