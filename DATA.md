
# DATA.md

## Estratégia para Criação e Uso da Massa de Dados de Testes com PostgreSQL

### 🎯 Objetivo
Garantir que os testes automatizados sejam reprodutíveis, confiáveis e independentes, utilizando dados consistentes armazenados em um banco PostgreSQL.

---

### 🧱 Estratégia Geral

#### Ambiente Isolado para Testes
- Utilizar um banco de dados separado para testes (`postgres_test`) para evitar impacto nos dados reais da aplicação.
- Automatizar a criação e limpeza deste banco antes e após a execução da suíte de testes.

#### Criação da Massa de Dados
- Definir scripts SQL ou usar uma ferramenta de migration/seeding (como Flyway, Liquibase ou EF Core Migrations) para popular o banco com dados necessários antes dos testes.
- Garantir que os dados inseridos sejam minimamente suficientes para cobrir todos os cenários de teste.

#### Dados Consistentes e Controlados
- Utilizar dados statically e previsíveis, como nomes, CPFs e e-mails fixos.
- Evitar uso de dados randômicos para que os testes tenham resultados previsíveis e fáceis de depurar.

#### Isolamento dos Testes
- Cada teste deve rodar com independência, podendo usar `transactions` com rollback ao fim do teste ou truncamento das tabelas entre execuções.
- Preferência por testes que "criam e apagam" seus próprios dados.

#### Scripts de Limpeza
- Disponibilizar scripts ou comandos que possam ser executados para resetar o banco ao estado inicial.
- Alternativa: usar containers Docker para subir/derrubar ambientes com facilidade.

---

### 🛠️ Ferramentas Sugeridas

- **Docker + docker-compose**: para subir uma instância isolada do PostgreSQL para testes.
- **pgAdmin ou DBeaver**: para inspecionar os dados manualmente durante o desenvolvimento dos testes.
- **Entity Framework Core** (se usado no projeto): uso de migrations e seeders.
- **xUnit com Reqnroll**: integração dos testes com execução controlada da massa de dados.

---

### 🔐 Considerações Finais

- Nunca utilizar o banco de produção como fonte de massa de testes.
- Garantir que nenhum dado sensível ou pessoal real esteja presente no banco de testes.
- Documentar os dados criados e manter consistência entre os ambientes de dev, staging e teste.

---

```markdown
# DATA.md

## 📌 Estratégia para Criação e Uso da Massa de Dados de Testes com PostgreSQL

### 🎯 Objetivo
Garantir que os testes automatizados sejam:
- **Reprodutíveis**: Mesmos resultados em qualquer ambiente.
- **Confiáveis**: Dados previsíveis e sem vazamentos.
- **Isolados**: Nenhum impacto em bancos de produção.

---

## 🧱 Estratégia Geral

### 1. Ambiente Isolado para Testes
- **Banco dedicado**: `postgres_test` (nunca compartilhar com produção).
- **Containerização**: Usar Docker para subir/derrubar o banco sob demanda.
  ```bash
  docker-compose -f docker-compose.test.yml up -d
  ```

### 2. Criação da Massa de Dados
- **Migrations/Scripts SQL**: 
  - Exemplo: `./scripts/seed_test_data.sql` com inserts controlados.
  ```sql
  TRUNCATE TABLE usuarios, produtos CASCADE;
  INSERT INTO usuarios (nome, email) VALUES 
    ('Alice', 'alice@teste.com'), 
    ('Bob', 'bob@teste.com');
  ```

### 3. Dados Consistentes
- **Regras**:
  - CPFs/E-mails fictícios (ex: `XXX.XXX.XXX-00`, `prefixo+teste@dominio.com`).
  - IDs fixos para buscas em testes (ex: `WHERE id = 1`).

### 4. Isolamento dos Testes
- **Padrão recomendado**:
  ```csharp
  [Test]
  public void Teste_Usuario_DeveTerEmailValido()
  {
      using (var transaction = _conn.BeginTransaction()) 
      {
          // Insere dados temporários
          _conn.Execute("INSERT INTO usuarios (...) VALUES (...)");
          
          // Teste aqui
          Assert.That(/* ... */);
          
          // Rollback automático
      }
  }
  ```

### 5. Limpeza Pós-Testes
- **Opções**:
  - **Rollback** de transações (recomendado para testes unitários).
  - **Scripts SQL** para truncar tabelas:
    ```sql
    DO $$ 
    BEGIN
      EXECUTE (SELECT 'TRUNCATE ' || string_agg(format('%I.%I', schemaname, tablename), ', ') 
               FROM pg_tables 
               WHERE schemaname = 'public');
    END $$;
    ```

---

## 🛠️ Ferramentas Recomendadas

| Ferramenta          | Uso                              | Exemplo                          |
|---------------------|----------------------------------|----------------------------------|
| **Docker**          | Subir PostgreSQL isolado         | `docker run -p 5433:5432 postgres` |
| **Flyway**         | Gerenciar migrations de testes   | `flyway migrate -configFiles=flyway.test.conf` |
| **xUnit/NUnit**    | Framework de testes              | `[TestFixture]` + `[TearDown]`   |
| **Faker.js**       | Gerar dados fictícios (opcional) | `faker.br.cpf()`                 |

---

## 📂 Estrutura de Diretórios Sugerida
```
/projeto
├── /scripts
│   ├── migrations/    # Flyway/Liquibase
│   ├── seeds/         # INSERTs de teste
│   └── cleanup.sql    # Limpeza global
├── /test
│   └── TestesBancoDados.cs
└── docker-compose.test.yml
```

---

## 🔐 Boas Práticas
- ✅ **Dados Anônimos**: Nunca usar dados reais de usuários.
- ✅ **IDs Fixos**: Facilitam asserts em testes (ex: `Assert.AreEqual(1, usuario.Id)`).
- ✅ **Logs Detalhados**: Registrar queries executadas durante os testes.

---

## 🚀 Exemplo Prático Completo
Veja um template funcional em:  
[Repositório de Exemplo](https://github.com/exemplo/postgres-test-strategy)
```
