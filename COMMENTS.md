# COMMENTS.md

## 🧠 Decisões de Arquitetura Utilizada

Optei por uma arquitetura simples, focada na clareza e manutenção dos testes. Usei scripts separados por carga (100, 500 e 1000 usuários) para facilitar a leitura e o controle dos testes de performance, mantendo cada teste isolado e com escopo bem definido. A ideia foi garantir que qualquer pessoa da equipe consiga entender e ajustar os testes com facilidade.

Além disso, organizei os arquivos de evidências e saídas dos testes para que possam ser analisados posteriormente de forma prática. O uso de dados estáticos e previsíveis também ajuda a evitar falsos positivos ou testes instáveis.

## 📦 Bibliotecas de Terceiros Utilizadas

- **k6**: ferramenta principal para testes de carga, por ser leve, rápida, e com curva de aprendizado tranquila.
- **Docker** (sugerido para ambiente isolado de banco): não incluído nos scripts, mas mencionado como boa prática.

## ⏳ O Que Melhoraria Se Tivesse Mais Tempo

- Automatizaria a execução dos testes com diferentes perfis de carga usando um script bash ou npm.
- Integraria os testes com um pipeline CI/CD para rodar automaticamente em pushes ou merges.
- Usaria variáveis de ambiente (.env) para controlar dados como URLs e credenciais.
- Criaria dashboards com Grafana + InfluxDB para visualizar as métricas com mais clareza.
- Implementaria testes mais amplos que simulassem o comportamento de um fluxo real de usuário e não só endpoints isolados.

## 📌 Requisitos Obrigatórios Não Entregues

- Todos os itens principais foram entregues, incluindo múltiplos cenários de carga, documentação da massa de dados e scripts de teste separados.
- Como melhoria futura, seria interessante expandir os testes para simular cenários mais realistas com autenticação JWT ou token, e testar outros endpoints do sistema se disponíveis.

---

Esse projeto foi feito com foco em clareza, praticidade e resultado. O objetivo foi demonstrar não só conhecimento técnico, mas também um olhar estratégico e prático sobre qualidade, performance e confiabilidade da aplicação.