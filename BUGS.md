# BUGS.md

## 📌 Bug 1: Nome com número é aceito

- **Gravidade:** **Moderada**  
  - **Justificativa:** Embora permita o avanço no formulário, a aceitação de números no campo "Nome completo" compromete a integridade dos dados e pode gerar problemas em cadastros, relatórios e validações futuras.

- **Descrição:** O campo "Nome completo" aceita caracteres numéricos, como "João123", o que não é apropriado para esse tipo de dado.
- **Passos para reproduzir:**
  1. Acessar a página de inscrição: https://developer.grupoa.education/subscription/
  2. Navegar até a etapa "Dados Pessoais".
  3. Preencher o campo "Nome completo" com um valor como "João123".
- **Comportamento esperado:** O sistema deve validar o campo e não permitir números no nome, exibindo uma mensagem de erro.
- **Comportamento atual:** O campo aceita o nome com números e permite seguir normalmente.
- **Ambiente:**  
  - Navegador: Google Chrome  
  - SO: Windows

---

## 📌 Bug 2: Data de nascimento aceita data atual, retroativa e futura

- **Gravidade:** **Moderada**  
  - **Justificativa:** Trata-se de um problema que compromete a integridade e consistência dos dados do usuário. Embora não cause uma falha crítica no sistema, pode gerar registros incoerentes ou inválidos, impactando relatórios e validações futuras.

- **Descrição:** O campo "Data de nascimento" permite a seleção de **datas incoerentes**, como a data atual (hoje), datas retroativas absurdas (como muito antes do nascimento de um ser humano possível) e datas futuras. Isso compromete a consistência lógica, pois não é possível que um usuário tenha nascido na data atual ou futura.  
  - **Impacto:** Dados inconsistentes podem afetar fluxos dependentes da validação correta, como geração de perfis, relatórios ou aprovações automáticas.

- **Passos para reproduzir:**
  1. Acessar a página de inscrição: https://developer.grupoa.education/subscription/
  2. Navegar até a etapa "Dados Pessoais".
  3. Informar uma data de nascimento:
     - Igual à data de hoje (data atual).
     - Uma data retroativa absurda (ex.: 01/01/1800).
     - Uma data futura (ex.: 01/01/2100).

- **Comportamento esperado:**  
  O sistema deve validar que a data de nascimento:
  - Deve ser **anterior à data atual**.
  - Deve estar dentro de um intervalo lógico aceitável (ex.: nos últimos 150 anos).  
  Ao não atender os critérios, uma mensagem de erro deve ser exibida.

- **Comportamento atual:**  
  O sistema aceita qualquer data, incluindo data atual, retroativa fora de contexto e futura, sem validação ou bloqueio.

- **Ambiente:**  
  - Navegador: Google Chrome  
  - SO: Windows  

---

## 📌 Bug 3: Formulário não valida coerência entre CEP, endereço, cidade e país

- **Gravidade:** **Alta**  
  - **Justificativa:** A ausência de validação pode causar envios de informações completamente inconsistentes e comprometer fluxos dependentes da integridade dos dados, como logística e correspondência.  

- **Descrição:** O formulário de endereço permite o envio de dados inconsistentes entre si. Por exemplo, foi possível informar um CEP válido do RJ, um endereço de SC e um país diferente (Colômbia), sem qualquer validação ou alerta de erro.
- **Passos para reproduzir:**
  1. Acessar a página de inscrição: https://developer.grupoa.education/subscription/
  2. Preencher os campos de endereço com as seguintes informações:
     - **CEP:** 23822160 (Rio de Janeiro - RJ)
     - **Endereço:** Rua das Orquídeas, 123
     - **Bairro:** Floresta
     - **Cidade:** Joinville
     - **Estado:** SC
     - **País:** Colômbia
  3. Prosseguir para a próxima etapa do formulário.
- **Comportamento esperado:** O sistema deve validar a coerência entre CEP, endereço, cidade, estado e país, impedindo o avanço em caso de inconsistência.
- **Comportamento atual:** O formulário aceita os dados e permite o avanço sem qualquer validação.
- **Ambiente:**  
  - Navegador: Google Chrome  
  - SO: Windows  
