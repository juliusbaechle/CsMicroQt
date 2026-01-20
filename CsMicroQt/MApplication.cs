namespace MicroQt {
    public class MApplication : MObject {
        public MApplication() : base() {
            Instance = this;
            MEventDispatcherRegistry.Register(new MEventDispatcher());
            m_eventLoop = new();
        }

        public override void Dispose() {
            MEventDispatcherRegistry.Unregister();
            base.Dispose();
        }

        public int Exec() { return m_eventLoop.Exec(); }
        public void Exit(int a_exitCode) { m_eventLoop.Exit(a_exitCode); }

        private MEventLoop m_eventLoop;

        public static MApplication? Instance { get; private set; } = null;
    }
}
