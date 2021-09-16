FROM mcr.microsoft.com/dotnet/sdk:5.0 AS dotnet-build
WORKDIR /src
COPY . /src
RUN curl -sL https://deb.nodesource.com/setup_12.x |  bash -
RUN apt-get install -y nodejs
RUN dotnet restore "Minave.App.csproj"
RUN dotnet build "Minave.App.csproj" -c Release -o /app/build

FROM dotnet-build AS dotnet-publish
RUN dotnet publish "Minave.App.csproj" -c Release -o /app/publish

FROM node AS node-builder
WORKDIR /node
COPY ./ClientApp /node
RUN npm install
RUN npm run-script build

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS final
WORKDIR /app
RUN mkdir /app/wwwroot
COPY --from=dotnet-publish /app/publish .
COPY --from=node-builder /node/build ./wwwroot
ENTRYPOINT ["dotnet", "Minave.App.dll"]