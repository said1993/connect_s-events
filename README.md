# connect_s-events


## Prerequisites

- .NET 7.0
- An API key for the third-party service that this microservice depends on

## Building and Running
1. Clone this repository
2. Navigate to the root directory of the repository
3. Run `dotnet restore` to install dependencies
4. Run `dotnet build` to build the project
5. Run `dotnet run` to start the microservice

## Testing
The unit tests are located in the `tests` directory. They use the [XUnit](https://xunit.net/) testing framework and are designed to test the individual components of the microservice.
To run the unit tests, use the following command:
`dotnet test`
