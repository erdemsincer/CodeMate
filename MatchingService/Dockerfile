FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["MatchingService/MatchingService.csproj", "MatchingService/"]
COPY ["CodeMate.Shared.Contracts/CodeMate.Shared.Contracts.csproj", "CodeMate.Shared.Contracts/"]

RUN dotnet restore "MatchingService/MatchingService.csproj"
COPY . .
WORKDIR "/src/MatchingService"
RUN dotnet publish "MatchingService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MatchingService.dll"]
