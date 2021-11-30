#ApuiBotNlu

Bot Framework v4

## Prerequisites

### Install .NET Core

- [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet/3.1) version 3.1

## To try

- In a terminal, navigate to `ApuiBotNlu`

    ```bash
    # change into project folder
    cd ApuiBotNlu
    ```

- Run the bot from a terminal or from Visual Studio, choose option A or B.

  A) From a terminal

  ```bash
  # run the bot
  dotnet run
  ```

  B) Or from Visual Studio

  - Launch Visual Studio
  - File -> Open -> Project/Solution
  - Navigate to `ApuiBotNlu` folder
  - Select `ApuiBotNlu.csproj` file
  - Press `F5` to run the project

## Testing the bot using Bot Framework Emulator

Install [Bot Framework Emulator](https://github.com/microsoft/BotFramework-Emulator/releases/tag/v4.14.1)

- Launch Bot Framework Emulator
- Open Bot
- Enter a Bot URL of `http://localhost:3978/api/messages`
