FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY Person/PersonDocument.csproj Person/
RUN dotnet restore "Person/PersonDocument.csproj"
COPY . .
WORKDIR "/src/Person"
RUN dotnet build "PersonDocument.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PersonDocument.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PersonDocument.dll"]
