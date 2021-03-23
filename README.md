# Movie Theater Api

## Setup Api

- [Install .NET 5 with Visual Studio](https://docs.microsoft.com/en-us/dotnet/core/install/windows?tabs=net50#install-with-visual-studio)
- git clone https://github.com/lucasfogliarini/MovieTheaterApi
- Run the tests in **PrintWayyMovieTheater.Test.Unit**
- Run the api **PrintWayyMovieTheater.Api** using IIS Express. It should:
  - Restore the packages.
  - Seed data using [EF Core In-Memory Database Provider](https://docs.microsoft.com/en-au/ef/core/providers/in-memory/?tabs=vs)
  - Open the api with the swagger interface on the server **http://localhost:1100**
  
## Setup UI
See instructions in [MovieTheaterUI](https://github.com/lucasfogliarini/MovieTheaterUI)
