FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FindPathProc.SpeedTest/FindPathProc.SpeedTest.csproj", "FindPathProc.SpeedTest/"]
RUN dotnet restore "FindPathProc.SpeedTest/FindPathProc.SpeedTest.csproj"
COPY . .
WORKDIR "/src/FindPathProc.SpeedTest"
RUN dotnet build "FindPathProc.SpeedTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FindPathProc.SpeedTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FindPathProc.SpeedTest.dll"]
