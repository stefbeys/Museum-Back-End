FROM mcr.microsoft.com/dotnet/core/runtime:2.2
COPY Release/ ./app/
ENTRYPOINT ["dotnet", "app/MuseumBack.dll"]