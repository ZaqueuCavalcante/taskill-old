# Taskill

A simple TODO app, made with ASP.NET, PostgreSQL and React.

Fiz esse projeto para servir de portifólio, consolidar meus conhecimentos e ajudar quem tá começando na área de desenvolvimento.

- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

## Backend

### Features

- Taskillers:
  - Criar usuário
  - Fazer login
- Tasks:
  - Criar uma task
  - Adicionar sub-tasks a uma task
  - Alterar uma task (título, descrição, prioridade, projeto, data de entrega, labels...)
  - Marcar / desmarcar como concluída
  - Visualizar uma ou várias
  - Buscar tasks por palavra no título ou descrição
  - Mudar a ordem delas dentro de um projeto
- Projetos:
  - Criar um projeto
  - Alterar o nome
  - Visualizar um ou vários
- Labels:
  - Criar uma label
  - Alterar o nome
  - Visualizar uma ou várias

### Conceitos e Tecnologias

- ASP.NET para criação da API
- PostgreSQL como banco de dados
- Identity para autenticação com JWT
- Entity Framework como ORM
- Swagger para documentação
- Conceitos de DDD para modelagem do domínio
- NUnit + FluentAssertions para testes automatizados

### Future Features

- Sub-tasks to tasks
- Project views (list / board) and sections
- Reminders para alertar vencimento do prazo de entrega (Hangfire?) (Premium Users?) (WebSocket?)
- Adicionar eventos / histórico de atividades (Activity Log)
- Relatórios de produtividade
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
