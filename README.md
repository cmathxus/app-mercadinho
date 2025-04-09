
# 🛒 Mercadinho do Caio

Sistema simples de controle de estoque feito em **ASP.NET Core Razor Pages** com banco de dados **SQLite**.  
Criado para ajudar no gerenciamento de produtos de um mercadinho real (doces, bebidas, etc).

---

## 🚀 Funcionalidades

- Cadastro de produtos com **nome**, **preço** e **quantidade**
- Listagem de todos os produtos cadastrados
- Cálculo automático do **valor total em estoque**
- Verificação de duplicidade: se um produto com o mesmo nome já existir, apenas soma a quantidade
- Banco de dados local com SQLite (.sqlite)
- Interface web simples e funcional

---

## 🧰 Tecnologias usadas

- C#
- ASP.NET Core Razor Pages
- SQLite
- HTML + Razor
- Entity Framework Core

---

## 🔧 Como rodar o projeto

1. Clone o repositório:
```bash
git clone https://github.com/teu-user/mercadinho.git
```

2. Acesse a pasta:
```bash
cd mercadinho
```

3. Restaure os pacotes:
```bash
dotnet restore
```

4. Rode o projeto:
```bash
dotnet watch run
```

5. Acesse no navegador:
```
http://localhost:port
```

---

## 📦 Estrutura principal

```bash
Mercadinho/
├── Pages/
│   ├── Cadastro.cshtml
│   ├── Cadastro.cshtml.cs
│   ├── Lista.cshtml
│   └── Lista.cshtml.cs
├── Models/
│   └── Produto.cs
├── Data/
│   └── AppDbContext.cs
├── banco.sqlite
├── Program.cs
└── README.md
```