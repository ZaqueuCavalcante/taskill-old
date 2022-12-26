# Taskill

A simple TODO app, made with ASP.NET, PostgreSQL and React.

Fiz esse projeto para ajudar quem tá começando na área de desenvolvimento, consolidar meus conhecimentos e servir de portfólio.

- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

## Backend

### Conceitos e Tecnologias

- ASP.NET para criação da API
- PostgreSQL como banco de dados
- Identity para autenticação com JWT
- Entity Framework como ORM
- Swagger para documentação
- Conceitos de DDD para modelagem do domínio
- NUnit + FluentAssertions para testes automatizados

### Funcionalidades

- Usuários:
  - Criar um novo, fazer login, adicionar usuário ao plano Premium
- Tasks:
  - Criar uma task
  - Adicionar sub-tasks a uma task
  - Alterar uma task (título, descrição, prioridade, projeto, data de entrega, labels...)
  - Marcar / desmarcar como concluída
  - Adicionar um lembrete para ser avisado da data de entrega da task
  - Visualizar uma ou várias
  - Buscar tasks por palavra no título ou descrição
- Projetos:
  - Criar um projeto, alterar seu nome, visualizar um ou vários
  - Criar uma ou mais seções dentro de um projeto
  - Alterar o layout de visualização das seções dentro de um projeto
  - Alterar a ordem de exibição das tasks dentro de um projeto ou seção
  - Mover uma task de uma seção para outra
- Labels:
  - Criar uma label, alterar seu nome, visualizar uma ou várias

### Backlog

- Adicionar eventos / histórico de atividades
- Relatórios de produtividade (Premium Plan)
- Usar o Bogus para gerar dados nos testes
- Testes de mutação
- Login com OAuth 2.0, OpenID Connect
- CI/CD pipeline no GitHub
- Subir o projeto com Docker + Compose

- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

## Frontend

### Conceitos e Tecnologias

- React
- Material UI
- Gerenciamento de Estado
