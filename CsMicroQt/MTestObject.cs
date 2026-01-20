namespace MicroQt {
    public class MTestObject : IDisposable {
        public MTestObject() {
            MLogger.Log("Creating MTestObject for thread " + Thread.CurrentThread.ManagedThreadId);
        }
        
        public void Print() {
            MLogger.Log("Called MTestObject for thread " + Thread.CurrentThread.ManagedThreadId);
        }

        public void Dispose() {
            MLogger.Log("Disposing MTestObject for thread " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
