
# 🎮 FCG - FIAP Cloud Games (Arquitetura .NET)

Projeto desenvolvido como parte do **Tech Challenge** da pós-graduação em Arquitetura .NET pela FIAP.

A aplicação representa uma **plataforma de jogos digitais**, permitindo que usuários possam adquirir jogos, acompanhar promoções, e gerenciar sua biblioteca. A estrutura do projeto foi pensada com foco em **boas práticas de arquitetura**, **DDD**, **segurança** e **testabilidade**.

---

## 🧭 Objetivo

Evoluir o sistema base que foi iniciado na Fase 1, utilizando **.NET 8**, aplicando os princípios de **Domain-Driven Design (DDD)** com foco na **automação do deploy**, na **conteinerização da aplicação** e no **monitoramento da infraestrutura**.

---

## 📂 Estrutura do Projeto

```
.
├── docs/                     # Documentação adicional
├── scripts/                  # Scripts úteis (migrations, cobertura, etc.)
├── src/                      # Código-fonte principal
│   ├── FCG.API               # Camada de apresentação (Web API)
│   ├── FCG.Application       # Camada de aplicação (serviços e DTOs)
│   ├── FCG.Domain            # Camada de domínio (entidades e regras de negócio)
│   ├── FCG.Infra.Data        # Persistência de dados (EF Core e repositórios)
│   └── FCG.Infra.Security    # Segurança e autenticação (JWT, Identity)
└── tests/
    └── FCG.UnitTests         # Testes unitários
```

---

## 🛠️ Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Entity Framework Core](https://learn.microsoft.com/ef/)
- [ASP.NET Core Web API](https://learn.microsoft.com/aspnet/core/web-api)
- [JWT Bearer Authentication](https://jwt.io/)
- [Identity](https://learn.microsoft.com/pt-br/aspnet/identity)
- [FluentValidation](https://fluentvalidation.net/)
- [xUnit](https://xunit.net/)
- [Moq](https://github.com/moq/moq)
- [Bogus (Faker)](https://github.com/bchavez/Bogus)
- [dotnet-reportgenerator-globaltool](https://github.com/danielpalme/ReportGenerator)
- [Docker](https://www.docker.com)
- [Prometheus](https://prometheus.io)
- [Grafana](https://grafana.com)

---

## 🚀 Como Executar o Projeto

1. **Clone o repositório**
   ```bash
   git clone https://github.com/louroRafael/fiap-cloud-games.git
   cd fiap-cloud-games
   ```

2. **Execute o projeto**
   ```bash
   cd src/FCG.API
   dotnet run
   ```

3. **Compilar o projeto**
   ```bash
   dotnet build --configuration Release
   ```

4. **Executar a API**
   ```bash
   cd src/FCG.API
   dotnet run
   ```

A API estará disponível em:
   👉 http://localhost:8080/swagger/index.html

---


## 🧪 Testes e Cobertura

### Executar testes
```bash
dotnet test --configuration Release
```

### Gerar relatório de cobertura

1. **Instale o ReportGenerator (uma vez só)**
   ```bash
   dotnet tool install -g dotnet-reportgenerator-globaltool
   ```

2. **Execute o script de cobertura**
   ```bash
   ./scripts/coverage-tests.sh
   ```

O relatório será gerado em `coverage-report/index.html`.

---

## 📌 Notas

- Estruturado para expansão futura (fases seguintes do Tech Challenge)
- Pronto para ser containerizado e publicado no Azure Container Apps

---
