FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
RUN apt-get update && apt-get install -y libgssapi-krb5-2 && rm -rf /var/lib/apt/lists/*

USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OrderService/OrderService.Presentation.csproj", "OrderService/"]
COPY ["OrderService.Application/OrderService.Application.csproj", "OrderService.Application/"]
COPY ["OrderService.Domain/OrderService.Domain.csproj", "OrderService.Domain/"]
COPY ["OrderService.Infrastructure/OrderService.Infrastructure.csproj", "OrderService.Infrastructure/"]
COPY ["BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/"]
RUN dotnet restore "./OrderService/OrderService.Presentation.csproj"
COPY . .
WORKDIR "/src/OrderService"
RUN dotnet build "./OrderService.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OrderService.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderService.Presentation.dll"]
