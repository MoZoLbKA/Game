using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Game
{
     public class Monster
     {
        private static string path = "Monsters.txt";
        public Monster()
        {
            _bloodLust = allMonsters[0]._bloodLust;
            _name = allMonsters[0]._name;
            _priceForOpen = allMonsters[0]._priceForOpen;
            _unblock = true;
        }
        static private List<Monster> allMonsters = new List<Monster>
        {
            new Monster("Zombie",1,0),
            new Monster("Demon",0, 100),
            new Monster("Ghost",0.5f,500)
        };
        private const int BloodInTick = 1;
        private float _bloodLust;

        

        public int PriceForOpen
        {
            get { return _priceForOpen; }
            private set { _priceForOpen = value; }
        }

        public float BloodLust
        {
            get { return _bloodLust; }
            private set { _bloodLust = value; }
        }

        private string _name;
        private int _priceForOpen;
        private bool _unblock;

       

        public string  Name
        {
            get { return _name; }
            private set { _name = value; }
        }

        Monster(string name,float bloodLust,int priceForOpen)
        {           
            _bloodLust = bloodLust;
            _name = name;
            _priceForOpen = priceForOpen;
        }
        public static Monster ChooseMonster()
        {
            SaveBuyersMonsters();
            PrintInfoAbotMonsters();
            Console.Write("Выберете номер монстра, за которого хотите играть : ");
            int choose;
            while(!int.TryParse(Console.ReadLine(),out choose)|| choose<1 || choose>allMonsters.Count)
            {
                Console.WriteLine("Неверный ввод,повторите!");               
            }
            if (allMonsters[choose - 1]._unblock)
            {
                return allMonsters[choose - 1];
            }
            else
            {
                if (GamePanel.CheckToBuyMonster(allMonsters[choose - 1]))
                {                  
                    allMonsters[choose - 1]._unblock = true;
                    SaveBuyersMonsters();
                    return allMonsters[choose - 1];
                }
                else return allMonsters[0];
            }
        }
        private static void PrintInfoAbotMonsters()
        {
            UploadBuyersMonsters();
            for (int i = 0; i < allMonsters.Count; i++)
            {
                Console.Write($"{i+1}. {allMonsters[i]._name}");
                if (!allMonsters[i]._unblock)
                {
                    Console.Write($" -ЗАБЛОКИРОВАН, цена покупки : {allMonsters[i]._priceForOpen}");
                }
                Console.WriteLine();
            }
        }
        public static void SaveBuyersMonsters()
        {

            File.Delete(path);
            

            using(StreamWriter writer = new StreamWriter(path))
            {
                
                foreach (var monster in allMonsters)
                {
                    if (monster._name.ToLower().Equals("zombie"))
                    {
                        writer.WriteLine("True");
                    }
                    else
                    {
                        writer.WriteLine($"{monster._unblock}");
                    }
                }

            }
        }
        public static void UploadBuyersMonsters()
        {
                     
            using (StreamReader reader = new StreamReader(path))
            {
                for (int i = 0; i < allMonsters.Count; i++)
                {
                    allMonsters[i]._unblock = bool.Parse(reader.ReadLine());
                }               
            }
        }




    }
}