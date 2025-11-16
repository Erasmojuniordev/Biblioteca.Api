# 📚 Biblioteca API  
Uma API RESTful construída com .NET 8 utilizando **Clean Architecture**, **Entity Framework Core**, **SQLite**, e **princípios de DDD**.  
Focada em boas práticas, separação de responsabilidades e alto potencial de escalabilidade.

---

## 🚀 Tecnologias Utilizadas

- **.NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQLite**
- **Swagger**
- **Repository Pattern**
- **Dependency Injection (DI)**
- **Clean Architecture**

---

## 🏗 Arquitetura (Clean Architecture)

A solução segue uma separação clara em camadas:


### **Domain**
- Contém **as entidades reais do negócio**: Livro, Autor, Usuário, Empréstimo
- Define **interfaces contratuais** (ex: `ILivroRepository`, `IUsuarioRepository`)

### **Application**
- Implementa **Services**, ex: `LivroService`, `UsuarioService`
- Regras de negócio ficam **aqui**, não no controller
- Usa **DTOs** e validações

### **Infrastructure**
- Implementa **os repositórios concretos**
- Configura o **DbContext**
- Aplica **Migrations**
- Escolhe o banco (aqui é **SQLite** 💾)

### **API**
- Controllers enxutos
- Usam apenas os Services (nunca o EF direto)
- Swagger habilitado

---

## 🛢 Banco de Dados

- Banco: **SQLite**
- Contexto: `BibliotecaDbContext`
- Migrations geradas via:  
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
## ⚙ Como Rodar Localmente
### 1️⃣ Clonar o repositório
```bash
git clone https://github.com/seu-usuario/Biblioteca.Api.git
```
### 2️⃣ Entrar na pasta do projeto
```bash
cd Biblioteca.Api
```
### 3️⃣ Restaurar dependências
```bash
dotnet restore
```
### 4️⃣ Criar o banco (SQLite) via migrations
```bash
dotnet ef database update
```
### 5️⃣ Rodar o projeto
```bash
dotnet run
```

---
## 🔍 Testando no Swagger
### 👉 Acesse:
```
https://localhost:7067/swagger
```

Lá você pode testar todos os endpoints.

---
## 📡 Endpoints Principais

| Método | Rota             | Descrição                  |
| ------ | ---------------- | -------------------------- |
| GET    | /api/livros      | Lista todos os livros      |
| POST   | /api/livros      | Cadastra um novo livro     |
| PUT    | /api/livros/{id} | Atualiza um livro          |
| DELETE | /api/livros/{id} | Remove um livro            |
| GET    | /api/autores     | Lista autores              |
| POST   | /api/emprestimos | Registra um empréstimo     |
| GET    | /api/usuarios    | Lista usuários cadastrados |

---
## 📌 Roadmap Futuro
- [ ] Autenticação via JWT
- [ ] Paginação e filtros de busca
- [ ] Testes unitários
- [ ] Deploy em nuvem
- [ ] Integração com frontend React

---
## 🙌 Autor
### Desenvolvido por Erasmo

*Se curtiu, ⭐ no repo ajuda demais!*
