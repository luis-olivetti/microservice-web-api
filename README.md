# microservice-web-api

### Dependências
- MassTransit (https://masstransit.io/)
- Ocelot (https://github.com/ThreeMammals/Ocelot)
- SQL Server 2017
- Entity Framework
- RabbitMQ

### Execução somente via contêineres

Este projeto foi configurado para executar entre contêineres, por este motivo algumas configurações de *Host* estão com o nome do contêiner ao invés de *localhost* ou algum *IP*.

Para iniciá-lo, execute: </br>
`docker-compose up -d --build`

### Execução sem uso de contêiner (exceto algumas dependências)

Como este projeto necessita do RabbitMQ e do SQL Server, será preciso iniciar os contêineres deles.

Já para os microsserviços e o gateway, utilize o run de cada um deles com o profile *Debug*: </br>
`dotnet run --launch-profile "Debug"`

#### Configuração de ambiente

Instalação do SDK .NET 3.1
`sudo apt install dotnet-sdk-3.1`

Instalação do Entity Framework
`dotnet tool install --global dotnet-ef`

### RabbitMQ

Para acessar o painel do RabbitMQ, utilize os seguintes valores:

**Usuário**: guest
**Senha**: guest

Endereço do painel:
http://localhost:15672/#/

### Como funciona?

Este projeto foi executado em um *SO Ubuntu* com limitações de *sudo*, por esse motivo não utiliza as configurações do *iisExpress* (Windows) que estão nos arquivos *launchSettings.json*.

O **API Gateway** está exposto em HTTP na **porta 5007**.
Ele se comunica com o microsserviço **Product** que está exposto em HTTP na **porta 5003**.
E também se comunica com o microsserviço **Customer** que está em HTTP na **porta 5005**.

Exemplos de uso através do gateway:

*GET* http://localhost:5007/gateway/product

*GET* http://localhost:5007/gateway/customer

---
Swagger dos microsserviços:

http://localhost:5003/swagger/index.html (Product)

http://localhost:5005/swagger/index.html (Customer)

### Dicas

Para executar o gateway e os microsserviços via *VSCODE*, abra uma nova aba do terminal para cada um dos projetos e execute `dotnet run --launch-profile "Debug"`.

Para criar um novo projeto, especifique a versão do framework 3.1:
`dotnet new classlib -f netcoreapp3.1 -o NovoProjeto`

Para adicionar referência de um projeto em outro:
`dotnet add reference ../Project1.Api/Project1.Api.csproj`

### Referências utilizadas para o projeto

O código é o mesmo deste artigo, mas modificado para executar em HTTP e um ajuste para carregar o xml do Swagger:
https://codewithmukesh.com/blog/microservice-architecture-in-aspnet-core/

Utilizado para configurar o SQL Server:
https://medium.com/bright-days/basic-docker-image-dockerfile-sql-server-with-custom-prefill-db-script-8f12f197867a

Para utilizar em HTTPS:
https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-7.0&tabs=visual-studio%2Clinux-ubuntu

Mensageria:
https://www.macoratti.net/21/04/net_masstrans1.htm
https://codewithmukesh.com/blog/rabbitmq-with-aspnet-core-microservice/

Executar migrations:
https://jasontaylor.dev/ef-core-database-initialisation-strategies/