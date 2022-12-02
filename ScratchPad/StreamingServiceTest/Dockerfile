FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /src
COPY ["StreamingServiceTest/StreamingServiceTest.csproj", "StreamingServiceTest/"]

RUN dotnet restore "StreamingServiceTest/StreamingServiceTest.csproj"
COPY . .
WORKDIR "/src/StreamingServiceTest"
RUN dotnet build "StreamingServiceTest.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "StreamingServiceTest.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "StreamingServiceTest.dll"]