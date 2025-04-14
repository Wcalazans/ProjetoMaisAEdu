
# 📄 k6-data.md

## 🎯 Objetivo

Descrever uma estratégia eficaz para a criação e utilização de massa de dados de testes em scripts de carga usando o K6, garantindo **realismo**, **reprodutibilidade** e **manutenção** dos testes de performance.

---

## 🔧 Estratégias de Massa de Dados com K6

### 1. Uso de Arquivos CSV ou JSON como Fonte de Dados
K6 permite importar arquivos `.csv` ou `.json` com dados para simular diferentes usuários ou variações nos testes.

#### Exemplo de uso com CSV:
```js
import { SharedArray } from 'k6/data';
import papa from 'https://jslib.k6.io/papaparse/5.1.1/index.js';

const users = new SharedArray('users', function () {
  return papa.parse(open('./data/usuarios.csv'), { header: true }).data;
});
```

**Vantagens:**
- Permite testes com múltiplos perfis de dados.
- Reutilizável em vários scripts.

---

### 2. Massa Estática no Código
Para testes simples ou dados repetidos, é possível declarar arrays ou objetos diretamente no código:
```js
const users = [
  { username: 'user1', password: 'pass' },
  { username: 'user2', password: 'pass' },
];
```

---

### 3. Geração Dinâmica com Funções
É possível gerar dados em tempo de execução:
```js
function randomUser() {
  return {
    username: `user${Math.floor(Math.random() * 1000)}`,
    password: '123456',
  };
}
```

**⚠️ Cuidado:** Evite dados randômicos quando precisar de resultados consistentes para análise.

---

## 🧹 Cuidados e Boas Práticas

- ✅ Utilize arquivos versionados (como `data/usuarios.csv`) no repositório.
- ✅ Organize seus dados em uma pasta padrão: `./data/`.
- ✅ Padronize colunas com nomes claros: `username`, `email`, `cpf`, etc.
- ✅ Use `SharedArray` para melhor performance e menor uso de memória.
- ❌ Evite acessar APIs externas durante o teste para buscar dados.

---

## 📁 Estrutura de Diretórios Sugerida

```
/testes-k6
├── test_my_messages.js
├── test_flip_coin.js
├── data/
│   └── usuarios.csv
└── k6-data.md
```

---

## 🚀 Exemplo de CSV

**Arquivo:** `data/usuarios.csv`
```csv
username,password
alice,123456
bob,senhaSegura
carlos,abc123
```

---

## 📌 Conclusão

Ter uma estratégia bem definida para massa de dados com K6:
- Garante **variedade nos testes** (vários perfis).
- Permite **simulação realista de usuários**.
- Melhora a **reprodutibilidade e confiabilidade** dos testes de carga.
