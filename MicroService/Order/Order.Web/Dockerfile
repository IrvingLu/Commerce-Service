#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MicroService/Order/Order.Web/Order.Web.csproj", "MicroService/Order/Order.Web/"]
COPY ["MicroService/Order/Order.Infrastructure/Order.Infrastructure.csproj", "MicroService/Order/Order.Infrastructure/"]
COPY ["MicroService/Order/Order.Core/Order.Core.csproj", "MicroService/Order/Order.Core/"]
COPY ["Shared/Shared.Infrastructure.Core/Shared.Infrastructure.Core.csproj", "Shared/Shared.Infrastructure.Core/"]
COPY ["Shared/Shared.Domain.Abstractions/Shared.Domain.Abstractions.csproj", "Shared/Shared.Domain.Abstractions/"]
RUN dotnet restore "MicroService/Order/Order.Web/Order.Web.csproj"
COPY . .
WORKDIR "/src/MicroService/Order/Order.Web"
RUN dotnet build "Order.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Web.dll"]