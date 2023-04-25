FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS sdk
WORKDIR /app

COPY SharedModel/*.csproj ./SharedModel/
RUN dotnet restore ./SharedModel/SharedModel.csproj

COPY Product.Microservice/*.csproj ./Product.Microservice/
RUN dotnet restore ./Product.Microservice/Product.Microservice.csproj

COPY SharedModel/ ./SharedModel/
RUN dotnet publish ./SharedModel/SharedModel.csproj -c Release -o microservice

COPY Product.Microservice/ ./Product.Microservice/
RUN dotnet publish ./Product.Microservice/Product.Microservice.csproj -c Release -o microservice

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
ENV ASPNETCORE_URLS=http://+:5003
WORKDIR /app
EXPOSE 5003
COPY --from=sdk /app/microservice .
ENTRYPOINT [ "dotnet", "Product.Microservice.dll", "--launch-profile LocalDev" ]