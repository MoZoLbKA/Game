using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace Game
{
    public static class GamePanel
    {
        private const int BloodInTick = 1;
        private const int GlobalTicks = 10;
        private static int balance=CheckBalance();
        
        public static int Balance
        {
            get { return balance; }
            private set { balance = value; }
        }

        private static int startIndexABC = 1072;
        private static int endIndexABC = 1103;
        private static string CreateNewGameString(int lengthStr)
        {
            Random r = new Random();

            string result = "";
            for (int i = 0; i < lengthStr; i++)
            {
                if (r.Next(7) == 1 && i != 0 && i!=lengthStr-1)
                {
                    result += " ";
                }
                else
                {
                    result += (char)r.Next(startIndexABC, endIndexABC);
                    if (i == 0)
                    {
                        result = result.ToUpper();
                    }
                }
            }
            return result;
        }
        public static void CreateNewGame(Monster monster)
        {
            int bonusForAnswer = 1;
            int healths;
            if (monster.BloodLust == 0)
            {
                healths = 1;
                bonusForAnswer = 5;
            }
            else 
            { 
                healths = (int)(GlobalTicks / monster.BloodLust); 
            }                                          
            int startLength = 5;
            int startSecondsForAnswer = 5;
            bool isRightAnswer = true;
            int summOfCoins = 0;
            

            while (isRightAnswer|| healths>0)
            {
                
                Random random = new Random();
                ///встреча с ведьмаком
                int chanceWitcher = random.Next(100);
                if (chanceWitcher < 10)
                {
                    int randomAddedSymbols = random.Next(1, 5);
                    DisplayInfo.PrintWithDelay("Вы попались на ведьмака!ОН ХОЧЕТ ВАС УБИТЬ!");
                    int randomChanceToKill = random.Next(1000);
                    if (randomChanceToKill <5)
                    {
                        DisplayInfo.PrintWithDelay("Вас перерубил ведьмак своим мечом!");
                        DisplayInfo.PrintWithDelay("Вы проиграли");
                        healths = 0;
                        isRightAnswer = false;
                        continue;
                        
                    }
                    DisplayInfo.PrintWithDelay("Вы сумели сбежать от него,но он добавил вам символов");

                }
                ///встреча с мальчиком
                int chanceGift = random.Next(100);
                if (chanceGift < 10 && startLength>5)
                {                   
                    DisplayInfo.PrintWithDelay("Вы встретили маленького мальчика,он дарит вам конфету");
                    int randomAddedCoins= random.Next(2, 4);
                    summOfCoins += randomAddedCoins;
                    startLength-=2;                  
                    DisplayInfo.PrintWithDelay($"Добавлено {randomAddedCoins} монеты, следующий набор символов уменьшен на 2");
                }



                ///Генерация строки и подсчет времени на ответ
                string newStr = CreateNewGameString(startLength);
                DisplayInfo.PrintWithDelay("Успейте ввести следующий набор букв:");
                Thread.Sleep(200);
                Stopwatch timer = new Stopwatch();
                Console.WriteLine(newStr);
                timer.Start();
                string resultFromPlayer = Console.ReadLine();
                timer.Stop();
                //проверка ответа введеного пользователем
                if (timer.Elapsed.TotalSeconds > startSecondsForAnswer || !resultFromPlayer.Equals(newStr))
                {
                    isRightAnswer = false;
                    healths--;
                    if (healths == 0)
                    {
                        DisplayInfo.PrintWithDelay("Вы проиграли!");
                        DisplayInfo.PrintWithDelay($"Монет заработано {summOfCoins}");
                    }
                    Console.WriteLine("Вы не успели ответить или неправильно ввели символы!");
                    DisplayInfo.PrintWithDelay($"Жизней осталось : {healths}");                                   
                    bonusForAnswer = 0;
                    Thread.Sleep(200);
                }
                else
                {
                    startLength++;
                    isRightAnswer = true;
                    summOfCoins += bonusForAnswer;
                    bonusForAnswer++;
                    
                }
                timer = null;
                
                Console.Clear();
            }
            balance += summOfCoins;
            SaveBalance();

        }
        private  static void SaveBalance()
        {
            int _balance = balance;
            string strBalance = balance.ToString();
            string path = "Game_Save.txt";
            
            if (File.Exists(path)) 
            {
                
                File.WriteAllText(path, balance.ToString());

            }

        }
        private static int CheckBalance()
        {
            string path = "Game_Save.txt";
            int balance;
            if (File.Exists(path))
            {
                if (int.TryParse(File.ReadAllText(path), out balance))
                {
                    return DecryptFileBalance(balance); 
                }
                else
                {
                    throw new Exception("Ошибка чтения баланса");
                }
            }
            else
            {
                throw new Exception("Ошибка чтения баланса");
            }
            
        }
        private static int DecryptFileBalance(int balance)
        {
            
            int currentBalance=balance;          
            return currentBalance;
        }
        public static void StartGame()
        {
            Monster.UploadBuyersMonsters();
        }
        public static bool CheckToBuyMonster(Monster monster)
        {
            if (monster.PriceForOpen <= Balance)
            {
                Console.WriteLine("Вы уверены,что хотите купить этого персонажа?ДА/НЕТ");
                string answer = Console.ReadLine().ToLower();
                if (answer.Equals("да"))
                {
                    Balance -= monster.PriceForOpen;
                    Monster.SaveBuyersMonsters();                
                    SaveBalance();
                    return true;
                }             
            }
            else
            {
                DisplayInfo.PrintWithDelay("Недостаточно монет!");
                Thread.Sleep(100);
            }
            return false;
        }
        




    }
}