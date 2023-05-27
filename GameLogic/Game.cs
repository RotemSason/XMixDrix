using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Game
    {
        private Player m_Player1;
        private Player m_Player2;
        private int[,] m_Board;
        private int[] m_RowCounterArr;
        private int[] m_ColCounterArr;
        private int[] m_SlantCounterArr;
        private int m_FullCellsCounter;

        public int[,] Board
        {
            get
            {
                return m_Board;
            }

            set
            {
                m_Board = value;
            }
        }

        public Player Player1
        {
            get
            {
                return m_Player1;
            }

            set
            {
                m_Player1 = value;
            }
        }

        public Player Player2
        {
            get
            {
                return m_Player2;
            }

            set
            {
                m_Player2 = value;
            }
        }

        public static void Main()
        {
        }

        public bool MakeRealMove(int i_PlayerId, int i_Row, int i_Col, out int o_WinnerId)
        {
            o_WinnerId = 0;
            bool isEmpty = CheckIfCellIsEmpty(i_Row, i_Col);
            if (isEmpty)
            {
                m_FullCellsCounter++;
                m_Board[i_Row - 1, i_Col - 1] = i_PlayerId;
                UpdateCounterArr(i_Row, i_Col);
                bool isWin = CheckWinning(i_Row, i_Col, out o_WinnerId);
            }

            return isEmpty;
        }

        public void MakeRandomMove(int i_PlayerId, out int o_WinnerId)
        {
            int row, col;
            bool isEmpty;
            o_WinnerId = 0;
            Random rnd = new Random();
            row = rnd.Next(1, m_Board.GetLength(0) + 1);
            col = rnd.Next(1, m_Board.GetLength(1) + 1);
            isEmpty = CheckIfCellIsEmpty(row, col);
            while (!isEmpty)
            {
                row = rnd.Next(1, m_Board.GetLength(0) + 1);
                col = rnd.Next(1, m_Board.GetLength(1) + 1);
                isEmpty = CheckIfCellIsEmpty(row, col);
            }

            m_Board[row - 1, col - 1] = i_PlayerId;
            m_FullCellsCounter++;
            UpdateCounterArr(row, col);
            CheckWinning(row, col, out o_WinnerId);
        }

        public bool CheckWinning(int i_Row, int i_Col, out int o_Winnerid)
        {
            bool isWin = false;
            o_Winnerid = 0;

            if (m_FullCellsCounter == m_Board.GetLength(0) * m_Board.GetLength(1))
            {
                o_Winnerid = -1; // in case of tie
                Player1.WinNumber++;
                Player2.WinNumber++;
            }
            else if (CheckRowWinning(i_Row) || CheckColWinning(i_Col) || CheckLeftSlantWinning() || CheckRightSlantWinning())
            {
                isWin = true;

                if (m_Player1.Id != m_Board[i_Row - 1, i_Col - 1])
                {
                    m_Player1.WinNumber++;
                    o_Winnerid = m_Player1.Id;
                }
                else
                {
                    m_Player2.WinNumber++;
                    o_Winnerid = m_Player2.Id;
                }
            }

            return isWin;
        }

        public bool CheckRowWinning(int i_Row)
        {
            int tmp;
            bool isWin = false;
            if (m_RowCounterArr[i_Row - 1] == m_Board.GetLength(0))
            {
                tmp = m_Board[i_Row - 1, 0];
                isWin = true;
                for (int i = 1; i < m_Board.GetLength(1); i++)
                {
                    if (m_Board[i_Row - 1, i] != tmp)
                    {
                        isWin = false;
                    }
                }
            }

            return isWin;
        }

        public bool CheckColWinning(int i_Col)
        {
            int tmp;
            bool isWin = false;
            if (m_ColCounterArr[i_Col - 1] == m_Board.GetLength(1))
            {
                tmp = m_Board[0, i_Col - 1];
                isWin = true;
                for (int i = 1; i < m_Board.GetLength(0); i++)
                {
                    if (m_Board[i, i_Col - 1] != tmp)
                    {
                        isWin = false;
                    }
                }
            }

            return isWin;
        }

        public bool CheckLeftSlantWinning()
        {
            int tmp;
            bool isWin = false;
            if (m_SlantCounterArr[0] == m_Board.GetLength(1))
            {
                tmp = m_Board[0, 0];
                isWin = true;
                for (int i = 1; i < m_Board.GetLength(0); i++)
                {
                    if (m_Board[i, i] != tmp)
                    {
                        isWin = false;
                    }
                }
            }

            return isWin;
        }

        public bool CheckRightSlantWinning()
        {
            int tmp;
            bool isWin = false;
            if (m_SlantCounterArr[1] == m_Board.GetLength(1))
            {
                tmp = m_Board[0, m_Board.GetLength(1) - 1];
                isWin = true;
                for (int i = 1; i < m_Board.GetLength(0); i++)
                {
                    if (m_Board[i, m_Board.GetLength(1) - 1 - i] != tmp)
                    {
                        isWin = false;
                    }
                }
            }

            return isWin;
        }

        public void UpdateCounterArr(int i_Row, int i_Col)
        {
            m_RowCounterArr[i_Row - 1]++;
            m_ColCounterArr[i_Col - 1]++;
            if (i_Row == i_Col)
            {
                m_SlantCounterArr[0]++;
            }

            if (i_Row + i_Col == m_Board.GetLength(1) + 1)
            {
                m_SlantCounterArr[1]++;
            }
        }

        public void ResetDataGame()
        {
            Array.Clear(m_Board, 0, m_Board.Length);
            m_FullCellsCounter = 0;
            Array.Clear(m_ColCounterArr, 0, m_ColCounterArr.Length);
            Array.Clear(m_RowCounterArr, 0, m_RowCounterArr.Length);
            Array.Clear(m_SlantCounterArr, 0, m_SlantCounterArr.Length);
        }

        public void InitGame(int i_BoardSize, int i_PlayerType)
        {
            bool realPlayer;
            m_Board = new int[i_BoardSize, i_BoardSize];
            m_Player1 = new Player(1, true);

            if (i_PlayerType == 1)
            {
                realPlayer = true;
            }
            else
            {
                realPlayer = false;
            }

            m_Player2 = new Player(2, realPlayer);
            m_RowCounterArr = new int[m_Board.GetLength(0)];
            m_ColCounterArr = new int[m_Board.GetLength(1)];
            m_SlantCounterArr = new int[2];
        }

        public bool CheckIfCellIsEmpty(int i_Row, int i_Col)
        {
            if (m_Board[i_Row - 1, i_Col - 1] == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
