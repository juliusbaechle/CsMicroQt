namespace MicroQt {
    public class MThread {
        public MThread() {
            m_thread = new(Run);
            MApplication.Instance.ThreadPool.Register(this);
        }

        internal MThread(Thread a_thread) {
            m_thread = a_thread;
            MApplication.Instance.ThreadPool.Register(this);
        }

        public void Dispose() {
            MApplication.Instance.ThreadPool.Unregister(Id);
        }

        public static MThread Current() {
            return MApplication.Instance.ThreadPool.Current();
        }

        public static int CurrentThreadId() {
            return Thread.CurrentThread.ManagedThreadId;
        }

        public void Start() {
            m_thread.Start();
        }
        
        public void Quit() {
            Exit(0);
        }

        public void Exit(int a_exitCode) {
            if (m_eventLoop != null)
                m_eventLoop.Exit(a_exitCode);
        }

        protected void Run() {
            Started();
            m_eventLoop = new();
            m_eventLoop.Exec();
            Finished();
        }

        public void Wait() {
            m_thread.Join();
        }

        public event Action Started = () => { };
        public event Action Finished = () => { };

        public int Id { get { return m_thread.ManagedThreadId; } }
        public MEventDispatcher EventDispatcher { get; } = new();

        private MEventLoop? m_eventLoop = null;
        private Thread m_thread;
    }
}
