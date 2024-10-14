using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    class Program
    {
        static void Main()
        {
            //BlackjackGame game = new BlackjackGame();
            //game.Start();

            MonteCarloSelectStrategy monte = new MonteCarloSelectStrategy();
            monte.engine = new BlackjackGame();
            monte.engine.InitializeGame();
            monte.strategy = new DefenciveDealerStrategy();

            Console.WriteLine(monte.AnalizeMonteCarlo(18));
        }
    }
}