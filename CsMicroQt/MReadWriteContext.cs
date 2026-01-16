namespace JsonRpc {
    public class MReadContext : IDisposable {
        public MReadContext(ReaderWriterLock a_lock, int a_ms = -1) {
            m_lock = a_lock;
            m_lock.AcquireReaderLock(a_ms);
        }

        public void Dispose() {
            m_lock.ReleaseReaderLock();
        }

        ReaderWriterLock m_lock;
    }

    public class MWriteContext : IDisposable {
        public MWriteContext(ReaderWriterLock a_lock, int a_ms = -1) {
            m_lock = a_lock;
            m_lock.AcquireWriterLock(a_ms);
        }

        public void Dispose() {
            m_lock.ReleaseWriterLock();
        }

        ReaderWriterLock m_lock;
    }
}
