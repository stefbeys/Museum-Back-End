FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["MuseumBack.csproj", ""]
RUN dotnet restore "./MuseumBack.csproj"
COPY . .
COPY ./assets /app/assets
WORKDIR "/src/."
RUN dotnet build "MuseumBack.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MuseumBack.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MuseumBack.dll"]