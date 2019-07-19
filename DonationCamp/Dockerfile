FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 50501

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY DonationCamp/DonationCamp.csproj DonationCamp/
RUN dotnet restore "DonationCamp/DonationCamp.csproj"
COPY . .
WORKDIR "/src/DonationCamp"
RUN dotnet build "DonationCamp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DonationCamp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DonationCamp.dll"]

FROM prom/prometheus:v2.11.1
COPY prometheus.yml /etc/prometheus/prometheus.yml
