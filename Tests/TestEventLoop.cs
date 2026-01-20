using MicroQt;

namespace MicroQtTests {
    [TestClass]
    public sealed class TestEventLoop {

        [TestMethod]
        public void Test() {
            MApplication app = new();
            MEventLoop eventLoop = new();
            MEventDispatcherRegistry.Current().EnqueueEvent(() => {
                MEventDispatcherRegistry.Current().EnqueueEvent(() => {
                    eventLoop.Exit(1);
                });
                int i = eventLoop.Exec();
                app.Exit(i);
            });
            Assert.AreEqual(1, app.Exec());
        }
    }
}