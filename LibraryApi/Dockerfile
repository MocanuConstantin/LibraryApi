#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LibraryApi/Library.Api.csproj", "LibraryApi/"]
COPY ["GoShop.Core/Library.Core.csproj", "GoShop.Core/"]
COPY ["GoShop.Domain/Library.Domain.csproj", "GoShop.Domain/"]
COPY ["GoShop.Data/Library.Data.csproj", "GoShop.Data/"]
RUN dotnet restore "./LibraryApi/Library.Api.csproj"
COPY . .
WORKDIR "/src/LibraryApi"
RUN dotnet build "./Library.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Library.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Library.Api.dll"]