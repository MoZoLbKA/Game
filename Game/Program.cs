using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            bool flag = true;
            Monster monster = new Monster();
            //DisplayInfo.PrintPurposeGame();          
            Console.WriteLine("Введите enter для продолжения");
            Console.ReadKey();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            while (flag)
            {

                GamePanel.StartGame();
                DisplayInfo.PrintWithDelay($"Вы играете за {monster.Name}");
                DisplayInfo.PrintWithDelay("Выберете действие:");
                Console.WriteLine("1.Играть");
                Console.WriteLine("2.Выбрать персонажа");
                Console.WriteLine("3.Выйти");

                string command = Console.ReadLine().ToLower();
                if(command.Equals("1")||command.Equals("играть")|| command.Equals("1.играть") || (command.Equals("buhfnm")))
                {
                    DisplayInfo.PrintWithDelay($"Вы играете за {monster.Name}");
                    GamePanel.CreateNewGame(monster);
                }
                else if(command.Equals("2") || command.Equals("выбрать") || command.Equals("1.персонаж") || (command.Equals("выбрать персонажа")))
                {
                    Console.WriteLine($"Осталось монет : {GamePanel.Balance}");
                    monster = null;
                    monster=Monster.ChooseMonster();
                    
                }
                else if(command.Equals("выйти") || command.Equals("выход")||command.Equals("3") || command.Equals("exit"))
                {
                    flag = false;
                }

                Console.Clear();

                
            }
        }
    }
}
