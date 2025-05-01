# Temel .NET ASP.NET imajını kullanıyoruz
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# SDK imajını kullanarak uygulama derlenecek
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# NotificationService ve Shared projelerini kopyalayalım
COPY ["NotificationService/NotificationService.csproj", "NotificationService/"]
COPY ["CodeMate.Shared.Contracts/CodeMate.Shared.Contracts.csproj", "CodeMate.Shared.Contracts/"]

# Bağımlılıkları restore edelim
RUN dotnet restore "NotificationService/NotificationService.csproj"

# Kodları kopyalayalım
COPY . .

# Uygulamayı yayınlayalım
WORKDIR "/src/NotificationService"
RUN dotnet publish "NotificationService.csproj" -c Release -o /app/publish

# Final imajını oluşturuyoruz
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "NotificationService.dll"]
