using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Reqnroll;
using Xunit;
using System;
using System.Threading;
using System.Collections.Generic;

namespace GrupoAEducation.Tests
{
    [Binding]
    public class CadastroSteps
    {
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;

        public CadastroSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            // Configuração robusta do ChromeDriver
            var options = new ChromeOptions();

            // REMOVA este comentário para modo headless (sem visualização)
            // options.AddArgument("--headless");

            options.AddArguments(
                "--start-maximized",
                "--disable-infobars",
                "--disable-notifications",
                "--disable-gpu",
                "--no-sandbox",
                "--disable-dev-shm-usage",
                "--window-size=1920,1080"
            );

            _driver = new ChromeDriver(options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
        }

        [Given(@"Que estou na página de cadastro")]
        public void GivenQueEstouNaPaginaDeCadastro()
        {
            _driver.Navigate().GoToUrl("https://developer.grupoa.education/subscription/");

            // Verificação robusta de carregamento
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            wait.Until(drv => drv.Url.Contains("subscription"));

            Thread.Sleep(1000); // Pausa para visualização
        }

        [When(@"Seleciono o nível de educação ""(.*)""")]
        public void WhenSelecionoONivelDeEducacao(string nivelEducacao)
        {
            try
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));

                // Versão resiliente para encontrar o dropdown
                var dropdown = wait.Until(drv =>
                {
                    try
                    {
                        return drv.FindElement(By.XPath("//select[@data-testid='education-level-select']"));
                    }
                    catch
                    {
                        return drv.FindElement(By.CssSelector("select[data-testid*='education']"));
                    }
                });

                var select = new SelectElement(dropdown);
                select.SelectByValue(nivelEducacao);

                Thread.Sleep(1000); // Pausa para visualização
            }
            catch (Exception ex)
            {
                TakeScreenshot("erro_selecao_nivel");
                throw new Exception($"Falha ao selecionar nível de educação: {ex.Message}");
            }
        }

        [When(@"Seleciono o curso ""(.*)""")]
        public void WhenSelecionoOCurso(string nomeCurso)
        {
            try
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

                // 1. Localizar o botão do combo
                IWebElement btnCurso = FindElementWithRetry(
                    new List<By>
                    {
                        By.XPath("//button[@data-testid='graduation-combo']"),
                        By.XPath("//button[contains(@data-testid, 'combo')]"),
                        By.CssSelector("button[data-testid='combo']")
                    });

                btnCurso.Click();
                Thread.Sleep(800); // Pausa para animação

                // 2. Localizar o curso específico
                IWebElement cursoElement = FindElementWithRetry(
                    new List<By>
                    {
                        By.XPath($"//div[@data-radix-vue-combobox-item and normalize-space(.) = '{nomeCurso}']")
                    });

                cursoElement.Click();
                Thread.Sleep(800); // Pausa para animação

                // 3. Clicar no botão Next
                IWebElement btnNext = FindElementWithRetry(
                    new List<By>
                    {
                        By.XPath("//button[@data-testid='next-button']"),
                        By.XPath("//button[contains(., 'Próximo')]"),
                        By.XPath("//button[contains(., 'Next')]")
                    });

                btnNext.Click();
                Thread.Sleep(2000); // Pausa para transição de tela
            }
            catch (Exception ex)
            {
                TakeScreenshot("erro_selecao_curso");
                throw new Exception($"Falha ao selecionar curso: {ex.Message}");
            }
        }

        [Then(@"Devo ser direcionado para a tela de preenchimento de formulário")]
        public void ThenDevoSerDirecionadoParaATelaDePreenchimentoDeFormulario()
        {
            try
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(25));

                // Verificação robusta da tela de formulário
                var formElement = wait.Until(drv =>
                {
                    try
                    {
                        return drv.FindElement(By.XPath("//h1[contains(., 'Formulário')]"));
                    }
                    catch
                    {
                        return drv.FindElement(By.CssSelector("h1.form-title"));
                    }
                });

                Assert.True(formElement.Displayed, "Tela de formulário não está visível");
                Thread.Sleep(2000); // Pausa para visualização
            }
            catch (Exception ex)
            {
                TakeScreenshot("erro_tela_formulario");
                throw new Exception($"Falha ao verificar tela de formulário: {ex.Message}");
            }
        }

  [When(@"Preencho o formulário com os seguintes dados:")]
public void WhenPreenchoOFormularioComOsSeguintesDados(Table table)
{
    var dados = table.Rows[0];

    PreencherCampo("cpf-input", dados["CPF"]);
    PreencherCampo("name-input", dados["Nome"]);
    PreencherCampo("surname-input", dados["Sobrenome"]);
    PreencherCampo("social-name-input", dados["NomeSocial"]);
    PreencherCampo("birthDate", dados["Nascimento"]);

    if (bool.TryParse(dados["Deficiencia"], out bool possuiDeficiencia) && possuiDeficiencia)
    {
        ClicarCampo("hasDisability");
    }

    PreencherCampo("email-input", dados["Email"]);
    PreencherCampo("cellphone-input", dados["Celular"]);
    PreencherCampo("phone-input", dados["Telefone"]);
    PreencherCampo("cep-input", dados["CEP"]);
    PreencherCampo("address-input", dados["Endereco"]);
    PreencherCampo("complement-input", dados["Complemento"]);
    PreencherCampo("neighborhood-input", dados["Bairro"]);
    PreencherCampo("city-input", dados["Cidade"]);
    PreencherCampo("state-input", dados["Estado"]);
    PreencherCampo("country-input", dados["País"]);

    Thread.Sleep(1000); // tempo para visualização

    // Clicar no botão de envio do formulário (ajuste o seletor se necessário)
IWebElement btnEnviar = FindElementWithRetry(new List<By>
    {
        By.CssSelector("button[data-testid='next-button']"),
        By.XPath("//button[contains(text(), 'Avançar')]"),
        By.XPath("//button[@type='submit']")
    });

    btnEnviar.Click();

    Thread.Sleep(1000); // tempo para transição de página

    IWebElement btnAcessar = FindElementWithRetry(new List<By>
    {
        By.CssSelector("button[data-testid='next-button']"),
        By.XPath("//button[contains(text(), 'Acessar área do candidato')]")
    });

    btnAcessar.Click();
    Thread.Sleep(3000); // tempo para transição final
// 🟢 Login com usuário e senha fornecidos
    PreencherCampo("username-input", "candidato");
    PreencherCampo("password-input", "subscription");

    // Clicar no botão de login
    IWebElement btnLogin = FindElementWithRetry(new List<By>
    {
        By.CssSelector("button[data-testid='login-button']")
    });

    btnLogin.Click();
    Thread.Sleep(3000); // tempo para login e carregamento da dashboard
}


private void PreencherCampo(string dataTestId, string valor)
{
    try
    {
        IWebElement campo = FindElementWithRetry(new List<By> { By.CssSelector($"[data-testid='{dataTestId}']") });
        campo.Click();
        campo.Clear();
        campo.SendKeys(valor);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao preencher campo {dataTestId}: {ex.Message}");
    }
}

private void ClicarCampo(string dataTestId)
    {
        try
        {
            IWebElement campo = FindElementWithRetry(new List<By> { By.CssSelector($"[data-testid='{dataTestId}']") });
            campo.Click();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao clicar no campo {dataTestId}: {ex.Message}");
        }
    }

    // ... outros métodos ...


        [AfterScenario]
        public void AfterScenario()
        {
            try
            {
                Thread.Sleep(3000); // Pausa final para visualização
                _driver?.Quit();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao fechar navegador: {ex.Message}");
            }
        }

        #region Métodos Auxiliares

        private IWebElement FindElementWithRetry(List<By> selectors)
        {
            foreach (var selector in selectors)
            {
                try
                {
                    return _driver.FindElement(selector);
                }
                catch { continue; }
            }
            throw new NoSuchElementException($"Nenhum dos seletores foi encontrado: {string.Join(", ", selectors)}");
        }

        private void TakeScreenshot(string prefix)
        {
            try
            {
                var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                screenshot.SaveAsFile($"{prefix}_{timestamp}.png");
                Console.WriteLine($"Screenshot salvo como {prefix}_{timestamp}.png");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Falha ao tirar screenshot: {ex.Message}");
            }
        }

        #endregion
    }
}