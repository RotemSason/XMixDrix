using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UIBoard
    {
        private int m_BoardSize;
        private int m_PlayerType;
        private GameLogic.Game m_Game;

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }

            set
            {
                m_BoardSize = value;
            }
        }

        public int PlayerType
        {
            get
            {
                return m_PlayerType;
            }

            set
            {
                m_PlayerType = value;
            }
        }

        public UIBoard()
        {
            m_Game = new GameLogic.Game();
        }

        public void drawBoard()
        {
            for (int i = 1; i <= m_BoardSize; i++)
            {
                Console.Write("   " + i);
            }

            Console.Write("\n");
            for (int i = 1; i < m_BoardSize + 1; i++)
            {
                Console.Write(i + "| ");
                for (int j = 0; j < m_BoardSize; j++)
                {
                    if (m_Game.Board[i - 1, j] == 1)
                    {
                        Console.Write('X');
                    }
                    else if (m_Game.Board[i - 1, j] == 2)
                    {
                        Console.Write('O');
                    }
                    else
                    {
                        Console.Write(' ');
                    }

                    Console.Write(" | ");
                }

                Console.Write("\n");
                Console.Write(" =");
                for (int j = 0; j < m_BoardSize; j++)
                {
                    Console.Write("====");
                }

                Console.Write("\n");
            }
        }

        public void StartGame()
        {
            bool foundWinner = false, exit = false;
            int winnerId, chooseifExit;

            m_Game.InitGame(m_BoardSize, m_PlayerType);
            Ex02.ConsoleUtils.Screen.Clear();
            drawBoard();

            while (exit == false)
            {
                while (foundWinner == false)
                {
                    foundWinner = PlayerStep(1);

                    if (foundWinner == false)
                    {
                        if (PlayerType == 2)
                        {
                            m_Game.MakeRandomMove(2, out winnerId);
                            Ex02.ConsoleUtils.Screen.Clear();
                            drawBoard();
                            PrintWinner(winnerId, ref foundWinner);
                        }
                        else
                        {
                            foundWinner = PlayerStep(2);
                        }
                    }
                }

                Console.WriteLine("press 1 to continue another round or 2 for exit");
                int.TryParse(Console.ReadLine(), out chooseifExit);

                while (chooseifExit != 1 && chooseifExit != 2)
                {
                    Console.WriteLine("Your choice is invalid!!! please choose 1 or 2\n");
                    int.TryParse(Console.ReadLine(), out chooseifExit);
                }

                if (chooseifExit == 2)
                {
                    exit = true;
                }

                if (chooseifExit == 1)
                {
                    m_Game.ResetDataGame();
                    foundWinner = false;
                    Ex02.ConsoleUtils.Screen.Clear();
                    drawBoard();
                }
            }
        }

        public bool PlayerStep(int i_PlayerId)
        {
            bool isQuit, isEmpty = false, foundWinner = false;
            int row, col = 0, winnerId = 0;
            Console.Write("Player " + i_PlayerId + " ");
            isQuit = CheckValidInput(out row, "row");
            if (!isQuit)
            {
                isQuit = CheckValidInput(out col, "column");
            }

            while (!isEmpty && !isQuit)
            {
                isEmpty = m_Game.MakeRealMove(i_PlayerId, row, col, out winnerId);
                if (!isEmpty)
                {
                    Console.WriteLine("This cell is not empty,please choose another one!");
                    isQuit = CheckValidInput(out row, "row");
                    if (!isQuit)
                    {
                        isQuit = CheckValidInput(out col, "column");
                    }
                }
            }

            Ex02.ConsoleUtils.Screen.Clear();
            if (isQuit)
            {
                foundWinner = true;
                Console.WriteLine("Game over!! Player " + i_PlayerId + " quit :(");
            }
            else
            {
                drawBoard();
                PrintWinner(winnerId, ref foundWinner);
            }

            return foundWinner;
        }

        public bool CheckValidInput(out int o_CellNumber, string i_TypeCell)
        {
            bool cellValid = false, quit = false;
            string cellStr;
            o_CellNumber = -1;
            while ((!cellValid) && (quit == false))
            {
                Console.WriteLine("please enter a " + i_TypeCell);
                cellStr = Console.ReadLine();
                if (cellStr == "Q")
                {
                    quit = true;
                }
                else
                {
                    cellValid = int.TryParse(cellStr, out o_CellNumber);
                    if (cellValid)
                    {
                        if ((o_CellNumber > m_BoardSize) || (o_CellNumber < 1))
                        {
                            Console.WriteLine("Your input is out of the board bounds! please select valid numbers.");
                            cellValid = false;
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

        public void PrintWinner(int i_WinnerId, ref bool io_FoundWinner)
        {
            if (i_WinnerId != 0)
            {
                if (i_WinnerId == -1)
                {
                    io_FoundWinner = true;
                    Console.WriteLine("Game over! there is a tie");
                }
                else
                {
                    io_FoundWinner = true;
                    Console.WriteLine("Game over!! the winner is player " + i_WinnerId + "\n");
                }

                Console.WriteLine("POINTS:");
                Console.WriteLine("Player 1: " + m_Game.Player1.WinNumber);
                if (m_PlayerType == 1)
                {
                    Console.WriteLine("Player 2: " + m_Game.Player2.WinNumber);
                }
                else
                {
                    Console.WriteLine("Computer: " + m_Game.Player2.WinNumber);
                }
            }
        }
    }
}
