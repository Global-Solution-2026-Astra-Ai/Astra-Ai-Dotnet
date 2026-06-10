# 🚀 Astra AI Dotnet — Plataforma de Comercialização de Energia Espacial

> Projeto desenvolvido em ASP.NET Core para gerenciamento de clientes premium, leilões de energia transmitida por satélites e registro de transações comerciais.

A aplicação implementa uma arquitetura em camadas utilizando Entity Framework Core com Oracle Database, permitindo o gerenciamento completo do ciclo de comercialização de energia, desde o cadastro de clientes até a finalização de leilões e geração de registros transacionais.

---

# 👥 Integrantes

| Nome               | RM       |
| ------------------ | -------- |
| Arthur Graciani    | RM561728 |
| João Pedro Scarpin | RM565421 |
| Wesley Andrade     | RM563593 |
| Lucas Hideki       | RM565355 |
| Gustavo Oliveira   | RM566358 |

---

# 🛠️ Tecnologias Utilizadas

| Tecnologia                 | Versão      | Finalidade                            |
| -------------------------- | ----------- | ------------------------------------- |
| .NET                       | 10          | Plataforma principal da aplicação     |
| ASP.NET Core               | 10          | Framework para construção da API REST |
| Entity Framework Core      | 10.0.8      | ORM para persistência de dados        |
| Oracle.EntityFrameworkCore | 10.23.26200 | Provider Oracle para EF Core          |
| Oracle Database            | 19c+        | Banco de dados relacional             |
| Swagger / OpenAPI          | 10.1.7      | Documentação interativa da API        |
| Dependency Injection       | Nativo .NET | Inversão de controle                  |
| LINQ                       | Nativo .NET | Consultas e manipulação de dados      |

---

# 📐 Arquitetura da Solução

A aplicação segue o padrão de arquitetura em camadas:

```text
Controllers
    ↓
Services
    ↓
Repositories
    ↓
Entity Framework Core
    ↓
Oracle Database
```

### Fluxo de Requisição

```text
Cliente HTTP
      ↓
Controller
      ↓
Service
      ↓
Repository
      ↓
DbContext
      ↓
Oracle Database
```

---

# 📂 Estrutura do Projeto

```text
Astra-Ai-Dotnet
│
├── ClientesPremium
│   ├── Controllers
│   │   └── ClientesPremiumController.cs
│   ├── DTOs
│   │   ├── ClientePremiumRequest.cs
│   │   └── ClientePremiumResponse.cs
│   ├── Models
│   │   └── ClientePremium.cs
│   ├── Repositories
│   │   └── Interfaces
│   │       └── IClientePremiumRepository.cs
│   │   └── Implementations
│   │       └── ClientePremiumRepository.cs
│   └── Services
│       └── ClientePremiumService.cs
│
├── Leiloes
│   ├── Controllers
│   │   └── LeiloesController.cs
│   ├── DTOs
│   │   ├── LeilaoRequest.cs
│   │   └── LeilaoResponse.cs
│   ├── Models
│   │   └── Leilao.cs
│   ├── Repositories
│   │   └── Interfaces
│   │       └── ILeilaoRepository.cs
│   │   └── Implementations
│   │       └── LeilaoRepository.cs
│   └── Services
│       └── LeilaoService.cs
│
├── LogTransacoes
│   ├── DTOs
│   │   ├── LogTransacaoRequest.cs
│   │   └── LogTransacaoResponse.cs
│   ├── Models
│   │   └── LogTransacao.cs
│   ├── Repositories
│   │   └── Interfaces
│   │       └── ILogTransacaoRepository.cs
│   │   └── Implementations
│   │       └── LogTransacaoRepository.cs
│   └── Services
│       └── LogTransacaoService.cs
├── Data
│   └── AppDbContext.cs
│
├── Migrations
│
├── appsettings.json
│
└── Program.cs
```

---

# ⚙️ Configuração do Ambiente

## Pré-requisitos

* .NET SDK 10
* Oracle Database 19c ou superior
* Visual Studio 2022
* VS Code (opcional)
* Git

---

## Clonar o Repositório

```bash
git clone https://github.com/seu-repositorio/astra-ai-dotnet.git
cd astra-ai-dotnet
```

---

## Instalar Dependências

```bash
dotnet restore
```

Caso necessário:

```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Oracle.EntityFrameworkCore
dotnet add package Swashbuckle.AspNetCore
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

---

## Configuração da Connection String

Arquivo:

```json
appsettings.json
```

```json
{
  "ConnectionStrings": {
    "OracleConnection": "User Id=USUARIO;Password=SENHA;Data Source=HOST:1521/SERVICE_NAME;"
  }
}
```

---

## Ferramenta EF Core

Instalar:

```bash
dotnet tool install --global dotnet-ef
```

Verificar versão:

```bash
dotnet ef --version
```

---

## Executar Migrations

Criar migration:

```bash
dotnet ef migrations add InitialCreate
```

Aplicar migration:

```bash
dotnet ef database update
```

---

## Executar Aplicação

```bash
dotnet run
```

Exemplo:

```text
Now listening on: https://localhost:5270
```

---

# 📖 Swagger

Após iniciar a aplicação:

```text
https://localhost:{porta}/swagger
```

A documentação OpenAPI será gerada automaticamente.

---

# 🔗 Endpoints da API

## Clientes Premium

Base URL:

```http
/api/clientes-premium
```

### Operações Disponíveis

| Método | Endpoint                            | Descrição               | Retorno |
| ------ | ----------------------------------- | ----------------------- | ------- |
| GET    | `/api/clientes-premium`             | Lista todos os clientes | 200     |
| GET    | `/api/clientes-premium/{id}`        | Busca cliente por ID    | 200     |
| POST   | `/api/clientes-premium`             | Cria novo cliente       | 201     |
| PUT    | `/api/clientes-premium/{id}`        | Atualiza cliente        | 200     |
| PATCH  | `/api/clientes-premium/{id}/status` | Atualiza status         | 200     |
| DELETE | `/api/clientes-premium/{id}`        | Remove cliente          | 204     |

---

### Exemplo de Cadastro

```json
{
  "razaoSocial": "Empresa XYZ Ltda",
  "cnpj": "12345678000190",
  "demandaContratadaGwh": 150.75,
  "statusCadastro": "ATIVO"
}
```

---

### Exemplo de Resposta

```json
{
  "id": 1,
  "razaoSocial": "Empresa XYZ Ltda",
  "cnpj": "12345678000190",
  "demandaContratadaGwh": 150.75,
  "statusCadastro": "ATIVO"
}
```

---

## Leilões

Base URL:

```http
/api/leiloes
```

### Operações Disponíveis

| Método | Endpoint                      | Descrição              | Retorno |
| ------ | ----------------------------- | ---------------------- | ------- |
| GET    | `/api/leiloes`                | Lista todos os leilões | 200     |
| GET    | `/api/leiloes/{id}`           | Busca leilão por ID    | 200     |
| GET    | `/api/leiloes/abertos`        | Lista leilões abertos  | 200     |
| POST   | `/api/leiloes`                | Cria novo leilão       | 201     |
| PUT    | `/api/leiloes/{id}`           | Atualiza leilão        | 200     |
| DELETE | `/api/leiloes/{id}`           | Remove leilão          | 204     |
| POST   | `/api/leiloes/{id}/finalizar` | Finaliza leilão        | 200     |

---

### Exemplo de Cadastro

```json
{
  "idSatelite": 1,
  "idRectennaOrigem": 10,
  "dataHoraInicio": "2026-06-10T08:00:00",
  "dataHoraFim": "2026-06-10T18:00:00",
  "gwhDisponivel": 150.5,
  "precoMinPorGwh": 200.75,
  "statusLeilao": "ABERTO"
}
```

---

### Exemplo de Finalização

```json
{
  "idLeilao": 1,
  "idClienteVencedor": 123,
  "valorArrematado": 5000.00
}
```

---

### Resposta

```json
{
  "id": 10,
  "idLeilao": 1,
  "idClienteVencedor": 123,
  "valorArrematado": 5000.00,
  "dataTransacao": "2026-06-10T18:00:00"
}
```

---

# 📋 Códigos de Retorno HTTP

| Código                    | Descrição                      |
| ------------------------- | ------------------------------ |
| 200 OK                    | Operação realizada com sucesso |
| 201 Created               | Recurso criado                 |
| 204 No Content            | Recurso removido               |
| 400 Bad Request           | Dados inválidos                |
| 404 Not Found             | Registro não encontrado        |
| 500 Internal Server Error | Erro interno da aplicação      |

---

# 🧪 Testes da API

## Criar Cliente Premium

```bash
curl -X POST "https://localhost:5270/api/clientes-premium" \
-H "Content-Type: application/json" \
-d '{
  "razaoSocial":"Empresa XYZ",
  "cnpj":"12345678000190",
  "demandaContratadaGwh":150,
  "statusCadastro":"ATIVO"
}'
```

---

## Buscar Cliente

```bash
curl -X GET "https://localhost:5270/api/clientes-premium/1"
```

---

## Atualizar Cliente

```bash
curl -X PUT "https://localhost:5270/api/clientes-premium/1" \
-H "Content-Type: application/json" \
-d '{
  "razaoSocial":"Empresa XYZ Atualizada",
  "cnpj":"12345678000190",
  "demandaContratadaGwh":180,
  "statusCadastro":"ATIVO"
}'
```

---

## Atualizar Status

```bash
curl -X PATCH "https://localhost:5270/api/clientes-premium/1/status" \
-H "Content-Type: application/json" \
-d '"INATIVO"'
```

---

## Excluir Cliente

```bash
curl -X DELETE "https://localhost:5270/api/clientes-premium/1"
```

---

## Criar Leilão

```bash
curl -X POST "https://localhost:5270/api/leiloes" \
-H "Content-Type: application/json" \
-d '{
  "idSatelite":1,
  "idRectennaOrigem":10,
  "dataHoraInicio":"2026-06-10T08:00:00",
  "dataHoraFim":"2026-06-10T18:00:00",
  "gwhDisponivel":150,
  "precoMinPorGwh":200,
  "statusLeilao":"ABERTO"
}'
```

---

## Listar Leilões Abertos

```bash
curl -X GET "https://localhost:5270/api/leiloes/abertos"
```

---

## Finalizar Leilão

```bash
curl -X POST "https://localhost:5270/api/leiloes/1/finalizar" \
-H "Content-Type: application/json" \
-d '{
  "idLeilao":1,
  "idClienteVencedor":123,
  "valorArrematado":5000
}'
```

---

# 🗄️ Modelagem de Dados

![Diagrama de Entidades](./diagrama-entidade-relacional.png)
    
---

# Tabelas Utilizadas na Aplicação .NET
## Cliente Premium

| Campo                | Tipo           | Obrigatório |
| -------------------- | -------------- | ----------- |
| Id                   | NUMBER(10)     | ✅           |
| RazaoSocial          | NVARCHAR2(255) | ✅           |
| Cnpj                 | NVARCHAR2(14)  | ✅           |
| DemandaContratadaGwh | NUMBER(18,2)   | ✅           |
| StatusCadastro       | NVARCHAR2(20)  | ✅           |

---

## Leilão

| Campo            | Tipo          | Obrigatório |
| ---------------- | ------------- | ----------- |
| Id               | NUMBER(10)    | ✅           |
| IdSatelite       | NUMBER(10)    | ✅           |
| IdRectennaOrigem | NUMBER(10)    | ✅           |
| DataHoraInicio   | TIMESTAMP     | ✅           |
| DataHoraFim      | TIMESTAMP     | ✅           |
| GwhDisponivel    | NUMBER(18,2)  | ✅           |
| PrecoMinPorGwh   | NUMBER(18,2)  | ✅           |
| StatusLeilao     | NVARCHAR2(20) | ✅           |

---

## Log de Transações

| Campo             | Tipo         | Obrigatório |
| ----------------- | ------------ | ----------- |
| Id                | NUMBER(10)   | ✅           |
| IdLeilao          | NUMBER(10)   | ✅           |
| IdClienteVencedor | NUMBER(10)   | ✅           |
| ValorArrematado   | NUMBER(18,2) | ✅           |
| DataTransacao     | TIMESTAMP    | ✅           |

---

# 🚀 Melhorias Futuras

* Autenticação JWT
* Controle de acesso baseado em Roles
* Filtros avançados
* Versionamento da API
* Testes unitários
* Cache distribuído com Redis
* Sistema assíncrono para lances em tempo real
* Integração com serviços de monitoramento e logging

---

# 📄 Licença

Projeto desenvolvido para fins acadêmicos utilizando ASP.NET Core, Entity Framework Core e Oracle Database.

Todos os direitos reservados Astra AI © 2026.
