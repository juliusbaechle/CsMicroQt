namespace MicroQt {
    public class MEventLoop : MObject {
        public MEventLoop() : base() {
            m_eventDispatcher = MEventDispatcherRegistry.Current();
        }

        public int Exec() {
            IsRunning = true;
            while (IsRunning)
                m_eventDispatcher.Update();
            return m_exitCode;
        }

        public void Exit(int a_exitCode) {
            m_eventDispatcher.EnqueueEvent(() => {
                m_exitCode = a_exitCode;
                IsRunning = false;
            });
        }

        public bool IsRunning { get; private set; }

        MEventDispatcher m_eventDispatcher;
        private int m_exitCode = 0;
    }
}
