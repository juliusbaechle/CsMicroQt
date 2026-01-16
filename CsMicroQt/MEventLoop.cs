namespace MicroQt {
    public class MEventLoop {
        public MEventLoop() {
            m_eventDispatcher = MEventDispatcher.Current();
        }

        public int Exec() {
            IsRunning = true;
            while (IsRunning)
                m_eventDispatcher.Update();
            return m_exitCode;
        }

        public bool IsRunning { get { return m_isRunning; } private set { m_isRunning = value; } }
        private volatile bool m_isRunning = false;

        public void Exit(int a_exitCode) {
            m_eventDispatcher.EnqueueEvent(() => {
                m_exitCode = a_exitCode;
                IsRunning = false;
            });
        }

        MEventDispatcher m_eventDispatcher;
        private volatile int m_exitCode = 0;
    }
}
