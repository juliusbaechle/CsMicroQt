namespace MicroQt {
    public static class Helpers {
        public static uint Millis() {
            return (uint)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
        }
    }
}
