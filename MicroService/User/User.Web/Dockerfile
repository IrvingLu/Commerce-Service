#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MicroService/User/User.Web/User.Web.csproj", "MicroService/User/User.Web/"]
COPY ["MicroService/User/User.Infrastructure/User.Infrastructure.csproj", "MicroService/User/User.Infrastructure/"]
COPY ["MicroService/User/User.Core/User.Core.csproj", "MicroService/User/User.Core/"]
COPY ["Shared/Shared.Infrastructure.Core/Shared.Infrastructure.Core.csproj", "Shared/Shared.Infrastructure.Core/"]
COPY ["Shared/Shared.Domain.Abstractions/Shared.Domain.Abstractions.csproj", "Shared/Shared.Domain.Abstractions/"]
RUN dotnet restore "MicroService/User/User.Web/User.Web.csproj"
COPY . .
WORKDIR "/src/MicroService/User/User.Web"
RUN dotnet build "User.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.Web.dll"]