# SimpleApiCleanCode

## 📄 Sobre o projeto
O **SimpleApiCleanCode** é um projeto de estudo focado em desenvolver uma WebAPI utilizando boas práticas de arquitetura em camadas, Clean Code e separação clara de responsabilidades.

O objetivo é criar uma estrutura base para aplicações reais, de forma simples, mas já alinhada com padrões profissionais.

---

## 👉 Tecnologias utilizadas
- .NET 8
- ASP.NET Core WebAPI
- Entity Framework Core
- C# 12


## 👉 Estrutura do Projeto

O projeto é dividido em fases evolutivas:

```
SimpleApiCleanCode-dotnet
├── 01-WebAPI       -> Estrutura básica da API REST
├── 02-WebAPI-Login -> Adiciona autenticação simples (login)
├── 03-WebAPI-JWT   -> Proteção de rotas com autenticação JWT
```

Dentro de cada pasta, você encontrará o projeto completo daquela fase.

Exemplo da estrutura da pasta `01-WebAPI`:

```
01-WebAPI/
├── Controllers
│   └── ClassesController.cs
├── Data
│   ├── Contexto (DbContext)
│   ├── IoC (Injeção de Dependência)
│   └── Repositorios (Generic Repository)
├── Dominio
│   ├── Entidades (Modelos de Negócio)
│   ├── Exceptions (Validações e Erros de Negócio)
│   ├── ModelViews (Modelos para requisicao/resposta)
│   └── Servicos (Regras de Negócio)
├── Recursos
│   ├── Constantes
│   ├── Extensao (Methods Helpers)
│   └── GerarCodigos (Utils para gerar GUIDs)
├── Program.cs
└── appsettings.json
```

---

## 👉 Como rodar o projeto localmente

1. Clone o repositório:
```bash
git clone https://github.com/seu-usuario/SimpleApiCleanCode-dotnet.git
```

2. Navegue até a pasta da versão desejada (ex: `01-WebAPI`):
```bash
cd SimpleApiCleanCode-dotnet/01-WebAPI
```

3. Restaure os pacotes:
```bash
dotnet restore
```

4. Rode a aplicação:
```bash
dotnet run
```

A API estará disponível em:
```
https://localhost:5001
http://localhost:5000
```

---

## 💬 Exemplos de Endpoints (01-WebAPI)

- **GET Listar Classes**
```bash
GET /api/v1/Classes
```

- **GET Obter Classe por ID**
```bash
GET /api/v1/Classes/{uid}
```

- **POST Criar nova Classe**
```bash
POST /api/v1/Classes
Content-Type: application/json

{
  "dados": {
    "descricao": "Nova Classe"
  }
}
```

- **PUT Atualizar Classe**
```bash
PUT /api/v1/Classes
Content-Type: application/json

{
  "dados": {
    "uid": "guid-da-classe",
    "descricao": "Classe Atualizada"
  }
}
```

- **DELETE Excluir Classe**
```bash
DELETE /api/v1/Classes
Content-Type: application/json

{
  "uid": "guid-da-classe"
}
```

---

## 🌐 Status do Projeto
- [x] Estrutura inicial da API
- [x] Regras de negócio básicas implementadas
- [x] Camadas separadas (Controller / Service / Repository / Entity)
- [ ] Implementação de testes unitários (futuro)
- [ ] Autenticação simples (em breve)
- [ ] JWT (em breve)


## 👋 Contato
Se quiser trocar uma ideia ou dar feedbacks:

- [LinkedIn - Paulo](https://www.linkedin.com/in/pjoliveira)


---

## 📅 Licença
Este projeto é apenas para fins de estudo. Sem restrições de uso.
