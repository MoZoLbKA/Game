using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Game
{

    public static class DisplayInfo
    {
        private const int DelayInput = 50;

        public static void PrintPurposeGame()
        {
            string info = "Цель игры успевать писать всплывающие сообщения за определенное время";
            PrintWithDelay(info);
            info = "Вы можете играть за монстра и пытаться убить этих несчастных людишек!";
            PrintWithDelay(info);
            info = "Убивайте людей и не умрите от голода, копите золото, чтобы открыть новых монстров!";
            PrintWithDelay(info);

        }
        public static void PrintWithDelay(string str,int DelayInMilliseconds=DelayInput)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var ch in str)
            {
                
                Console.Write(ch);
                Thread.Sleep(DelayInMilliseconds);
            }
            Thread.Sleep(DelayInMilliseconds * 4);
            Console.WriteLine();
        }
        
        
    }
}
