
# RapidPay

A small project to show how to create a Web API in dotnet core 6, implementing vertical slice architecture.
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



## Vertical Slices Architecture

According to Jimmy Bogard: "In this style, my architecture is built around distinct requests, encapsulating and grouping all concerns from front-end to back. You take a normal "n-tier" or hexagonal/whatever architecture and remove the gates and barriers across those layers, and couple along the axis of change:"
![vertical-slices](https://github.com/hgdiaz/RapidPay/blob/main/img/slices.png?raw=true)

Source: https://www.jimmybogard.com/vertical-slice-architecture/

The main idea with this project is to make a simple implementation of this architecture, without having several projects or other artifacts.

In a future, probably I make the same solution using an onion architecture, like the image below:
![onion-sample](https://github.com/hgdiaz/RapidPay/blob/main/img/Onion_sample.jpg?raw=true)

Here remains the idea of a clean architecture, but I prefer this for more complex projects.
For more information, check Palermo's website: https://jeffreypalermo.com/2008/07/the-onion-architecture-part-1/

## Running the project
To run this project you need Visual Studio 2022 with dotnet core 6 installed.
Then, inside the src folder, open the solution file RapidPay.sln with VS.
The nuget packages will be restored automatically.
Select the RapidPayAPI project and press F5. You'll see the Swagger UI to call the endpoints.
Before calling the endpoints, you must login with a valid user. There are 2 already created: "admin" with the pass "Pass.123" and "user" with the same password.
Just call the /api/Authenticate/login endpoint and fill the credentials. You'll get a JWT in the response. Copy this token and in the Authorize button in the upper right. In the Value textbox, type the word Bearer and then paste your token. The click on the authorize button. That´s all....now you can call the endpoints (if the user has the permission to call it).
Tip: in the CardsController.cs and the PaymentsController.cs files, there are commented endpoints to retreive usefull data to test if everything is working ok.
