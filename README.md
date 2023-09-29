# Scheduler API

Este é o repositório do back-end do Sistema de Agendamento, uma aplicação construída com .NET 6.0 (C#) para lidar com as operações de agendamento de uma empresa que presta serviços.

## Recursos

- API RESTful para gerenciar agendamentos e serviços
- Suporte a CRUD (Create, Read, Update, Delete) de agendamentos e serviços
- Aviso via e-mail com 24h de antecedência
- Integração com o front-end para sincronização de dados

## Pré-requisitos

- .NET 6.0 SDK instalado
- Um banco de dados SQL Server configurado

## Como Iniciar

1. Clone este repositório:

   ```bash
   git clone https://github.com/brunoferreira94/scheduler-api.git
   ```

1. Navegue até o diretório do projeto:

   ```bash
   cd scheduler-api\src\Scheduler.Services.Host\
   ```

1. Configure a conexão com o banco de dados no arquivo `appsettings.json`.

1. Inicie o servidor. Ao iniciar, o Entity Framework irá criar as tabelas necessárias e popular com os dados pré-definidos:

   ```bash
   dotnet run
   ```

1. A API estará disponível em <https://localhost:7192/>

1. O Swagger estará disponível em <https://localhost:7192/swagger/index.html>

## Endpoints da API

- `/api/appointments`: Endpoint para agendamentos (CRUD)
- `/api/services`: Endpoint para serviços (CRUD)

## Como Contribuir

Se você deseja contribuir para este projeto, siga estas etapas:

- Faça um fork deste repositório.
- Crie uma branch para sua modificação: git checkout -b feature/sua-feature.
- Faça as alterações desejadas e faça commit delas: git commit -m 'Adicionar nova funcionalidade'.
- Envie as alterações para a branch principal do seu fork: git push origin feature/sua-feature.
- Abra uma solicitação pull neste repositório.

## Licença

Este projeto está licenciado sob a Licença MIT - consulte o arquivo LICENSE.md para obter detalhes.

## Contato

- Autor: Bruno Ferreira
- GitHub: [brunoferreira94](https://github.com/brunoferreira94/fis-api)
- Email: [contatobrunofs@gmail.com](mailto:contatobrunofs@gmail.com)
