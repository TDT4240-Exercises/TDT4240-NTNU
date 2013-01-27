using System;

namespace Exercise1
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Task1Game game = new Task1Game())
            {
                game.Run();
            }
        }
    }
#endif
}

