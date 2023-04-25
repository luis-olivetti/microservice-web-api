FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS sdk
WORKDIR /app

COPY Gateway.WebApi/*.csproj ./Gateway.WebApi/
RUN dotnet restore ./Gateway.WebApi/Gateway.WebApi.csproj

COPY Gateway.WebApi/ ./Gateway.WebApi/
RUN dotnet publish ./Gateway.WebApi/Gateway.WebApi.csproj -c Release -o microservice

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
ENV ASPNETCORE_URLS=http://+:5007
WORKDIR /app
EXPOSE 5007
COPY --from=sdk /app/microservice .
ENTRYPOINT [ "dotnet", "Gateway.WebApi.dll", "--launch-profile LocalDev" ]