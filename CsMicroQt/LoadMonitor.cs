namespace MicroQt {
    internal class LoadMonitor {
        public void Update(uint a_busyMs, uint a_idleMs) {
            if (IntervalMs == 0)
                return;

            m_sumBusyMs += a_busyMs;
            m_sumIdleMs += a_idleMs;

            if (m_sumBusyMs + m_sumIdleMs > IntervalMs) {
                LogCpuUsage();
                m_sumBusyMs = 0;
                m_sumIdleMs = 0;
            }
        }

        private void LogCpuUsage() {
            uint cpuPerc = (100 * m_sumBusyMs) / (m_sumBusyMs + m_sumIdleMs);
            var msg = "Thread load: ";
            if (cpuPerc < 10) {
                msg += "  ";
            } else if (cpuPerc < 100) {
                msg += " ";
            }
            Logging.Log(msg + cpuPerc + " %");
        }

        public uint IntervalMs { get; set; } = 0;

        private uint m_sumBusyMs = 0;
        private uint m_sumIdleMs = 0;
    }
}
