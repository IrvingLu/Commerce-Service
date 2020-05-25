#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MicroService/Sales/Sales.Web/Sales.Web.csproj", "MicroService/Sales/Sales.Web/"]
COPY ["MicroService/Sales/Sales.Infrastructure/Sales.Infrastructure.csproj", "MicroService/Sales/Sales.Infrastructure/"]
COPY ["MicroService/Sales/Sales.Core/Sales.Core.csproj", "MicroService/Sales/Sales.Core/"]
COPY ["Shared/Shared.Infrastructure.Core/Shared.Infrastructure.Core.csproj", "Shared/Shared.Infrastructure.Core/"]
COPY ["Shared/Shared.Domain.Abstractions/Shared.Domain.Abstractions.csproj", "Shared/Shared.Domain.Abstractions/"]
RUN dotnet restore "MicroService/Sales/Sales.Web/Sales.Web.csproj"
COPY . .
WORKDIR "/src/MicroService/Sales/Sales.Web"
RUN dotnet build "Sales.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sales.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sales.Web.dll"]