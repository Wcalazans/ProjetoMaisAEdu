using Reqnroll;
using Reqnroll.BoDi;

namespace GrupoAEducation.Tests
{
    [Binding]
    public class TestHooks
    {
        private readonly IObjectContainer _objectContainer;

        public TestHooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            // Configurações adicionais podem ser feitas aqui
        }
    }
}