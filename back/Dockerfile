FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
COPY . /src
WORKDIR /src

RUN dotnet restore --packages .nuget/packages
RUN dotnet build -c Release --no-restore

FROM build AS publish
RUN dotnet publish -c Release -o /app --no-build

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY --from=build /src/Src/appsettings.json appsettings.json
COPY --from=build /src/Src/appsettings.Development.json appsettings.Development.json

ENTRYPOINT ["dotnet", "Src.dll"]
