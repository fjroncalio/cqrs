#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CQRS.Api/CQRS.Api.csproj", "CQRS.Api/"]
COPY ["CQRS.Domain/CQRS.Domain.csproj", "CQRS.Domain/"]
COPY ["CQRS.Infra.Cache/CQRS.Infra.Cache.csproj", "CQRS.Infra.Cache/"]
COPY ["CQRS.Infra.Repository/CQRS.Infra.Repository.csproj", "CQRS.Infra.Repository/"]
RUN dotnet restore "CQRS.Api/CQRS.Api.csproj"
COPY . .
WORKDIR "/src/CQRS.Api"
RUN dotnet build "CQRS.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CQRS.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CQRS.Api.dll"]