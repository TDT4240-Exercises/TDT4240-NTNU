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
            using (Task3Game game = new Task3Game())
            {
                game.Run();
            }
        }
    }
#endif
}

