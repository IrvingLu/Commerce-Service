#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MicroService/Identity/Identity.Web/Identity.Web.csproj", "MicroService/Identity/Identity.Web/"]
COPY ["MicroService/Identity/Identity.Infrastructure/Identity.Infrastructure.csproj", "MicroService/Identity/Identity.Infrastructure/"]
COPY ["MicroService/Identity/Identity.Core/Identity.Core.csproj", "MicroService/Identity/Identity.Core/"]
RUN dotnet restore "MicroService/Identity/Identity.Web/Identity.Web.csproj"
COPY . .
WORKDIR "/src/MicroService/Identity/Identity.Web"
RUN dotnet build "Identity.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.Web.dll"]