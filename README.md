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


# Dica

Para executar o Gateway e os micro serviços via VSCODE, abra uma nova aba do terminal para cada um deles e execute `dotnet run`

# Referências

O código é o mesmo deste artigo, mas modificado para executar em HTTP e um ajuste para carregar o xml do Swagger:
https://codewithmukesh.com/blog/microservice-architecture-in-aspnet-core/

Utilizado para configurar o SQL Server:
https://medium.com/bright-days/basic-docker-image-dockerfile-sql-server-with-custom-prefill-db-script-8f12f197867a

Para utilizar em HTTPS:
https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-7.0&tabs=visual-studio%2Clinux-ubuntu