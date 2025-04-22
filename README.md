# Desafio MaisA Educação - Automação de Testes

Este projeto foi desenvolvido como parte do processo seletivo da MaisA Educação. Ele automatiza o fluxo de inscrição no formulário disponível no site:

[https://developer.grupoa.education/subscription/](https://developer.grupoa.education/subscription/)

## Tecnologias utilizadas

- **.NET 6.0**: Framework principal da aplicação de testes.
- **Reqnroll (SpecFlow)**: Ferramenta BDD para definição de cenários em linguagem Gherkin.
- **Selenium WebDriver**: Utilizado para interações com o navegador (ChromeDriver).
- **xUnit**: Framework de testes para execução e validação dos testes automatizados.

## Estrutura do Projeto

- `/Features`: Contém os arquivos `.feature` com os cenários BDD em Gherkin.
- `/Steps`: Implementações dos steps definidos nos arquivos `.feature`.
- `/Drivers`: Configurações de WebDriver.
- `/Pages` *(opcional)*: Padrão Page Object para encapsular elementos e ações da página (caso utilizado).

## Funcionalidades automatizadas

- Acesso à página de inscrição.
- Preenchimento completo do formulário com dados fictícios válidos.
- Validação do título da página.
- Submissão do formulário.
- Validação da resposta esperada após o envio.

## Pré-requisitos

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Chrome](https://www.google.com/chrome/) instalado
- [ChromeDriver](https://sites.google.com/chromium.org/driver/) compatível com sua versão do navegador (ou configurado via NuGet)

## Como executar os testes

1. Clone o repositório:
   ```bash
   git clone https://github.com/Wcalazans/Projeto-A.git
   cd Projeto-A
   ```

2. Restaure os pacotes NuGet:
   ```bash
   dotnet restore
   ```

3. Execute os testes:
   ```bash
   dotnet test
   ```

## Observações

- Certifique-se de que o Chrome esteja atualizado e o ChromeDriver seja compatível.
- Caso o formulário utilize algum tipo de verificação (como CAPTCHA), os testes podem falhar ou precisar de tratamento adicional.

## Autor

Desenvolvido por **Wesley Calazans**  
[wesleycalazansqa@gmail.com](mailto:wesleycalazansqa@gmail.com)  
[https://github.com/Wcalazans](https://github.com/Wcalazans)
