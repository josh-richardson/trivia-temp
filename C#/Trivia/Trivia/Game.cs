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

        public bool CanStartGame()
        {
            return (GetPlayerCount() >= 2);
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
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(Players[currentPlayer] + " is getting out of the penalty box");
                    IncrementPlayerPosition(roll);
                    askQuestion();
                }
                else
                {
                    Console.WriteLine(Players[currentPlayer] + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {
                IncrementPlayerPosition(roll);
                askQuestion();
            }

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

        

        private void askQuestion()
        {
            if (GetCurrentCategory() == "Pop")
            {
                GetQuestion(popQuestions);
            }
            if (GetCurrentCategory() == "Science")
            {
                GetQuestion(scienceQuestions);
            }
            if (GetCurrentCategory() == "Sports")
            {
                GetQuestion(sportsQuestions);
            }
            if (GetCurrentCategory() == "Rock")
            {
                GetQuestion(rockQuestions);
            }
        }

        private void GetQuestion(LinkedList<string> questionStack)
        {
            Console.WriteLine(questionStack.First());
            questionStack.RemoveFirst();
        }

        private String GetCurrentCategory()
        {
            if (Places[currentPlayer] == 0) return "Pop";
            if (Places[currentPlayer] == 4) return "Pop";
            if (Places[currentPlayer] == 8) return "Pop";
            if (Places[currentPlayer] == 1) return "Science";
            if (Places[currentPlayer] == 5) return "Science";
            if (Places[currentPlayer] == 9) return "Science";
            if (Places[currentPlayer] == 2) return "Sports";
            if (Places[currentPlayer] == 6) return "Sports";
            if (Places[currentPlayer] == 10) return "Sports";
            return "Rock";
        }

        public bool CorrectPlayerAnswer()
        {
            if (IsInPenaltyBox[currentPlayer])
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    IncrementScore();

                    bool winner = hasCurrentPlayerWon();
                    MoveNextPlayer();

                    return winner;
                }
                else
                {
                    MoveNextPlayer();
                    return true;
                }
            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                IncrementScore();

                bool winner = hasCurrentPlayerWon();
                MoveNextPlayer();

                return winner;
            }
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


        private bool hasCurrentPlayerWon()
        {
            return !(Coins[currentPlayer] == coinsToWin);
        }
    }
}
