
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
- [ ] Logging with Serilog



## Vertical Slices Architecture

According to Jimmy Bogard: "In this style, my architecture is built around distinct requests, encapsulating and grouping all concerns from front-end to back. You take a normal "n-tier" or hexagonal/whatever architecture and remove the gates and barriers across those layers, and couple along the axis of change:"
![vertical-slices](https://github.com/hgdiaz/RapidPay/blob/main/img/slices.png?raw=true)

Source: https://www.jimmybogard.com/vertical-slice-architecture/

The main idea with this project is to make a simple implementation of this architecture, without having several projects or other artifacts.

In a future, probably I make the same solution using an onion architecture, like the image below:
![onion-sample](https://github.com/hgdiaz/RapidPay/blob/main/img/Onion_sample.jpg?raw=true)
Here remains the idea of a clean architecture, but I prefer this for more complex projects.
For more information, check Palermo's website: https://jeffreypalermo.com/2008/07/the-onion-architecture-part-1/

