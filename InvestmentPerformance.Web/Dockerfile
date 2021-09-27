FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["InvestmentPerformance.Web/InvestmentPerformance.Web.csproj", "InvestmentPerformance.Web/"]
RUN dotnet restore "InvestmentPerformance.Web/InvestmentPerformance.Web.csproj"
COPY . .
WORKDIR "/src/InvestmentPerformance.Web"
RUN dotnet build "InvestmentPerformance.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InvestmentPerformance.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InvestmentPerformance.Web.dll"]
