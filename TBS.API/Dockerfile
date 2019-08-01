FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app

ENV ASPNETCORE_URLS http://+:7000
EXPOSE 7000

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["TBS.API/azure-secrets.json", "/app/"]
COPY ["TBS.API/TBS.API.csproj", "TBS.API/"]
COPY ["TBS.Data/TBS.Data.csproj", "TBS.Data/"]
COPY ["TBS.Services/TBS.Services.csproj", "TBS.Services/"]
RUN dotnet restore "TBS.API/TBS.API.csproj"
COPY . .
WORKDIR "/src/TBS.API"
RUN dotnet build "TBS.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TBS.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TBS.API.dll"]