FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DummyService.AcceptanceTests/DummyService.AcceptanceTests.csproj", "DummyService.AcceptanceTests/"]
RUN dotnet restore "DummyService.AcceptanceTests/DummyService.AcceptanceTests.csproj"
COPY . .
WORKDIR "/src/DummyService.AcceptanceTests"
RUN dotnet build "DummyService.AcceptanceTests.csproj" -c Release -o /app

FROM build AS test
#ENTRYPOINT ["dotnet", "test", "DummyService.AcceptanceTests.csproj"]