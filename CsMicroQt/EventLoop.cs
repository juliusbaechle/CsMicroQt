namespace MicroQt {
    public class MEventLoop {
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
        }

        public bool IsRunning { get; private set; }

        internal uint RegisterTask(Action a_task) {
            var id = m_nextTaskId++;
            m_tasks.Add(id, a_task);
            return id;
        }

        internal void UnregisterTask(uint a_id) {
            m_tasks.Remove(a_id);
        }

        public void EnqueueEvent(Action a_action) {
            m_events.Add(a_action);
        }

        public void Exit(int a_exitCode) {
            EnqueueEvent(() => {
                m_exitCode = a_exitCode;
                IsRunning = false;
            });
        }

        private Dictionary<uint, Action> m_tasks = [];
        private uint m_nextTaskId = 0;
        private List<Action> m_events = [];
        private int m_exitCode = 0;
    }
}
