using MicroQt;

namespace MicroQtTests {
    [TestClass]
    public static class TestInitializer {
        [AssemblyInitialize]
        public static void Initialize(TestContext context) {
            MLogger.Log = (s) => { System.Diagnostics.Debug.WriteLine(s); };
        }
    }
}
