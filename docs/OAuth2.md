# OAuth2 and OpenID Connect

Tem muito mais coisa além disso, mas vamos focar em resolver um problema apenas: logar no Taskill usando uma conta do Google. (Usando o Authorization Code Flow do OpenID Connect)

- OAuth2 serve pra dar acesso granular (**autorizar**) a recursos.
    - Delegated Authorization
- OpenID Connect serve para **autenticar** um usuário. [FOCO_DO_POST]
    - Simple Login
    - Single Sign-On Across Sites
    - Mobile App Login

## Que problema vamos resolver?

- Hoje, o Taskill permite a criação de um novo usuário, basta informar um e-mail e uma senha forte. Depois é só fazer login, receber um JWT e usá-lo nas demais requisições para interagir com a API.
- Mas eu já tô logado na minha conta do Google, seria legal ter a opção de logar no Taskill com ela, já que isso é bem comum em várias outras aplicações.
- Então vamos ter que, de alguma maneira segura, dar ao Taskill a permissão de ir lá no servidor do Google e pegar algumas informações do meu perfil. Com isso, poderei logar nele.

## O que são?

- Identity and Access Management (IAM)

- Autenticação:
    - Processo de **verificar a identidade** (autenticidade) de um usuário ou aplicação.
    - Quando você faz login em algum site ou app, está se autenticando.
    - Quando desbloqueia o celular com a digital, também.
    - Pra acessar a AWS, precisa de MFA.
    - Quando integramos sistemas via API, geralmente tem um token de autenticação envolvido também.

- Autorização:
    - Processo de **dar permissão de acesso à um recurso** a um usuário ou aplicação.
    - Esse acesso pode ser de criar um novo recurso, editar um existente, apagar...
    - Podem existir políticas de autorização ou roles para gerenciar essas permissões, além de coisas mais complexas, como Attribute-Based Access Control (ABAC) e Relationship-Based Access Control (ReBAC).
    - Geralmente está relacionado com o processo de autenticação. Por emxemplo, quando logamos num app, o backend pode gerar um token com claims que autorizam o usuário à acessar determinados recursos.

- OpenID Connect:
    - Focado em autenticação

- OAuth2:
    - Focado em autorização
    - Is an open standard for delegated authorization.
    - Delegated authorization is the process of giving a service or application
permission to access a user’s resource on their behalf.
    - As an example, consider when you give LinkedIn the ability to publish your post on Twitter as well. In this case, you authorize LinkedIn to access your Twitter profile and post a tweet on your behalf. In other words, you delegate LinkedIn to tweet for you.
    - Antes do OAuth, isso era feito dando username e password pro LinkedIn, que tinha acesso total ao seu Twitter.
    - OAuth solves this problem by proposing a clear architecture and some **authorization flows** for specific scenarios.
    - Atores:
        - Resource Owner: tipicamente o usuário, que tem acesso total ao recurso. Nesse caso, sou eu.
        - Client: a aplicação que quer acesso ao recurso. No caso, o Taskill.
        - Resource Server: a aplicação que controla o acesso ao recurso. No caso, o servidor do Google que armazena meu profile.
        - Authorization Server: is the intermediary between the above actors. It receives authorization requests from the client, asks for approval from the user, shares with the resource server details about the authorization process, and so on.
    - Elementos:
        - Consent Screen: When the authorization server receives a request to access a resource from the client, it asks the user for confirmation. It shows a screen with the details of the request: the requesting application, the requested resource, and the requested operations on that resource. The user has the ability to grant or deny that access.
        - Access Token. Once the user grants access to the client application, the authorization server provides it with an access token. This is a string that authorizes the client to access the resource on behalf of the user. While the format of the access token is not defined by OAuth specifications, a widely adopted format is JSON Web Token (JWT).
        - Scopes: delegated authorization allows the user to **restrict the set of operations** that a client can perform on a resource. Scopes are strings that represent operations that can be performed on a resource. They are similar to permissions but have a different semantic in the delegated authorization context.

## Flows

- • Authorization Code Flow. This flow is usually used with regular web
applications. It relies on the exchange of an authorization code for the
access token between the client and the authorization server.
• Authorization Code Flow with PKCE. This flow is similar to the
Authorization Code Flow but has one more security measure: the use of
a Proof Key for Code Exchange (PKCE) — that is, a piece of information
that makes sure the receiver of the token is the original requester. This
flow is used when the client is a Single-Page Application (SPA) or a
native application.

- 0 - Na tela inicial do Taskill, o Mr. White clica em "Logar com o Google".
- 1 - O Taskill chama o Google Auth, pedindo acesso aos recursos do Mr. White que estão no Google Accounts. (Quais dados vão no request?)
- 2 - O Google Auth pergunta pro Mr. White, via Consent Screen, se ele deixa o Taskill acessar os recursos.
- 3 - O Mr. White consente.
- 4 - O Google Auth emite um token (JWT) e o envia pro Taskill.
- 5 - O Taskill usa o token pra acessar os dados do Mr. White no Google Accounts.
- 6 - O Taskill faz o que com esses dados????

## OpenID Connect

- Protocolo de autenticação, construído em cima do OAuth2.
- It allows applications to verify the identity of a user and optionally receive basic profile information.
- ID Token (JWT)
- 







## Que problemas resolvem?

## Como funcionam?

-

## References

- [OAuth 2.0 and OpenID Connect in Plain English! - Nate Barbettini - PADNUG](https://youtu.be/0VWkQMr7r_c)
- [OAuth Debugger](https://oauthdebugger.com/)
- [Asp.Net Google Login](https://mahdikarimipour.com/blog/google-auth-for-react-with-aspnet-identity)
- [OAuth Playground](https://www.oauth.com/playground)
- [ASP.NET Core Identity Series – OAuth 2.0, OpenID Connect & IdentityServer](https://chsakell.com/2019/03/11/asp-net-core-identity-series-oauth-2-0-openid-connect-identityserver/)
- [An Illustrated Guide to OAuth and OpenID Connect](https://youtu.be/t18YB3xDfXI)

https://ml-software.ch/posts/switching-google-authentication-to-oidc-in-asp-net-core-2-2
https://andrewlock.net/an-introduction-to-openid-connect-in-asp-net-core/

## Topics

- Auth0
- Identity and Access Management (IAM)
- Authentication and Authorization
- 
