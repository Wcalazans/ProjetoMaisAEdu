Feature: Cadastro no sistema Grupo A Educação
  Como um usuário potencial
  Quero me cadastrar no sistema
  Para acessar os serviços oferecidos

  Background:
    Given Que estou na página de cadastro

  Scenario: Selecionar nível de educação e curso
    When Seleciono o nível de educação "undergraduate"
    And Seleciono o curso "Mestrado em Ciência da Computação"
    Then Devo ser direcionado para a tela de preenchimento de formulário

  Scenario: Preencher e enviar o formulário de cadastro com sucesso
    When Seleciono o nível de educação "undergraduate"
    And Seleciono o curso "Mestrado em Ciência da Computação"
    And Preencho o formulário com os seguintes dados:
      | CPF         | Nome   | Sobrenome | NomeSocial       | Nascimento | Deficiencia | Email            | Celular     | Telefone   | CEP       | Endereco     | Complemento | Bairro | Cidade | Estado | País   |
      | 13449831704 | Ramon  | Silva     | Wesley Calazans  | 28/03/1990 | false       | joao@email.com   | 21999998888 | 2122223333 | 12345678  | Rua Exemplo  | Apto 101    | Centro | Rio    | RJ     | Brasil |
    Then Devo ver a mensagem "Sua jornada começa aqui!"

  Scenario: Realizar login após o cadastro
    When Seleciono o nível de educação "undergraduate"
    And Seleciono o curso "Mestrado em Ciência da Computação"
    And Preencho o formulário com os seguintes dados:
      | CPF         | Nome   | Sobrenome | NomeSocial       | Nascimento | Deficiencia | Email            | Celular     | Telefone   | CEP       | Endereco     | Complemento | Bairro | Cidade | Estado | País   |
      | 13449831704 | Ramon  | Silva     | Wesley Calazans  | 28/03/1990 | false       | joao@email.com   | 21999998888 | 2122223333 | 12345678  | Rua Exemplo  | Apto 101    | Centro | Rio    | RJ     | Brasil |
    And Faço login com o usuário "candidato" e senha "subscription"
    Then Devo ser redirecionado para a área do candidato
