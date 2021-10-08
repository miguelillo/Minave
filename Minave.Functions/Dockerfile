#SQUAD FOR FUN

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
