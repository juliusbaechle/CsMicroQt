using MicroQt;

namespace MicroQtTests {
    [TestClass]
    public sealed class TestTimer {
        [TestMethod]
        public void Test() {
            MEventLoop eventLoop = new();

            MTimer timer1 = new(eventLoop);
            timer1.Elapsed += () => { 
                timer1.Dispose(); 
                eventLoop.Exit(1); 
            };
            timer1.Start(140);
            timer1.SingleShot = true;

            uint counter = 0;
            MTimer timer2 = new(eventLoop, 50);
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
    }
}
