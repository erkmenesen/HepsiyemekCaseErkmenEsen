FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/HepsiyemekCaseErkmenEsen.Shared/*.csproj Shared/HepsiyemekCaseErkmenEsen.Shared/
COPY Services/Menu/HepsiyemekCaseErkmenEsen.Services.Menu/*.csproj Services/Menu/HepsiyemekCaseErkmenEsen.Services.Menu/
RUN dotnet restore Services/Menu/HepsiyemekCaseErkmenEsen.Services.Menu/*.csproj
COPY . .
RUN dotnet publish Services/Menu/HepsiyemekCaseErkmenEsen.Services.Menu/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","HepsiyemekCaseErkmenEsen.Services.Menu.dll" ]