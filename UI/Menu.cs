using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class Menu
    {
        private UIBoard m_GameBoard;

        public Menu()
        {
            m_GameBoard = new UIBoard();
        }

        public UIBoard Board
        {
            get
            {
                return m_GameBoard;
            }

            set
            {
                m_GameBoard = value;
            }
        }

        public void ShowMenu()
        {
            Console.WriteLine("Welcome to the game!\n");
            m_GameBoard.BoardSize = ChooseBoardNumber();
            m_GameBoard.PlayerType = ChoosePlayerType();
        }

        private static int ChooseBoardNumber()
        {
            int choice;
            Console.WriteLine("Please choose the board size:\n");
            Console.WriteLine("1. 3x3\n2. 4x4\n3. 5x5\n4. 6x6\n5. 7x7\n6. 8x8\n7. 9x9\n");
            int.TryParse(Console.ReadLine(), out choice);

            while (choice > 7 || choice < 1)
            {
                Console.WriteLine("Your choice is invalid!!! please choose a number between 1 to 7\n");
                int.TryParse(Console.ReadLine(), out choice);
            }

            return choice + 2;
        }

        private static int ChoosePlayerType()
        {
            int choice;
            Console.WriteLine("Press 1 if you want to play against a real player, or 2 to play against the computer:\n");
            int.TryParse(Console.ReadLine(), out choice);
            while (choice != 1 && choice != 2)
            {
                Console.WriteLine("Your choice is invalid!!! please press 1 or 2\n");
                int.TryParse(Console.ReadLine(), out choice);
            }

            return choice;
        }

        public bool CheckValidInput(out int o_Row, out int o_Col)
        {
            bool rowValid = false, colValid = false, quit = false;
            string rowStr, colStr;
            o_Row = -1;
            o_Col = -1;
            while ((!rowValid || !colValid) && (quit == false))
            {
                Console.WriteLine("Please enter a row:");
                rowStr = Console.ReadLine();
                Console.WriteLine("Please enter a column:");
                colStr = Console.ReadLine();
                if (rowStr == "Q" || colStr == "Q")
                {
                    quit = true;
                }
                else
                {
                    rowValid = int.TryParse(rowStr, out o_Row);
                    colValid = int.TryParse(colStr, out o_Col);
                    if (rowValid && colValid)
                    {
                        if ((o_Row > m_GameBoard.BoardSize) || (o_Row < 1) || (o_Col < 1) || (o_Col > m_GameBoard.BoardSize))
                        {
                            Console.WriteLine("Your input is out of the board bounds! please select valid numbers.");
                            rowValid = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Your input is invalid!");
                    }
                }
            }

            return quit;
        }
    }
}