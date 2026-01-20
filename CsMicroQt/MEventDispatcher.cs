namespace MicroQt {
    public class MEventDispatcher : MObject {
        internal void Update() {
            List<Action> tasks = [];
            List<Action> events = [];

            lock(m_lock) {
                tasks = m_tasks.Values.ToList();
                events = m_events;
                m_events = new();
            }

            foreach (var task in tasks)
                task();
            foreach (var e in events)
                e();
        }

        public void EnqueueEvent(Action a_event) {
            lock (m_lock) {
                m_events.Add(a_event);
            }
        }

        internal uint RegisterTask(Action a_task) {
            lock (m_lock) {
                var id = m_nextTaskId++;
                m_tasks.Add(id, a_task);
                return id;
            }
        }

        internal void UnregisterTask(uint a_id) {
            lock(m_lock) {
                m_tasks.Remove(a_id);
            }
        }

        private Lock m_lock = new();
        private Dictionary<uint, Action> m_tasks = [];
        private uint m_nextTaskId = 0;
        private List<Action> m_events = [];
    }
}
