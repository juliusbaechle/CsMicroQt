using MicroQt;

namespace MicroQtTests {
    [TestClass]
    public sealed class TestSynchronizer {

        [TestMethod]
        public void Test() {
            EventLoop eventLoop = new();
            Synchronizer synchronizer = new(eventLoop);
            eventLoop.EnqueueEvent(() => {
                eventLoop.EnqueueEvent(() => {
                    synchronizer.Exit(1);
                });

                int i = synchronizer.Exec();
                eventLoop.Exit(i);
            });
            Assert.AreEqual(1, eventLoop.Exec());
        }
    }
}
