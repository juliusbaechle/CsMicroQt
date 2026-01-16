namespace MicroQt {
    public class MTimer : IDisposable {
        public MTimer(uint a_intervalMs = 0) {
            IntervalMs = a_intervalMs;
            m_eventDispatcher = MEventDispatcher.Current();
            m_taskId = m_eventDispatcher.RegisterTask(Update);
        }

        public void Dispose() {
            m_eventDispatcher.UnregisterTask(m_taskId);
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

        private uint m_taskId = 0;
        private uint m_timeElapsed = 0;
        private uint m_startMs = 0;
        private MEventDispatcher m_eventDispatcher;
    }
}
