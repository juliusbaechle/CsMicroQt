using MicroQt;

namespace MicroQtTests {
    [TestClass]
    public sealed class Tests {
        [TestMethod]
        public void TestTimers() {
            EventLoop eventLoop = new();

            MicroQt.Timer timer1 = new(eventLoop, 140);
            timer1.Elapsed += () => { eventLoop.Exit(1); };
            timer1.Start();

            uint counter = 0;
            MicroQt.Timer timer2 = new(eventLoop, 50);
            timer2.Elapsed += () => {
                counter++;
                if (timer1.Active)
                    timer1.Stop();
                else
                    timer1.Resume();
            };
            timer2.Start();

            eventLoop.Exec();
            Assert.AreEqual((uint) 4, counter);
        }

        [TestMethod]
        public void TestSynchronizer() {
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
