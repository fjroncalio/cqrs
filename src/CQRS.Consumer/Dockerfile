#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CQRS.Consumer/CQRS.Consumer.csproj", "CQRS.Consumer/"]
COPY ["CQRS.Domain/CQRS.Domain.csproj", "CQRS.Domain/"]
COPY ["CQRS.Infra.Repository/CQRS.Infra.Repository.csproj", "CQRS.Infra.Repository/"]
RUN dotnet restore "CQRS.Consumer/CQRS.Consumer.csproj"
COPY . .
WORKDIR "/src/CQRS.Consumer"
RUN dotnet build "CQRS.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CQRS.Consumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CQRS.Consumer.dll"]