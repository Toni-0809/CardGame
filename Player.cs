using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGame;


namespace CardGame
    {
        public class Player
        {
            public string Name { get; }
            public List<Card> Cards { get; }
            public int Score => CalculateScore(Cards);

            public Player(string name)
            {
                Name = name;
                Cards = new List<Card>();
            }

            public void AddCard(Card card)
            {
                Cards.Add(card);
            }

            public int CalculateScore(List<Card> cards)
            {
                int score = 0;
                int aceCount = 0;

                foreach (var card in cards)
                {
                    if (int.TryParse(card.Rank, out int cardValue))
                    {
                        score += cardValue;
                    }
                    else if (card.Rank == "A")
                    {
                        score += 11;
                        aceCount++;
                    }
                    else
                    {
                        score += 10; // J, Q, K
                    }
                }

                while (score > 21 && aceCount > 0)
                {
                    score -= 10; // Adjust for Aces
                    aceCount--;
                }

                return score;
            }

            public override string ToString()
            {
                return string.Join(", ", Cards);
            }
        }
    }

    