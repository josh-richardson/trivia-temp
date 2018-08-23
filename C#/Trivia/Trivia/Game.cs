using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trivia;

namespace UglyTrivia
{
    public class Game
    {
        List<Player> Players = new List<Player>();

        LinkedList<string> popQuestions = new LinkedList<string>();
        LinkedList<string> scienceQuestions = new LinkedList<string>();
        LinkedList<string> sportsQuestions = new LinkedList<string>();
        LinkedList<string> rockQuestions = new LinkedList<string>();

        private Player _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;

        private const int CoinsToWin = 6;
        private int _numberOfPlayers = 0;
        private const int NumberOfPlacesOnBoard = 12;

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
            
            Players.Add(new Player() {Name= playerName, Place = 0 , Coins = 0, IsInPenaltyBox = false, Number = _numberOfPlayers});
            if (_numberOfPlayers == 0) _currentPlayer = Players.First();
            _numberOfPlayers++;
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
            Console.WriteLine(_currentPlayer.Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);
            if (_currentPlayer.IsInPenaltyBox)
            {
                _isGettingOutOfPenaltyBox = (roll % 2 != 0);
                Console.WriteLine(_currentPlayer.Name + " is " + (_isGettingOutOfPenaltyBox ? "" : "not ") + "getting out of the penalty box");
                if (!_isGettingOutOfPenaltyBox) return;
            }
            IncrementPlayerPosition(roll);
            AskQuestion();
        }

        private void IncrementPlayerPosition(int roll)
        {
            _currentPlayer.Place += roll;
            if (_currentPlayer.Place >= NumberOfPlacesOnBoard)
                _currentPlayer.Place -= NumberOfPlacesOnBoard;
            Console.WriteLine(_currentPlayer.Name
                              + "'s new location is "
                              + _currentPlayer.Place);
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
            var categorySequence = _currentPlayer.Place % 4;
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
            if (_currentPlayer.IsInPenaltyBox && !_isGettingOutOfPenaltyBox)
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
            _currentPlayer.Coins++;
            Console.WriteLine(_currentPlayer.Name
                              + " now has "
                              + _currentPlayer.Coins
                              + " Gold Coins.");
        }

        private void MoveNextPlayer()
        {
            _currentPlayer = Players.Find(x => x.Number == (_currentPlayer.Number + 1 ) % _numberOfPlayers);
        }

        public bool IncorrectPlayerAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_currentPlayer.Name + " was sent to the penalty box");
            _currentPlayer.IsInPenaltyBox = true;
            MoveNextPlayer();
            return true;
        }


        private bool HasCurrentPlayerWon()
        {
            return _currentPlayer.Coins != CoinsToWin;
        }
    }
}
