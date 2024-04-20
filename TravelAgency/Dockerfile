#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TravelAgency/TravelAgency.AgencyService.csproj", "TravelAgency/"]
COPY ["AgencyService.Adapter.RestAPI/AgencyService.Adapter.API.csproj", "AgencyService.Adapter.RestAPI/"]
COPY ["AgencyService.Core.Application/AgencyService.Core.Application.csproj", "AgencyService.Core.Application/"]
COPY ["AgencyService.Core.Domain/AgencyService.Core.Domain.csproj", "AgencyService.Core.Domain/"]
COPY ["AgencyService.Adapter.SQLServer/AgencyService.Adapter.SQLServer.csproj", "AgencyService.Adapter.SQLServer/"]
RUN dotnet restore "TravelAgency/TravelAgency.AgencyService.csproj"
COPY . .
WORKDIR "/src/TravelAgency"
RUN dotnet build "TravelAgency.AgencyService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TravelAgency.AgencyService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TravelAgency.AgencyService.dll"]