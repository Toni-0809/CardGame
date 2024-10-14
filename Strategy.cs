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


    public class RandomDealerStrategy : ISelectionStrategy
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


    public class DefenciveDealerStrategy : ISelectionStrategy
    {
        public string Print()
        {
            return "Defencive";
        }

        public bool Select(int Score)
        {

            Console.WriteLine("Dealer's Turn:");

            while (Score < 15)
            {
                //Console.WriteLine("Decision: Not draw a card");
                return true;
            }
            return false;

        }
    }

    public class AgressiveDealerStrategy : ISelectionStrategy
    {

        public string Print()
        {
            return "Agressive";
        }

        public bool Select(int Score)
        {

            while (Score < 17)
            {

                //Console.WriteLine("Decision: Draw a card");
                return true;
            }
            return false;
        }
    }
}

public class MonteCarloSelectStrategy : ISelectionStrategy
{
    public ISelectionStrategy strategy;
    public BlackjackGame engine;
    bool ISelectionStrategy.Select(int Score)
    {
        return AnalizeMonteCarlo(Score) > 70;
    }

    public int AnalizeMonteCarlo(int score)
    {
       
        int numberOfIterations = 10000;

        int monteCarloWins = 0;


        for (int i = 0; i < numberOfIterations; i++)
        {
            Console.WriteLine($"Iteration: {i + 1}");
            List<Card> deckCopy = new List<Card>();
            engine.ShuffleDeck();
            engine.Deck.ForEach(
                x => deckCopy.Add(new Card(x.Suit, x.Rank)));
            int dealerScore = 0;

            List<Card> dealerCards = new List<Card>();
            while (strategy.Select(dealerScore))
            {
                dealerCards.Add(deckCopy[0]);
                deckCopy.RemoveAt(0);
                dealerScore = engine.CalculateScore(dealerCards);

            }
            Console.WriteLine($"Dealer Score: {dealerScore}, monteCarloWins: {monteCarloWins}");
            Console.WriteLine($"Remaining cards: {deckCopy.Count}");


            if (dealerScore > 21 || dealerScore <= score)
            {
                monteCarloWins++;
            }
            
        }

        return monteCarloWins;

    }

    string ISelectionStrategy.Print()
    {
        return "BestSelectStrategy";
    }
}



