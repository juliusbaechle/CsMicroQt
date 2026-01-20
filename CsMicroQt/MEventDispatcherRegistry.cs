namespace MicroQt {
    public static class MEventDispatcherRegistry {
        public static MEventDispatcher Current() {
            return Get(MThread.CurrentThreadId());
        }

        internal static MEventDispatcher Get(int a_threadId) {
            lock(m_lock) {
                if (!m_eventDispatchers.ContainsKey(a_threadId))
                    Register(MThread.CurrentThreadId(), new MEventDispatcher());
                return m_eventDispatchers[a_threadId];
            }
        }

        internal static void Register(MEventDispatcher a_dispatcher) {
            Register(MThread.CurrentThreadId(), a_dispatcher);
        }

        internal static void Register(int a_threadId, MEventDispatcher a_dispatcher) {
            lock(m_lock) {
                m_eventDispatchers.Add(a_threadId, a_dispatcher);
            }
        }

        internal static void Unregister() {
            Unregister(MThread.CurrentThreadId());
        }

        internal static void Unregister(int a_threadId) {
            lock(m_lock) {
                m_eventDispatchers.Remove(a_threadId);
            }
        }

        private static Dictionary<int, MEventDispatcher> m_eventDispatchers = [];
        private static Lock m_lock = new();
    }
}
