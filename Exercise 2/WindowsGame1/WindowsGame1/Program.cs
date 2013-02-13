using System;

namespace Exercise2
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Exercise2Game game = new Exercise2Game())
            {
                game.Run();
            }
        }
    }
#endif
}

