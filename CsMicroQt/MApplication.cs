namespace MicroQt {
    public class MApplication : IDisposable {
        public MApplication() {
            if (Instance != null)
                throw new Exception("Only one MApplication can be created");
            Instance = this;

            MainThread = new MThread(Thread.CurrentThread);
            m_eventLoop = new();
        }

        public void Dispose() {
            ThreadPool.Unregister(MThread.CurrentThreadId());
            Instance = null;
        }

        public int Exec() { return m_eventLoop.Exec(); }
        public void Exit(int a_exitCode) { m_eventLoop.Exit(a_exitCode); }
        public MThread MainThread { get; private set; }

        public static MApplication? Instance { get; private set; } = null;
        public MThreadPool ThreadPool { get; } = new();

        private MEventLoop m_eventLoop;
    }
}
