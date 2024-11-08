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
            const int ITERATIONS_COUNT = 10000;
            int playerWins = 0;
            Parallel.For(0, ITERATIONS_COUNT, (i) =>
            {
                Console.WriteLine($"game {i + 1} first wins:{playerWins}");
                BlackjackGame game = new BlackjackGame();
                game.Start(new AgressiveStrategy(), new DealerStrategy());
                playerWins += Convert.ToInt32(game.PlayerWins);
            });
            Console.WriteLine($"{((double)playerWins/ITERATIONS_COUNT) *100} %");


        }
    }
}