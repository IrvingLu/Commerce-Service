#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MicroService/Product/Product.Web/Product.Web.csproj", "MicroService/Product/Product.Web/"]
COPY ["MicroService/Product/Product.Infrastructure/Product.Infrastructure.csproj", "MicroService/Product/Product.Infrastructure/"]
COPY ["MicroService/Product/Product.Core/Product.Core.csproj", "MicroService/Product/Product.Core/"]
COPY ["Shared/Shared.Infrastructure.Core/Shared.Infrastructure.Core.csproj", "Shared/Shared.Infrastructure.Core/"]
COPY ["Shared/Shared.Domain.Abstractions/Shared.Domain.Abstractions.csproj", "Shared/Shared.Domain.Abstractions/"]
RUN dotnet restore "MicroService/Product/Product.Web/Product.Web.csproj"
COPY . .
WORKDIR "/src/MicroService/Product/Product.Web"
RUN dotnet build "Product.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Product.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Product.Web.dll"]