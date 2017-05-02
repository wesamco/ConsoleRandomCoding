using System;
using System.Text;
using System.Threading;

namespace ConsoleRandomRenderer
{
    class Program
    {
        static Renderer renderer = new Renderer(60, () =>
        {
            Update();
            return 0;
        });

        public static void Main(string[] args)
        {
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
            renderer.Start();
        }

        public static void Update()
        {
            Console.Clear();
            randomPaint();
        }

        static Random random = new Random();
        static ConsoleColor RandomColor()
        {
            Array values = Enum.GetValues(typeof(ConsoleColor));
            ConsoleColor randomColor = (ConsoleColor)values.GetValue(random.Next(values.Length));
            return randomColor;
        }
        static void randomPaint()
        {
            for (int i = random.Next(Console.WindowHeight); i < Console.WindowHeight; i++)
            {
                Print(RandomString(random.Next(Console.WindowWidth)), RandomColor());
            }
        }
        static void Print(object obj, ConsoleColor color)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(obj);
            Console.ForegroundColor = current;
        }
        private static string RandomString(int Size)
        {
            string input = "abcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < Size; i++)
            {
                ch = input[random.Next(0, input.Length)];
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }

    class Renderer
    {
        public Func<object> Callback;

        public int Fps { get; set; }

        public Renderer(int fps, Func<object> Callback)
        {
            this.Fps = fps;
            this.Callback = Callback;
        }
        private bool running = false;
        public bool Running
        {
            get { return this.running; }
            set {
                this.running = value;
            }
        }

        private void Run()
        {
            while (running)
            {
                Thread.Sleep(1000 / Fps);
                Callback.DynamicInvoke();
            }
        }
        internal void Start()
        {
            Running = true;
            Run();
        }
        internal void Stop()
        {
            Running = false;
        }
    }
}