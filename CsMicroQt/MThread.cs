namespace MicroQt {
    public class MThread : MObject {
        public MThread() {
            m_thread = new(Run);
            MEventDispatcherRegistry.Register(Id, new MEventDispatcher());
        }

        internal MThread(Thread a_thread) {
            m_thread = a_thread;
            MEventDispatcherRegistry.Register(Id, new MEventDispatcher());
        }

        public override void Dispose() {
            base.Dispose();
            MEventDispatcherRegistry.Unregister(Id);
        }

        public static MThread Current() {
            return new MThread(System.Threading.Thread.CurrentThread);
        }

        public static int CurrentThreadId() {
            return System.Threading.Thread.CurrentThread.ManagedThreadId;
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

        private MEventLoop? m_eventLoop = null;
        private Thread m_thread;
    }
}
