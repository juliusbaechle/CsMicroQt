namespace MicroQt {
    public class Synchronizer {
        public Synchronizer(EventLoop a_eventLoop) {
            m_eventLoop = a_eventLoop;
        }

        public int Exec() {
            m_exit = false;
            while (!m_exit)
                m_eventLoop.Update();
            return m_exitCode;
        }

        public void Exit(int a_exitCode) {
            m_eventLoop.EnqueueEvent(() => {
                m_exitCode = a_exitCode;
                m_exit = true;
            });
        }

        public bool IsRunning() {
            return !m_exit;
        }

        private EventLoop m_eventLoop;
        private bool m_exit = true;
        private int m_exitCode = 0;
    }
}
