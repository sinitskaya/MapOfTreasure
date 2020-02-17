using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureIsland
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Введите номер карты: ");
            //string s = Console.ReadLine();
            //string mapFilePath = "../TestData/Map" + s + ".txt";
            string mapFilePath = "../../../../TestData/Map2.txt";
            Map map = new Map();
            map.ReadMapFile(mapFilePath);         
            map.PrintMap();

            Algorithm.ApplyAlgorithm(map.baseRobot.pos[0], map.treasure.pos[0], map);
            Algorithm.PrintWay(map);
            //Position p = new Position(18, 1);
            //map.Neighbors(p);
            Console.ReadKey();
        }
    }
}
