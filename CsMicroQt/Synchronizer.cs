namespace MicroQt {
    public class Synchronizer {
        public Synchronizer(EventLoop a_eventLoop) {
            m_eventLoop = a_eventLoop;
        }

        public int Exec() {
            IsRunning = true;
            while (IsRunning)
                m_eventLoop.Update();
            return m_exitCode;
        }

        public void Exit(int a_exitCode) {
            m_eventLoop.EnqueueEvent(() => {
                m_exitCode = a_exitCode;
                IsRunning = false;
            });
        }

        public bool IsRunning { get; private set; }

        private EventLoop m_eventLoop;
        private int m_exitCode = 0;
    }
}
