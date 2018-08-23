using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UglyTrivia
{
    public class Game
    {
        List<string> Players = new List<string>();

        int[] Places = new int[6];
        int[] Coins = new int[6];

        bool[] IsInPenaltyBox = new bool[6];

        LinkedList<string> popQuestions = new LinkedList<string>();
        LinkedList<string> scienceQuestions = new LinkedList<string>();
        LinkedList<string> sportsQuestions = new LinkedList<string>();
        LinkedList<string> rockQuestions = new LinkedList<string>();

        int currentPlayer = 0;
        bool isGettingOutOfPenaltyBox;

        const int coinsToWin = 6;
        const int numberOfPlacesOnBoard = 12;

        public Game()
        {
            for (int i = 0; i < 50; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast(("Science Question " + i));
                sportsQuestions.AddLast(("Sports Question " + i));
                rockQuestions.AddLast(CreateRockQuestion(i));
            }
        }

        public String CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool AddPlayer(String playerName)
        {
            Players.Add(playerName);
            Places[GetPlayerCount()] = 0;
            Coins[GetPlayerCount()] = 0;
            IsInPenaltyBox[GetPlayerCount()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + Players.Count);
            return true;
        }

        public int GetPlayerCount()
        {
            return Players.Count;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(Players[currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);
            if (IsInPenaltyBox[currentPlayer])
            {
                isGettingOutOfPenaltyBox = (roll % 2 != 0);
                Console.WriteLine(Players[currentPlayer] + " is " + (isGettingOutOfPenaltyBox ? "" : "not ") + "getting out of the penalty box");
                if (!isGettingOutOfPenaltyBox) return;
            }
            IncrementPlayerPosition(roll);
            AskQuestion();
        }

        private void IncrementPlayerPosition(int roll)
        {
            Places[currentPlayer] = Places[currentPlayer] + roll;

            if (Places[currentPlayer] >= numberOfPlacesOnBoard)
                Places[currentPlayer] = Places[currentPlayer] - numberOfPlacesOnBoard;

            Console.WriteLine(Players[currentPlayer]
                              + "'s new location is "
                              + Places[currentPlayer]);
            Console.WriteLine("The category is " + GetCurrentCategory());
        }


        private void AskQuestion()
        {
            var category = GetCurrentCategory();
            var testDic = new Dictionary<string, LinkedList<string>>
            {
                {"Pop", popQuestions}, {"Science", scienceQuestions}, {"Sports", sportsQuestions}, {"Rock", rockQuestions}
            };
            var dictEntry = (testDic[category]);
            Console.WriteLine(dictEntry.First());
            dictEntry.RemoveFirst();
        }


        private String GetCurrentCategory()
        {
            var categorySequence = Places[currentPlayer] % 4;
            switch (categorySequence)
            {
                case 0:
                    return "Pop";
                case 1:
                    return "Science";
                case 2:
                    return "Sports";
                default:
                    return "Rock";
            }
        }

        public bool CorrectPlayerAnswer()
        {
            if (IsInPenaltyBox[currentPlayer] && !isGettingOutOfPenaltyBox)
            {
                MoveNextPlayer();
                return true;
            }

            Console.WriteLine("Answer was correct!!!!");
            IncrementScore();
            bool winner = HasCurrentPlayerWon();
            MoveNextPlayer();
            return winner;
        }

        private void IncrementScore()
        {
            Coins[currentPlayer]++;
            Console.WriteLine(Players[currentPlayer]
                              + " now has "
                              + Coins[currentPlayer]
                              + " Gold Coins.");
        }

        private void MoveNextPlayer()
        {
            currentPlayer++;
            if (currentPlayer == Players.Count) currentPlayer = 0;
        }

        public bool IncorrectPlayerAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(Players[currentPlayer] + " was sent to the penalty box");
            IsInPenaltyBox[currentPlayer] = true;

            currentPlayer++;
            if (currentPlayer == Players.Count) currentPlayer = 0;
            return true;
        }


        private bool HasCurrentPlayerWon()
        {
            return !(Coins[currentPlayer] == coinsToWin);
        }
    }
}
