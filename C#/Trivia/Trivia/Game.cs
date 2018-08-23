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

        LinkedList<Question> questions = new LinkedList<Question>();
        
        private Player _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;

        private const int CoinsToWin = 6;
        private int _numberOfPlayers = 0;
        private const int NumberOfPlacesOnBoard = 12;

        public Game()
        {
            for (int i = 0; i < 50; i++)
            {
                questions.AddLast(new Question() { QuestionBody = "Pop Question " + i, Type = QuestionType.Pop });
                questions.AddLast(new Question() { QuestionBody = "Science Question " + i, Type = QuestionType.Science });
                questions.AddLast(new Question() { QuestionBody = "Sports Question " + i, Type = QuestionType.Sports});
                questions.AddLast(new Question() { QuestionBody = "Rock Question " + i, Type = QuestionType.Rock });
                //questions.AddLast(new Question() { QuestionBody = "Joshua Question " + i, Type = QuestionType.Joshua });
            }
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
            _currentPlayer.IncrementPlayerPosition(roll, NumberOfPlacesOnBoard);
            Console.WriteLine("The category is " + (QuestionType)(_currentPlayer.Place % Question.GetNumberOfTypes()));
            AskQuestion();
        }

        private void AskQuestion()
        {
            var category = (QuestionType)(_currentPlayer.Place % Question.GetNumberOfTypes());
            var dictEntry = (questions.First(x => x.Type == category));
            Console.WriteLine(dictEntry.QuestionBody);
            questions.Remove(dictEntry);
        }

        public bool CorrectPlayerAnswer()
        {
            if (_currentPlayer.IsInPenaltyBox && !_isGettingOutOfPenaltyBox)
            {
                MoveNextPlayer();
                return true;
            }
            Console.WriteLine("Answer was correct!!!!");
            _currentPlayer.IncrementScore();
            var winner = _currentPlayer.HasWon(CoinsToWin);
            MoveNextPlayer();
            return winner;
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

       
    }
}
