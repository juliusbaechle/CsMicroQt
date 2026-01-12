namespace MicroQt {
    public static class MLogging {
        public static Action<string> Log = (s) => { Console.WriteLine(s); };
    }
}
