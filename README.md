
# RapidPay

A small project to show how to create a Web API in dotnet core 6, implementing vertical slices architecture.
With this sample you can see how to use CQRS (with Mediatr), Fluent Validation, Entity Framework, Automapper, Identity with JWT, Swagger, Logging (with Serilog) and other usefull stuff.


## Features

- [x] CQRS with Mediatr
- [x] Fluent Validation
- [x] Automapper
- [x] Entity Framework
- [x] Identity
- [x] JWT
- [x] Swagger
- [x] Logging with Serilog
## Pending improvements

- [ ] Use scopes for authorization
- [ ] Better test coverage


## Vertical Slices Architecture

According to Jimmy Bogard: "In this style, my architecture is built around distinct requests, encapsulating and grouping all concerns from front-end to back. You take a normal "n-tier" or hexagonal/whatever architecture and remove the gates and barriers across those layers, and couple along the axis of change:"
![vertical-slices](https://github.com/hgdiaz/RapidPay/blob/main/img/slices.png?raw=true)

Source: https://www.jimmybogard.com/vertical-slice-architecture/

The main idea with this project is to make a simple implementation of this architecture, without having several projects or other artifacts.

Update 09/13/2023: check this new repo with a similar project: https://github.com/hgdiaz/CardManagement
It's a small project to show how to create a Web API in dotnet core 6, implementing the onion architecture. With this sample you can see how to use CQRS (with Mediatr), Fluent Validation, Dapper, Automapper, Identity with JWT, Swagger, Logging (with Serilog) and other usefull stuff.


## Running the project
To run this project you need Visual Studio 2022 with dotnet core 6 installed.
Then, inside the src folder, open the solution file **RapidPay.sln** with VS.
The nuget packages will be restored automatically.
Select the RapidPayAPI project and press F5. You'll see the Swagger UI to call the endpoints.
Before calling the endpoints, you must login with a valid user. There are 2 already created: "**admin**" with the pass "**Pass.123**" and "**user**" with the same password.
Just call the /api/Authenticate/login endpoint and fill the credentials. You'll get a JWT in the response. Copy this token and in the Authorize button in the upper right. In the Value textbox, type the word **Bearer** and then paste your token. The click on the authorize button. That´s all....now you can call the endpoints (if the user has the permission to call it).
**Obs**: in the CardsController.cs and the PaymentsController.cs files, there are commented endpoints to retreive usefull data to check if everything is working ok. It's a good idea to uncomment this endpoints to see in Swagger the records saved in the DB.

## About the functionality
The requirements are:

 1. The card management module includes three API endpoints:
 - Create card (card format is 15 digits) 
 - Pay (using the created card,
   and update balance) 
   Get card balance
 2. Every hour, the Universal Fees Exchange (UFE) randomly selects a decimal between 0 and 2.
The new fee price is the last fee amount multiplied by the recent random decimal.
You should develop a Singleton to simulate the UFE service and the fee should be applied to every
payment

Extra requirements:

 - Improve your API performance and throughput using multithreading.
 - Generally, using basic authentication is not a good solution. Improve the authentication so we can make our Authorization system secure.
 - Make the shared resources thread safe using a design pattern in case you are storing the data in the memory. In case you are using a database to persist the cards and transaction improve the database design and the usage of the ORM framework
 
 About the card logic:
 The Card entity does not behave like one of those known as Credit or Debit, instead, they are governed by the following rules:
1. A card is created without a balance or line of credit and is ready to be used to pay for goods or services.
2. A card can only be used to make payments, there is no way to pay your balance or restore your credit level, since both concepts do not exist in the problem statement.
3. The balance query will only reflect the sum of the consumptions made added to the commission calculated for each event, in its calculation
Concepts such as charge/credit or revolving credit do not enter."
