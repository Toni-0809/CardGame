using CardGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{

    public interface ISelectionStrategy
    {
        bool Select(int Score);
        string Print();
    }

    public class RandomStrategy : ISelectionStrategy
    {
        public string Print()
        {
            return "Random";
        }

        public bool Select(int Score)
        {
            Random random = new Random();
           
                return random.Next(1, 10) % 2 == 0;
        }
    }

    public class DealerStrategy : ISelectionStrategy
    {
        public string Print()
        {
            return "DealerStrategy";
        }

        public bool Select(int Score)
        {

            while (Score <= 17)
            {
                return true;
            }
            return false;
        }
    }

    public class DefenciveStrategy : ISelectionStrategy
    {
        public string Print()
        {
            return "DefenciveStrategy";
        }

        public bool Select(int Score)
        {

            while (Score >=17)
            {
                return false;
            }
            return true;
        }
    }

    public class AgressiveStrategy : ISelectionStrategy
    {
        public string Print()
        {
            return " AgressiveStrategy";
        }

        public bool Select(int Score)
        {

            while (Score <= 14)
            {
                return true;
            }
            return false;
        }
                
    }

    public class MonteCarloSelectStrategy : ISelectionStrategy
    {
        private const int NUMBER_OF_ITERATIONS = 10000;
        public ISelectionStrategy strategy;
        public BlackjackGame engine;
        bool ISelectionStrategy.Select(int Score)
        {
            return AnalizeMonteCarlo(Score) <= NUMBER_OF_ITERATIONS * 0.7;
        }

        public int AnalizeMonteCarlo(int score)
        {
            int  monteCarloWins = 0;
            
            int[] results = new int[Environment.ProcessorCount]; // Массив для хранения выигрышей от каждой задачи
            object lockObj = new object(); // Для синхронизации доступа к monteCarloWins


            Parallel.For(0, NUMBER_OF_ITERATIONS, (i) =>
            {
           
               //Console.WriteLine($"monte game {i + 1}\n monte score {score}");
               //Console.WriteLine($"Iteration: {i + 1}");
               List<Card> deckCopy = new List<Card>();
               lock (engine) 
               {
                   deckCopy = new List<Card>(engine.Deck); // Копируем колоду
                   engine.ShuffleDeck();
               }
               int dealerScore = 0;

               List<Card> dealerCards = new List<Card>();
               while (dealerScore < 17)
               {
                   dealerCards.Add(deckCopy[0]);
                   //Console.WriteLine(string.Join(",",dealerCards.Select(x=>x.Suit+x.Rank)));
                   deckCopy.RemoveAt(0);
                   dealerScore = engine.CalculateScore(dealerCards);

               }


               if (dealerScore > 21 || dealerScore < score)
               {
                   lock (lockObj)
                   {
                       monteCarloWins++;
                   }
               }
           });

            return monteCarloWins; 
        }

        string ISelectionStrategy.Print()
        {
            return "BestSelectStrategy";
        }
    }
}



