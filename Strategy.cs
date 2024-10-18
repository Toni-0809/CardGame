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
            int monteCarloWins = 0;


            for (int i = 0; i < NUMBER_OF_ITERATIONS; i++)
            {

                //Console.WriteLine($"monte game {i + 1}\n monte score {score}");
                //Console.WriteLine($"Iteration: {i + 1}");
                List<Card> deckCopy = new List<Card>();
                engine.ShuffleDeck();
                engine.Deck.ForEach(
                    x => deckCopy.Add(new Card(x.Suit, x.Rank)));
                int dealerScore = 0;

                List<Card> dealerCards = new List<Card>();
                while (strategy.Select(dealerScore))
                {
                    dealerCards.Add(deckCopy[0]);
                    //Console.WriteLine(string.Join(",",dealerCards.Select(x=>x.Suit+x.Rank)));
                    deckCopy.RemoveAt(0);
                    dealerScore = engine.CalculateScore(dealerCards);

                }


                if (dealerScore > 21 || dealerScore < score)
                {
                    monteCarloWins++;
                }
                //Console.WriteLine($"Dealer Score: {dealerScore}, monteCarloWins: {monteCarloWins}");

            }

            return monteCarloWins;

        }

        string ISelectionStrategy.Print()
        {
            return "BestSelectStrategy";
        }
    }
}



