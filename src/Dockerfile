FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS sdk
WORKDIR /app

COPY SharedModel/*.csproj ./SharedModel/
RUN dotnet restore ./SharedModel/SharedModel.csproj

COPY Customer.Microservice/*.csproj ./Customer.Microservice/
RUN dotnet restore ./Customer.Microservice/Customer.Microservice.csproj

COPY SharedModel/ ./SharedModel/
RUN dotnet publish ./SharedModel/SharedModel.csproj -c Release -o microservice

COPY Customer.Microservice/ ./Customer.Microservice/
RUN dotnet publish ./Customer.Microservice/Customer.Microservice.csproj -c Release -o microservice

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
ENV ASPNETCORE_URLS=http://+:5005
WORKDIR /app
EXPOSE 5005
COPY --from=sdk /app/microservice .
ENTRYPOINT [ "dotnet", "Customer.Microservice.dll", "--launch-profile LocalDev" ]