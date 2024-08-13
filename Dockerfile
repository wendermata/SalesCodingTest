FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY src/WebApi/WebApi.csproj ./src/WebApi/
RUN dotnet restore src/WebApi/WebApi.csproj

COPY . .
WORKDIR /app/src/WebApi
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/src/WebApi/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "WebApi.dll"]