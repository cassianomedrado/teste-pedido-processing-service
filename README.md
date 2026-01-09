# teste-pedido-processing-service
📦 Pedido Processing Service

Microserviço de processamento de pedidos desenvolvido em ASP.NET Core, seguindo princípios de Clean Architecture, com persistência em PostgreSQL, mensageria com RabbitMQ e Entity Framework Core para acesso a dados.

🏗️ Arquitetura

O projeto segue uma separação clara de responsabilidades:

src/
 ├── PedidosProcessamento.WebApi          → API / Startup
 ├── PedidosProcessamento.Application     → Casos de uso, DTOs, Validators
 ├── PedidosProcessamento.Domain          → Entidades e regras de domínio
 └── PedidosProcessamento.Infrastructure → EF Core, Repositórios, RabbitMQ

🚀 Tecnologias utilizadas

.NET 10

ASP.NET Core Web API

Entity Framework Core

PostgreSQL

RabbitMQ

Docker & Docker Compose

FluentValidation

Swagger (Swashbuckle)

📋 Pré-requisitos

Antes de executar o projeto, certifique-se de ter instalado:

.NET SDK 10

Docker Desktop

CLI do Entity Framework:

dotnet tool install --global dotnet-ef


ou

dotnet tool update --global dotnet-ef

⚙️ Configuração do ambiente
🔹 appsettings.json (WebApi)

Arquivo:
src/PedidosProcessamento.WebApi/appsettings.json

{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=pedidos;Username=postgres;Password=postgres"
  },
  "RabbitMQ": {
    "Host": "localhost",
    "Port": 5672,
    "Username": "guest",
    "Password": "guest"
  }
}

🐳 Subindo dependências com Docker

Na raiz do projeto, execute:

docker compose up -d


Isso irá subir:

PostgreSQL (porta 5432)

RabbitMQ (porta 5672)

RabbitMQ Management UI (http://localhost:15672)

Credenciais padrão do RabbitMQ:

Usuário: guest

Senha: guest

🗄️ Executando migrations do banco
1️⃣ Criar a migration (caso ainda não exista)
dotnet ef migrations add InitialCreate \
  --project src/PedidosProcessamento.Infrastructure \
  --startup-project src/PedidosProcessamento.WebApi \
  --output-dir Persistence/Migrations

2️⃣ Aplicar a migration no banco
dotnet ef database update \
  --project src/PedidosProcessamento.Infrastructure \
  --startup-project src/PedidosProcessamento.WebApi


⚠️ Observação:
Em ambiente local com Docker, é normal aparecer um aviso de lock ou leitura inicial do __EFMigrationsHistory.
Se o comando finalizar com Done, a migration foi aplicada corretamente.

▶️ Executando a aplicação

Na raiz do projeto:

dotnet run --project src/PedidosProcessamento.WebApi


A API ficará disponível em:

https://localhost:7xxx
http://localhost:5xxx


(os números de porta podem variar)

📑 Swagger

Após subir a aplicação, acesse:

https://localhost:7xxx/swagger


Você poderá:

Criar pedidos

Validar payloads

Testar a API facilmente

📬 Mensageria (RabbitMQ)

Ao criar um pedido:

O pedido é persistido no banco

Um evento PedidoCriado é publicado na fila:

pedido-criado


Você pode acompanhar as mensagens em:

http://localhost:15672

🧪 Fluxo principal

Requisição HTTP para POST /api/pedidos

Validação com FluentValidation

Criação da entidade de domínio

Persistência no PostgreSQL

Publicação de evento no RabbitMQ

🛠️ Comandos úteis
Listar migrations
dotnet ef migrations list \
  --project src/PedidosProcessamento.Infrastructure \
  --startup-project src/PedidosProcessamento.WebApi

Remover última migration
dotnet ef migrations remove \
  --project src/PedidosProcessamento.Infrastructure \
  --startup-project src/PedidosProcessamento.WebApi
