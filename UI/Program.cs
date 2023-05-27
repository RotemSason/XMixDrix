using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class Program
    {
        public static void Main()
        {
            Menu menu = new Menu();
            menu.ShowMenu();
            menu.Board.StartGame();
        }
    }
}
