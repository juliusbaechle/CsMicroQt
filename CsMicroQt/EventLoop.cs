namespace MicroQt {
    public class EventLoop {
        public int Exec() {
            IsRunning = true;
            while (IsRunning)
                Update();
            return m_exitCode;
        }

        internal void Update() {
            var startMs = Helpers.Millis();
            foreach (var task in m_tasks.Values)
                task();
            var events = m_events;
            m_events = new();
            foreach (var e in events)
                e();
            m_loadMonitor.Update(Helpers.Millis() - startMs, 10);
            Thread.Sleep(10);
        }

        public bool IsRunning { get; private set; }

        public uint RegisterTask(Action a_task) {
            var id = m_nextTaskId++;
            m_tasks.Add(id, a_task);
            return id;
        }

        public void UnregisterTask(uint a_id) {
            m_tasks.Remove(a_id);
        }

        public void EnqueueEvent(Action a_action) {
            m_events.Add(a_action);
        }

        public void SetLogIntervalMs(uint a_ms) {
            m_loadMonitor.IntervalMs = a_ms;
        }

        public void Exit(int a_exitCode) {
            EnqueueEvent(() => {
                m_exitCode = a_exitCode;
                IsRunning = false;
            });
        }

        private LoadMonitor m_loadMonitor = new();
        private Dictionary<uint, Action> m_tasks = [];
        private uint m_nextTaskId = 0;
        private List<Action> m_events = [];
        private int m_exitCode = 0;
    }
}
