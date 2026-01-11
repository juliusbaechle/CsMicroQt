namespace MicroQt {
    public static class Logging {
        public static Action<string> Log = (s) => { Console.WriteLine(s); };
    }
}
