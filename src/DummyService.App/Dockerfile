FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DummyService.App/DummyService.App.csproj", "DummyService.App/"]
RUN dotnet restore "DummyService.App/DummyService.App.csproj"
COPY . .
WORKDIR "/src/DummyService.App"
RUN dotnet build "DummyService.App.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DummyService.App.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DummyService.App.dll"]
