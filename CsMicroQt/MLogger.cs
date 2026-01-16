namespace MicroQt {
    public static class MLogger {
        public static Action<string> Log = (s) => { Console.WriteLine(s); };
    }
}
