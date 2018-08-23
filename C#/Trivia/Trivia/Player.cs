using System;

namespace Trivia
{
    class Player
    {
        public string Name { get; set; }
        public int Coins { get; set; }
        public int Place { get; set; }
        public bool IsInPenaltyBox { get; set; }
        public int Number { get; set; }


        public void IncrementPlayerPosition(int roll, int numberOfPlacesOnBoard)
        {
            Place += roll;
            if (Place >= numberOfPlacesOnBoard)
                Place -= numberOfPlacesOnBoard;
            Console.WriteLine(Name
                              + "'s new location is "
                              + Place);

        }

        public void IncrementScore()
        {
            Coins++;
            Console.WriteLine(Name
                              + " now has "
                              + Coins
                              + " Gold Coins.");
        }


        public bool HasWon(int coinsToWin)
        {
            return Coins != coinsToWin;
        }
    }


}
