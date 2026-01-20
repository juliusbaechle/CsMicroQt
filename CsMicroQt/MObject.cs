using System.Threading;

namespace MicroQt {
    public abstract class MObject : IDisposable {
        public MObject(MObject? parent = null) {
            ThreadId = parent != null ? parent.ThreadId : MThread.CurrentThreadId();
        }

        public virtual void Dispose() {

        }

        public int ThreadId { get; protected set; }
    }
}
