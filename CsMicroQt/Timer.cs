namespace MicroQt {
    public class Timer : IDisposable {
        public Timer(EventLoop a_eventLoop, uint a_intervalMs = 0) {
            m_eventLoop = a_eventLoop;
            IntervalMs = a_intervalMs;
            m_taskId = m_eventLoop.RegisterTask(Update);
        }

        public void Dispose() {
            m_eventLoop.UnregisterTask(m_taskId);
        }

        public void Start() {
            m_timeElapsed = 0;
            m_startMs = Helpers.Millis();
            Active = true;
        }

        public void Start(uint a_intervalMs) {
            IntervalMs = a_intervalMs;
            Start();
        }

        public void Resume() {
            m_startMs = Helpers.Millis();
            Active = true;
        }

        public void Stop() {
            m_timeElapsed += Helpers.Millis() - m_startMs;
            Active = false;
        }

        private void Update() {
            if (!Active)
                return;

            if (m_timeElapsed + (Helpers.Millis() - m_startMs) >= IntervalMs) {
                if (SingleShot) {
                    Active = false;
                } else {
                    Start();
                }
                Elapsed();
            }
        }

        public event Action Elapsed = () => { };

        public bool SingleShot { get; set; } = false;
        public uint IntervalMs { get; set; } = 0;
        public bool Active { get; private set; } = false;

        private EventLoop m_eventLoop;
        private uint m_taskId = 0;
        private uint m_timeElapsed = 0;
        private uint m_startMs = 0;
    }
}
