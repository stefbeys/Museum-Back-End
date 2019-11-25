FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["MuseumBack.csproj", ""]
COPY ["../MuseumBack.Models/MuseumBack.Models.csproj", "../MuseumBack.Models/"]
RUN dotnet restore "./MuseumBack.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "MuseumBack.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MuseumBack.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MuseumBack.dll"]