FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["KaydenMiller.BattleTech.Helper.Cli/KaydenMiller.BattleTech.Helper.Cli.csproj", "KaydenMiller.BattleTech.Helper.Cli/"]
RUN dotnet restore "KaydenMiller.BattleTech.Helper.Cli/KaydenMiller.BattleTech.Helper.Cli.csproj"
COPY . .
WORKDIR "/src/KaydenMiller.BattleTech.Helper.Cli"
RUN dotnet build "KaydenMiller.BattleTech.Helper.Cli.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "KaydenMiller.BattleTech.Helper.Cli.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KaydenMiller.BattleTech.Helper.Cli.dll"]
