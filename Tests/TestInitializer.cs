namespace MicroQtTests {
    [TestClass]
    public static class TestInitializer {
        [AssemblyInitialize]
        public static void Initialize(TestContext context) {
            MicroQt.Logging.Log = (s) => { System.Diagnostics.Debug.WriteLine(s); };
        }
    }
}
