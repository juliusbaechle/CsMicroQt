using MicroQt;

namespace MicroQtTests {
    [TestClass]
    public sealed class TestMultithreading {
        [TestMethod]
        public void Test() {
            MApplication app = new();
            MThread thread = new();
            thread.Started += () => {
                MTimer timer = new(1000);
                timer.Elapsed += () => {
                    thread.Exit(0);
                    app.Exit(0);
                    Assert.AreEqual(thread.Id, MThread.CurrentThreadId());
                };
                timer.Start();
            };
            thread.Start();
            Assert.AreEqual(0, app.Exec());
        }
    }
}
