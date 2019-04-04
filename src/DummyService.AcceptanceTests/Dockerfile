FROM microsoft/dotnet:2.1-runtime-stretch-slim AS base
WORKDIR /app


FROM microsoft/dotnet:2.1-sdk-stretch AS build
WORKDIR /src
COPY ["DummyService.AcceptanceTests/DummyService.AcceptanceTests.csproj", "DummyService.AcceptanceTests/"]
RUN dotnet restore "DummyService.AcceptanceTests/DummyService.AcceptanceTests.csproj"
COPY . .
WORKDIR "/src/DummyService.AcceptanceTests"
RUN dotnet build "DummyService.AcceptanceTests.csproj" -c Release -o /app

FROM build AS test
RUN chmod 777 wait-for-it.sh
#ENTRYPOINT ["dotnet", "test", "DummyService.AcceptanceTests.csproj"]