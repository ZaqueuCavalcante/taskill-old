# Taskill Backend

The Taskill Backend API.

## Features

- Taskillers:
  - Criar usuário
  - Fazer login
- Tasks:
  - Criar uma task
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

## Future Features

- Sub-tasks to tasks
- Project views (list / board) and sections
- Reminders para alertar vencimento do prazo de entrega
- Adicionar eventos / histórico de atividades (Activity Log)
- Relatórios de produtividade
- Adicionar suporte a times (roles e policies para cada ação)
- Testes de mutação
- Login com OAuth 2.0, OpenID Connect
- CI/CD pipeline no GitHub
- Subir o projeto com Docker + Compose

## POST

- DDD para modelagem do domónio
- EF Core para modelagem relacional e para interação com o banco de dados (Postgres)
- Full Text Search com EF Core + Postgres
- Identity para autenticação com JWT
- Documentação com o Swagger
- Testes unitários e de integração, usando NUnit + FluentAssertions
- TDD para o algoritmo de reordenação das tasks dentro de um projeto
- Strongly typed classes to represent configuration information
