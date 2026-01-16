using JsonRpc;

namespace MicroQt {
    public class MThreadPool {
        public MThread Current() {
            return Get(MThread.CurrentThreadId());
        }

        public MThread Get(int a_threadId) {
            using (new MReadContext(m_lock)) {
                if (!m_threads.ContainsKey(a_threadId))
                    throw new Exception("Thread not registered");
                return m_threads[a_threadId];
            }
        }

        public void Register(MThread a_thread) {
            using (new MWriteContext(m_lock)) {
                m_threads.Add(a_thread.Id, a_thread);
            }
        }

        public void Unregister(int a_threadId) {
            using (new MWriteContext(m_lock)) {
                m_threads.Remove(a_threadId);
            }
        }

        private ReaderWriterLock m_lock = new();
        private Dictionary<int, MThread> m_threads = [];
    }
}
