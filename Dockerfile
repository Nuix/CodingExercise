FROM mcr.microsoft.com/dotnet/sdk:10.0.404 AS build
WORKDIR /source
COPY global.json Directory.Build.props ./
COPY src/InvestmentPerformance.Api/InvestmentPerformance.Api.csproj src/InvestmentPerformance.Api/
RUN dotnet restore src/InvestmentPerformance.Api
COPY src/ src/
RUN dotnet publish src/InvestmentPerformance.Api -c Release --no-restore -o /app /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0.15 AS runtime
WORKDIR /app
COPY --from=build /app .
USER root
RUN mkdir /data && chown $APP_UID:$APP_UID /data
USER $APP_UID
ENV ASPNETCORE_HTTP_PORTS=8080
ENV ConnectionStrings__Investments="Data Source=/data/investments.db"
EXPOSE 8080
HEALTHCHECK --interval=30s --timeout=5s --start-period=20s --retries=3 \
    CMD ["dotnet", "InvestmentPerformance.Api.dll", "--check-health"]
ENTRYPOINT ["dotnet", "InvestmentPerformance.Api.dll"]
