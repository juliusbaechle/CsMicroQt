using MicroQt;

namespace MicroQtTests {
    [TestClass]
    public sealed class TestEventLoop {

        [TestMethod]
        public void Test() {
            MApplication app = new();
            MEventLoop eventLoop = new();
            MEventDispatcher.Current().EnqueueEvent(() => {
                MEventDispatcher.Current().EnqueueEvent(() => {
                    eventLoop.Exit(1);
                });
                int i = eventLoop.Exec();
                app.Exit(i);
            });
            Assert.AreEqual(1, app.Exec());
        }
    }
}

// Thread exists --> Register EventDispatcher