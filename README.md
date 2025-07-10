# MonoCore - Enterprise .NET 8 Modular Monolith

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-12-239120?style=for-the-badge&logo=csharp)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![MySQL](https://img.shields.io/badge/MySQL-4479A1?style=for-the-badge&logo=mysql&logoColor=white)
![MongoDB](https://img.shields.io/badge/MongoDB-47A248?style=for-the-badge&logo=mongodb&logoColor=white)
![RabbitMQ](https://img.shields.io/badge/RabbitMQ-FF6600?style=for-the-badge&logo=rabbitmq&logoColor=white)

> **Um projeto de portfólio demonstrando arquitetura enterprise-level com .NET 8, implementando DDD, Event Sourcing, CQRS e padrões de Clean Architecture em um monólito modular escalável.**

## **Sobre o Projeto**

MonoCore é uma aplicação **enterprise-grade** desenvolvida como **monólito modular**, demonstrando expertise em arquiteturas complexas e padrões avançados de desenvolvimento. O projeto implementa **Domain-Driven Design (DDD)**, **Event Sourcing**, **CQRS** e **Clean Architecture**, representando o estado da arte em desenvolvimento .NET moderno.

### **Contexto Profissional**
- Tomada de decisões arquiteturais complexas
- Implementação de padrões enterprise
- Integração de múltiplas tecnologias
- Práticas DevOps modernas

## **Arquitetura do Sistema**

### **Modular Monolith Pattern**
```
┌─────────────────────────────────────────────────────────────┐
│                    WebBff (API Layer)                       │
├─────────────────────────────────────────────────────────────┤
│  User Module  │  Future Module  │  Future Module            │
│  ┌─────────────┴─────────────────┴─────────────────────┐    │
│  │                Core Framework                       │    │
│  │  • Application  • Domain  • Infrastructure          │    │
│  │  • Persistence  • Shared                            │    │
│  └─────────────────────────────────────────────────────┘    │
├─────────────────────────────────────────────────────────────┤
│  Database Layer: MySQL + MongoDB + RabbitMQ                 │
└─────────────────────────────────────────────────────────────┘
```

### **Clean Architecture Implementation**
- **Domain Layer**: Entities, Value Objects, Domain Events
- **Application Layer**: Use Cases, CQRS Handlers, Interfaces
- **Infrastructure Layer**: Database, Message Bus, External Services
- **Presentation Layer**: REST APIs, Validation, Authentication

## **Stack Técnico**

### **Backend (.NET 8.0)**
```csharp
// Exemplo: CQRS Command Handler com Event Sourcing
public class CreateUserHandler : ICommandHandler<CreateUserCommand, IdentifierResponse>
{
    public async Task<Result<IdentifierResponse>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = Domain.Aggregates.User.Create(Guid.NewGuid(), command.Document, command.Phone);
        await _userApplicationService.AppendEventsAsync(user, cancellationToken);
        return Result.Success<IdentifierResponse>(new(user.Id));
    }
}
```

| Tecnologia | Propósito | Justificativa Técnica |
|------------|-----------|---------------------|
| **.NET 8.0** | Framework Principal | Performance, AOT, latest C# features |
| **MediatR** | CQRS Pattern | Desacoplamento e pipeline behaviors |
| **Entity Framework Core** | ORM | Code-first, migrations, performance |
| **FluentValidation** | Validação | Fluent interface, composição de regras |
| **Serilog** | Logging Estruturado | Correlation IDs, structured data |
| **MassTransit** | Message Bus | Enterprise messaging patterns |

### **Databases & Persistence**
- **MySQL 8.0**: Event Store principal (ACID compliance)
- **MongoDB 7**: Read models e projeções (performance)
- **RabbitMQ 3**: Message broker (reliable messaging)

### **DevOps & Infrastructure**
- **Docker**: Multi-stage builds com otimizações de segurança
- **GitHub Actions**: CI/CD com testes automatizados
- **Health Checks**: Monitoramento de infraestrutura
- **Internationalization**: Suporte a PT-BR

## 🎨 **Padrões e Práticas Implementadas**

### **1. Domain-Driven Design (DDD)**
```csharp
// Aggregate Root com Event Sourcing
public sealed class User : AggregateRoot
{
    public static User Create(Guid id, string document, string phone)
    {
        var user = new User(id);
        user.AddDomainEvent(new UserCreated(id, document, phone));
        return user;
    }
    
    protected override void Apply(IEvent @event)
    {
        switch (@event)
        {
            case UserCreated created:
                ApplyUserCreated(created);
                break;
        }
    }
}
```

### **2. CQRS com MediatR**
```csharp
// Pipeline Behavior para Validação
public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validationResult = await ValidateAsync(request);
        return validationResult.IsSuccess ? await next() : CreateValidationResponse<TResponse>(validationResult);
    }
}
```

### **3. Result Pattern para Error Handling**
```csharp
// Result Pattern para tratamento funcional de erros
public readonly struct Result<T>
{
    public bool IsSuccess { get; }
    public T Value { get; }
    public Error Error { get; }
    
    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<Error, TResult> onFailure)
        => IsSuccess ? onSuccess(Value) : onFailure(Error);
}
```

### **4. Event Sourcing Implementation**
```csharp
// Generic Event Store com Type Safety
public async Task AppendEventsAsync<T>(T aggregate, CancellationToken cancellationToken = default) 
    where T : class, IAggregateRoot
{
    var events = aggregate.GetDomainEvents().Select(e => new StoreEvent
    {
        AggregateId = aggregate.Id,
        EventType = e.GetType().Name,
        EventData = JsonSerializer.Serialize(e),
        Version = aggregate.Version
    }).ToList();
    
    await _context.StoreEvents.AddRangeAsync(events, cancellationToken);
    await _context.SaveChangesAsync(cancellationToken);
}
```

## **Features Implementadas**

### **Core Features**
- ✅ **Event Sourcing**: Store completo de eventos com replay
- ✅ **CQRS**: Separação comando/query com projeções
- ✅ **JWT Authentication**: Sistema completo de autenticação
- ✅ **API Versioning**: Versionamento semântico de APIs
- ✅ **Health Checks**: Monitoramento de infraestrutura
- ✅ **Correlation IDs**: Rastreamento distribuído
- ✅ **Background Jobs**: Processamento assíncrono

### **Quality Assurance**
- ✅ **Validation Pipeline**: FluentValidation integrado
- ✅ **Error Handling**: Result pattern consistente
- ✅ **Logging Structured**: Serilog com enrichers
- ✅ **PR Templates**: Processo estruturado de review
- ✅ **CI/CD Pipeline**: Testes e build automatizados

## **Docker & DevOps**

### **Multi-Stage Dockerfile**
```dockerfile
# Otimizado para produção com usuário não-root
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["Monolith.Core.sln", "."]
# ... build steps

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
RUN adduser -S appuser -G appgroup
USER appuser
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebBff.dll"]
```

### **GitHub Actions CI/CD**
- **Build & Test**: Compilação e testes automatizados
- **Multi-Service**: MySQL, MongoDB, RabbitMQ
- **Security Scanning**: Dockerfile security checks
- **Docker Build**: Imagens otimizadas para produção

## **Métricas do Projeto**

| Métrica | Valor | Descrição |
|---------|-------|-----------|
| **Linhas de Código** | ~8.000+ | Código limpo e bem estruturado |
| **Projetos** | 13 | Separação modular clara |
| **Padrões Implementados** | 10+ | DDD, CQRS, Event Sourcing, etc. |
| **Tecnologias Integradas** | 15+ | Stack enterprise completo |
| **Cobertura de Testes** | Configurado | Framework de testes implementado |

## **Aprendizados e Desafios**

### **Desafios Técnicos Superados**
1. **Consistency Eventual**: Implementação de CQRS com projeções consistentes
2. **Event Replay**: Sistema de replay de eventos para reconstrução de estado
3. **Multi-Database**: Orchestração de múltiplos databases com transações
4. **Message Ordering**: Garantia de ordem de mensagens em cenários distribuídos

### **Decisões Arquiteturais**
- **Monólito Modular vs Microservices**: Optei pelo monólito modular para reduzir complexidade operacional mantendo flexibilidade
- **Event Sourcing**: Implementado para auditoria completa e capacidade de replay
- **Multi-Database**: MySQL para consistência ACID, MongoDB para performance de leitura

## **Executando o Projeto**

### **Pré-requisitos**
```bash
- .NET 8.0 SDK
- Docker & Docker Compose
- MySQL 8.0+
- MongoDB 7+
- RabbitMQ 3.12+
```

### **Setup Rápido**
```bash
# 1. Clone o repositório
git clone https://github.com/your-username/MonoCore.git

# 2. Subir infraestrutura
docker-compose -f docker-compose.dev.yml up -d

# 3. Executar aplicação
dotnet run --project src/Web/WebBff

# 4. Acessar documentação
curl http://localhost:5171/swagger
```

### **Endpoints Principais**
- **Swagger UI**: `http://localhost:5171/swagger`
- **Health Checks**: `http://localhost:5171/api/healthz`
- **User API**: `http://localhost:5171/api/v1/users`

## **Roadmap Futuro**

- [ ] **Implementar Cache Distribuído** (Redis)
- [ ] **Adicionar Outbox Pattern** para garantias transacionais
- [ ] **Métricas e Observabilidade** (Prometheus/Grafana)
- [ ] **Testes de Performance** (k6 integration)
- [ ] **Novos Módulos** (Product, Order)

## **Sobre o Desenvolvedor**

**Desenvolvedor Backend** especializado em:
- **Arquiteturas Complexas**: DDD, Event Sourcing, CQRS
- **Performance & Scalability**: Otimização de aplicações enterprise
- **DevOps**: Docker, CI/CD
- **Clean Code**: SOLID, Design Patterns, Best Practices

### **Tecnologias Dominadas**
- **.NET Ecosystem**: C#, ASP.NET Core, Entity Framework Core
- **Databases**: MySQL, MongoDB
- **Cloud & DevOps**: Docker, GitHub Actions
- **Architecture**: Microservices, Event-Driven, Domain-Driven Design

---

## **Contato**

- **LinkedIn**: *https://www.linkedin.com/in/augusto-sandrini/*
- **GitHub**: *https://github.com/AugustoSandrini/*
- **Email**: augustosandrini@gmail.com

---

*Este projeto demonstra competências técnicas em desenvolvimento .NET, arquitetura de software e práticas DevOps modernas, representando capacidade para trabalhar em projetos enterprise de alta complexidade.*
