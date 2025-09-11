FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001
ENV ASPNETCORE_URLS=http://+:5001 \
    ASPNETCORE_ENVIRONMENT=Development \
    DOTNET_EnableDiagnostics=0

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["InvestmentPerformanceWebApi.csproj", "./"]
RUN dotnet restore "InvestmentPerformanceWebApi.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "InvestmentPerformanceWebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "InvestmentPerformanceWebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false /p:PublishReadyToRun=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN useradd -m appuser && chown -R appuser /app
USER appuser
ENTRYPOINT ["dotnet", "InvestmentPerformanceWebApi.dll"]
