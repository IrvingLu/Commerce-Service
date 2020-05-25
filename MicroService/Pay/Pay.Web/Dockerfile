#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MicroService/Pay/Pay.Web/Pay.Web.csproj", "MicroService/Pay/Pay.Web/"]
COPY ["MicroService/Pay/Pay.Infrastructure/Pay.Infrastructure.csproj", "MicroService/Pay/Pay.Infrastructure/"]
COPY ["MicroService/Pay/Pay.Core/Pay.Core.csproj", "MicroService/Pay/Pay.Core/"]
COPY ["Shared/Shared.Infrastructure.Core/Shared.Infrastructure.Core.csproj", "Shared/Shared.Infrastructure.Core/"]
COPY ["Shared/Shared.Domain.Abstractions/Shared.Domain.Abstractions.csproj", "Shared/Shared.Domain.Abstractions/"]
RUN dotnet restore "MicroService/Pay/Pay.Web/Pay.Web.csproj"
COPY . .
WORKDIR "/src/MicroService/Pay/Pay.Web"
RUN dotnet build "Pay.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pay.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pay.Web.dll"]