#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Minave.API/Minave.API.csproj", "Minave.API/"]
RUN dotnet restore "Minave.API/Minave.API.csproj"
COPY . .
WORKDIR "/src/Minave.API"
RUN dotnet build "Minave.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Minave.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5000
ENTRYPOINT ["dotnet", "Minave.API.dll"]