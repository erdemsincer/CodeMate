FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["PaymentService/PaymentService.csproj", "PaymentService/"]
COPY ["CodeMate.Shared.Contracts/CodeMate.Shared.Contracts.csproj", "CodeMate.Shared.Contracts/"]

RUN dotnet restore "PaymentService/PaymentService.csproj"

COPY . .

WORKDIR "/src/PaymentService"
RUN dotnet publish "PaymentService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PaymentService.dll"]
