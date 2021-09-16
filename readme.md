Commands:

## Create a Docker network

As we want to connect two containers, it is necessary to create a network, so that both containers have connection between them.

![alt text](http://url/to/img.png)

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
EXPOSE 80
EXPOSE 443
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




docker build -t manavarro/minave.app:1.0 .

docker run -itd -p 2000:80 --name minaveapp --network=squad-for-fun-network manavarro/minave.app:1.0

docker build -t manavarro/minave.functions:1.0 .

docker run -itd -p 1500:80  --name minavefunctions --network=squad-for-fun-network manavarro/minave.functions:1.0