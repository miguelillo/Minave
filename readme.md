Commands:

## Create a Docker network

As we want to connect two containers, it is necessary to create a network, so that both containers have connection between them.

![alt text](https://github.com/miguelillo/Minave/blob/master/Docker%20network.png?raw=true)

```
docker network create -d bridge squad-for-fun-network
```


## Dockerize App

1. First of all, get noticed that we have a Minave.App folder

2. Open the terminal and go to the inside app

3. Now, we can create a Dockerfile:

```
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

```

4. Go to the console and build it
```
docker build -t manavarro/minave.app:1.0 .
```

5. Finally, Run it
```
docker run -itd -p 2000:80 --name minaveapp --network=squad-for-fun-network manavarro/minave.app:1.0
```

6. Test if it works!

```
http://localhost:2000/
```

7. Try to Fetch Data ¿Kapaxao?

We need the azure function ☢

## Dockerize Azure Function

1. First of all, get noticed that we have an functions folder

2. Open the terminal and go to the inside functions

3. Now, we can create a Dockerfile:

```
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS installer

COPY . /src/dotnet-function-app
WORKDIR /src/dotnet-function-app
RUN mkdir -p /home/site/wwwroot
RUN dotnet restore "Minave.Functions.csproj"
RUN dotnet build "Minave.Functions.csproj" -c Release -o /app/build

RUN dotnet publish *.csproj --output /home/site/wwwroot

FROM mcr.microsoft.com/azure-functions/dotnet:3.0
ENV AzureWebJobsScriptRoot=/home/site/wwwroot
ENV AzureFunctionsJobHost__Logging__Console__IsEnabled=true

COPY --from=installer ["/home/site/wwwroot", "/home/site/wwwroot"]
```


4. Go to the console and build it
```sh
docker build -t manavarro/minave.functions:1.0 .
```

5. Finally, Run it
```sh
docker run -itd -p 1500:80  --name minavefunctions --network=squad-for-fun-network manavarro/minave.functions:1.0
```
6. Test if it works!

```sh
http://localhost:1500/api/weatherforecastcalc
```

## Test Dockerized APP and Dockerized Function together!

1. Fetch data from : http://localhost:2000/

2. Go to Docker Desktop and see Function logs
