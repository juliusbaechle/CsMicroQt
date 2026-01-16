using MicroQt;

namespace MicroQtTests {
    [TestClass]
    public sealed class TestTimer {
        [TestMethod]
        public void Test() {
            MApplication app = new();

            MTimer timer1 = new();
            timer1.Elapsed += () => { 
                timer1.Dispose();
                app.Exit(1); 
            };
            timer1.Start(140);
            timer1.SingleShot = true;

            uint counter = 0;
            MTimer timer2 = new(50);
            timer2.Elapsed += () => {
                counter++;
                if (timer1.Active)
                    timer1.Stop();
                else
                    timer1.Resume();
            };
            timer2.Start();

            app.Exec();
            Assert.AreEqual((uint) 4, counter);
        }
    }
}
