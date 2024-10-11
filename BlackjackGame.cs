using CardGame;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace CardGame { 

        public class BlackjackGame()
        {

            private const int WinScore = 21;//победнный счет
            //private const int StopScore = 17;// счет, при котором дилер останавливается
            private List<Card> deck; //список  для хранения колоды карт
            private Player player;
            private Player dealer;// 2 класса для игрока и дилера

        public List<Card> Deck { get => deck; set => deck = value; }

        public void Start()
            {
                InitializeGame();

                DealCards();

                PlayerTurn();
                if (player.Score > WinScore) return;

                DealerTurn();
                DetermineWin();
            }

            public void InitializeGame()
            {
            // Инициализируем колоду карт
            //InitializeDeck();
            Deck = CreateDeck();
            // Перемешиваем колоду
            ShuffleDeck();

                // Создаем игроков
                player = new Player("Player");
                dealer = new Player("Dealer");
            }

        public List<Card> CreateDeck()
        {
            List<Card> deck = new List<Card>();
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    deck.Add(new Card(suit, rank));
                }
            }

            return deck;
        }

        public void ShuffleDeck()
        {
            if (Deck == null || Deck.Count == 0)
            {
                throw new InvalidOperationException("Cannot shuffle an empty or null deck.");
            }

            Random rng = new Random();
            int n = Deck.Count;
            while (n > 1)
            {
                int k = rng.Next(n--);
                // Меняем местами
                var temp = Deck[n];
                Deck[n] = Deck[k];
                Deck[k] = temp;
            }
        }

            public void DealCards()
            {
                for (int i = 0; i < 2; i++)
                {
                    player.AddCard(DrawCard());
                    dealer.AddCard(DrawCard());
                }

                Console.WriteLine($"{player.Name}'s Cards: {player}");
                Console.WriteLine($"Dealer's Showing Card: {dealer.Cards[0]}");
            }

            public Card DrawCard() // берет первую карту из колоды (индекс 0) и убирает её из колоды, возвращая её.
            {
                Card card = Deck[0];
                Deck.RemoveAt(0);
                return card;
            }

            private void PlayerTurn()
            {
                while (player.Score < WinScore)
                {
                    Console.WriteLine($"Current Score: {player.Score}");
                    Console.Write("Do you want to hit (h) or stand (s)? ");
                    string choice = Console.ReadLine();
                    if (choice.ToLower() == "h")
                    {
                        player.AddCard(DrawCard());
                        Console.WriteLine($"{player.Name} draws: {player.Cards[^1]}");
                    }
                    else
                    {
                        break;
                    }
                }

                Console.WriteLine($"{player.Name}'s Final Score: {player.Score}");
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
        private void DealerTurn()
        {
            while (dealer.Score < 17)
            {
                dealer.AddCard(DrawCard());
                Console.WriteLine($"{dealer.Name} draws: {dealer.Cards[^1]}");
            }

            Console.WriteLine($"{dealer.Name}'s Final Score: {dealer.Score}");
        }

        private void DetermineWin()
            {
                if (player.Score > WinScore)
                    Console.WriteLine("Player busts! Dealer wins!");
                else if (dealer.Score > WinScore)
                    Console.WriteLine("Dealer busts! Player wins!");
                else if (player.Score > dealer.Score)
                    Console.WriteLine("Player wins!");
                else if (dealer.Score > player.Score)
                    Console.WriteLine("Dealer wins!");
                else
                    Console.WriteLine("It's a tie!");
            }
        }
    }





