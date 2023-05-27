using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Player
    {
        private int m_PlayerId;
        private int m_WinNumber;
        private bool m_RealPlayer;

        public Player(int i_PlayerId, bool i_RealPlayer)
        {
            m_PlayerId = i_PlayerId;
            m_RealPlayer = i_RealPlayer;
        }

        public int Id
        {
            get
            {
                return m_PlayerId;
            }

            set
            {
                m_PlayerId = value;
            }
        }

        public int WinNumber
        {
            get
            {
                return m_WinNumber;
            }

            set
            {
                m_WinNumber = value;
            }
        }

        public bool RealPlayer
        {
            get
            {
                return m_RealPlayer;
            }

            set
            {
                m_RealPlayer = value;
            }
        }
    }
}
