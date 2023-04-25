# microservice-web-api

# Configuração

Instalação do .NET 3.1
`sudo apt install dotnet-sdk-3.1`

Instalação do Entity Framework
`dotnet tool install --global dotnet-ef`

# Configuração do banco de dados

Foi utilizado esta imagem docker do banco SQL Server:
`docker pull mcr.microsoft.com/mssql/server:2017-latest`

Usuário: sa
Senha: abcDEF123#

```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=abcDEF123#" \
-p 1433:1433 --name sqlsv \
-d mcr.microsoft.com/mssql/server:2017-latest
```

# Configuração do RabbitMQ

Para criar um container com nome some-rabbit-admin, expondo a porta 5672 para comunicação AMQP ou 15672 para utilizar o porta de administração:
`docker run -d --hostname my-rabbit --name some-rabbit-admin -p 5672:5672 -p 15672:15672 rabbitmq:3.11-management`

Usuário: guest
Senha: guest

Endereço do painel:
http://localhost:15672/#/

Mais infos:
https://hub.docker.com/_/rabbitmq

# Execução da migrations

No projeto *Product* e *Customer*, execute:
`dotnet ef database update`

# Como funciona?

Este projeto foi executado em uma máquina Ubuntu com limitações de sudo, por esse motivo não utiliza as configurações do *iisExpress* que estão nos arquivos *launchSettings.json*.

O Gateway está exposto em HTTP na porta 5007.
Ele se comunica com o micro serviço *Product* que está exposto em HTTP na porta 5003.
E também se comunica com o micro serviço *Customer* que está em HTTP na porta 5005.

Exemplos de uso:

*GET* http://localhost:5007/gateway/product

*GET* http://localhost:5007/gateway/customer

Swagger:

http://localhost:5003/swagger/index.html

http://localhost:5005/swagger/index.html

# Dica

Para executar o Gateway e os micro serviços via VSCODE, abra uma nova aba do terminal para cada um deles e execute `dotnet run`

Para criar um novo projeto, especificar a versão do framework
`dotnet new classlib -f netcoreapp3.1 -o NovoProjeto`

Para adicionar referência de um projeto em outro:
`dotnet add reference ../Project1.Api/Project1.Api.csproj`

# Referências

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