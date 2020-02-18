using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace TreasureIsland
{
    public class Position
    { //позиция обьекта
        public int X;
        public int Y;
        /*override public int CompareTo(object p)
        {
            return 1;
        }*/
        public Position()
        {
            X = 0;
            Y = 0;
        }
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
        public static bool  operator == (Position pos1, Position pos2)
        {
            if (pos1.X == pos2.X && pos1.Y == pos2.Y)
                return true;
            else
                return false;
        }
        public static bool operator != (Position pos1, Position pos2)
        {
            return (pos1 == pos2);
        }
    }
    public class ObjectOnMap //обьекты на карте
    {
        public List<Position> pos;
        public ObjectOnMap()
        {
            pos = new List<Position>();
        }
        virtual public void PrintName()
        {
            Console.Write("ObjectOnMap");
        }
    }
    public class Base : ObjectOnMap //база
    {
        public Base() : base() { }
        override public void PrintName()
        {
            Console.Write("Base");
        }
    }
    public class Treasure : ObjectOnMap //сокровище
    {
        public Treasure() : base() { }
        override public void PrintName()
        {
            Console.Write("Treasure");
        }
    }
    class Water : ObjectOnMap //вода
    {
        public struct Fraction
        {
            public int dx;
            public int dy;
        }
        public List<Fraction> angles;
        public Water() : base()
        {
            angles = new List<Fraction>();
        }
        override public void PrintName()
        {
            Console.Write("Water");
        }
        public void SetAngles(List<Position> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                Fraction f = new Fraction();
                f.dx = list[i].X - list[i + 1].X;
                f.dy = list[i].Y - list[i + 1].Y;
                angles.Add(f);
            }
        }
    }
    public class Bridge : ObjectOnMap //мост
    {
        public Bridge() : base() { }
        override public void PrintName()
        {
            Console.Write("Bridge");
        }
    }
    public class Map //карта сокровищ
    {
        public Base baseRobot;
        Water water;
        public Bridge bridge;
        public Treasure treasure;

        public Map()
        {
            baseRobot = new Base();
            water = new Water();
            bridge = new Bridge();
            treasure = new Treasure();
        }

        public void ReadMapFile(string filePath) //Читать карту из файла
        {
            string[] DeleteSymbolInString(string[] substrings) //Удалить ненужные символы в строке
            {
                //Берем подстроку с цифрами
                substrings[1] = substrings[1].Replace(" ", "");
                substrings[1] = substrings[1].Trim(')');

                Char[] ch = { ':', '>', '-' };

                string[] substrs = substrings[1].Split(ch, StringSplitOptions.RemoveEmptyEntries);
                return substrs;
            }
            // возвращает массив строк
            string[] allLines = File.ReadAllLines(filePath);

            foreach (var line in allLines)
            {
                if (line.IndexOf("//") == 0 || line.Length == 0)
                {
                    continue;
                }
                else
                {
                    char delimiter = '(';
                    //Разделяем слова от цифр от скобок и пробелов
                    string[] substrings = line.Split(delimiter);
                    string[] substrs = DeleteSymbolInString(substrings);
                    ObjectOnMap obj;
                    if (substrings[0].IndexOf("BASE") == 0)
                    {
                        obj = baseRobot;
                        SetPositionObject(substrs, obj);
                    }
                    else if (substrings[0].IndexOf("Treasure") == 0)
                    {
                        obj = treasure;
                        SetPositionObject(substrs, obj);
                    }
                    else if (substrings[0].IndexOf("WATER") == 0)
                    {
                        obj = water;
                        List<Position> list = new List<Position>();
                        SetPositionForWater(substrs, list);
                        water.SetAngles(list);
                        AddElem(list);
                    }
                    else if (substrings[0].IndexOf("bridge") == 0)
                    {
                        obj = bridge;
                        SetPositionObject(substrs, obj);
                    }
                }
            }
        }

        public void SetPositionForWater(string [] substrings, List<Position> list)
        {
            for (int j = 0; j < (substrings.Length); j++) //по подстрокам пары 34,1
            {
                Position p = new Position();
                string[] substrs1 = substrings[j].Split(',');
                try
                {
                    p.X = int.Parse(substrs1[0]);
                    p.Y = int.Parse(substrs1[1]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(" Uncorrect input data: letters in numbers");
                    throw;
                }
                list.Add(p);
            }
            //PrintInputData(obj);
        }
        public void SetPositionObject(string[] substrings, ObjectOnMap obj) //Установить позиции для обьекта
        {
            for (int j = 0; j < (substrings.Length); j++) //по подстрокам пары 34,1
            {
                Position p = new Position();
                string[] substrs1 = substrings[j].Split(',');
                try
                {
                    p.X = int.Parse(substrs1[0]);
                    p.Y = int.Parse(substrs1[1]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(" Uncorrect input data: letters in numbers");
                    throw;
                }
                obj.pos.Add(p);
            }
            //PrintInputData(obj);
        }
        public void PrintInputData(ObjectOnMap obj) //Вывести входные данные
        {
            obj.PrintName(); Console.Write(" ");
            foreach (var o in obj.pos)
            {
                Console.Write("X: "); Console.Write(o.X); Console.Write(" ");
                Console.Write("Y: "); Console.Write(o.Y); Console.Write(" ");
            }
            Console.WriteLine();
        }
        public void AddElem(List<Position> list)
        {
            //ConsoleWindow cw = new ConsoleWindow();
            //cw.SetupConsoleWindow();
            try
            {
                //Print Water
                bool CheckConditionForNormalX(int i, int fX, int iEnd)
                {
                    if (fX == -1)
                    {
                        return (i >= iEnd);
                    }
                    else
                    {
                        return (i <= iEnd);
                    }
                }
                bool CheckConditionForNormalY(int j, int fY, int jEnd)
                {
                    if (fY == -1)
                    {
                        return (j >= jEnd);
                    }
                    else
                    {
                        return (j <= jEnd);
                    }
                }
                int toCountY(int x, double k, double b)
                {
                    return (int)Math.Round(k * x + b, 0);
                }
                int toCountX(int j, double coeff, double b)
                {
                    return (int)Math.Round((j - b) / coeff, 0);
                }
                for (int k = 0; k < list.Count - 1; k+=1)
                {
                    int jStr = list[k].Y;
                    int jEnd = list[k + 1].Y;
                    int iStr = list[k].X;
                    int iEnd = list[k + 1].X;
                    int fX = 1;
                    if (list[k].X - list[k + 1].X > 0)
                    {
                        fX = -1;
                    }
                    int fY = 1;
                    if (list[k].Y - list[k + 1].Y > 0)
                    {
                        fY = -1;
                    }
                    if (water.angles[k].dx == 0 || water.angles[k].dy == 0) //перпендикуляр
                    {
                        for (int i = iStr; CheckConditionForNormalX(i, fX, iEnd); i += fX)
                            for (int j = jStr; CheckConditionForNormalY(j, fY, jEnd); j += fY)
                            {
                                //Console.SetCursorPosition(i, j);
                                //Console.Write("~");
                                Position p = new Position(i, j);
                                Predicate<Position> isExists = delegate (Position posPredic) { return posPredic.X == i && posPredic.Y == j; };
                                if (water.pos.Exists(isExists))
                                {
                                    continue;
                                }
                                else
                                    water.pos.Add(p);
                                //obj.pos.Insert(k + 1, p);
                            }
                    }
                    else if (Math.Abs(water.angles[k].dy) >= Math.Abs(water.angles[k].dx))// вправо вниз
                    {
                        int f_str = list[k].Y;
                        double coeff = (double)water.angles[k].dy / water.angles[k].dx;
                        double b = f_str - coeff * list[k].X;
                        for (int j = list[k].Y; CheckConditionForNormalY(j, fY, list[k + 1].Y); j += fY)
                        {
                            int i = toCountX(j, coeff, b);
                            //Console.SetCursorPosition(i, j);
                            //Console.Write("~");
                            Position p = new Position(i, j);
                            Predicate<Position> isExists = delegate (Position posPredic) { return posPredic.X == i && posPredic.Y == j; };
                            if (water.pos.Exists(isExists))
                            {
                                continue;
                            }
                            else
                                water.pos.Add(p);
                        }
                    }
                    else if (Math.Abs(water.angles[k].dy) < Math.Abs(water.angles[k].dx))
                    {
                        int f_str = list[k].Y;
                        double coeff = (double)water.angles[k].dy / water.angles[k].dx;
                        double b = f_str - coeff * list[k].X;
                        for (int i = list[k].X; CheckConditionForNormalX(i, fX, list[k + 1].X); i += fX)
                        {
                            int j = toCountY(i, coeff, b);
                            //Console.SetCursorPosition(i, j);
                            //Console.Write("~");
                            Position p = new Position(i, j);
                            Predicate<Position> isExists = delegate (Position posPredic) { return posPredic.X == i && posPredic.Y == j; };
                            if (water.pos.Exists(isExists))
                            {
                                continue;
                            }
                            else
                                water.pos.Add(p);
                        }
                    }
                }
                //Print мост
                //Console.SetCursorPosition(bridge.pos[0].X, bridge.pos[0].Y);
                //Console.Write("#");
            }
            catch (Exception e)
            {
                Console.WriteLine(" Uncorrect input data: x or y not in (20, 40)");
                throw;
            }
            /*for (int i = 0; i < water.pos.Count; i++)
            {
                Console.Write(water.pos[i].X); Console.Write(" ");
                Console.WriteLine(water.pos[i].Y);
            }*/
        }

        public void PrintMap()
        {
            void MaxPosition(List<Position> l, ref int tempX, ref int tempY) //максимальный х,y из списка позиций
            {
                for (int i = 0; i < l.Count - 1; i++)
                {
                    tempX = l[i].X;
                    if (l[i + 1].X > tempX)
                    {
                        tempX = l[i + 1].X;
                    }
                }
                //int tempY = 0;
                for (int i = 0; i < l.Count - 1; i++)
                {
                    tempY = l[i].Y;
                    if (l[i + 1].Y > tempY)
                    {
                        tempY = l[i + 1].Y;
                    }
                }
            }

            int waterXMax = 0, waterYMax = 0;
            MaxPosition(water.pos, ref waterXMax, ref waterYMax);
            int x = Math.Max(treasure.pos[0].X, waterXMax);
            int y = Math.Max(treasure.pos[0].Y, waterYMax);

            ConsoleWindow cw = new ConsoleWindow();
            cw.SetupConsoleWindow(x, y);

            //Print Treasure
            Console.SetCursorPosition(treasure.pos[0].X, treasure.pos[0].Y);
            Console.Write("+");
            //Print Base of Robot
            for (int j = baseRobot.pos[0].X; j <= baseRobot.pos[1].X; j++)
                for (int i = baseRobot.pos[0].Y; i <= baseRobot.pos[1].Y; i++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.Write("@");
                }
            //Print Water
            for (int k = 0; k < water.pos.Count; k++)
            {
                Console.SetCursorPosition(water.pos[k].X, water.pos[k].Y);
                Console.Write("~");
                    
            }
            //Print bridge
            Console.SetCursorPosition(bridge.pos[0].X, bridge.pos[0].Y);
            Console.Write("#");
            //for (int j = 0; j < cw.Rows; j++)
            //    for (int i=0; i<cw.Columns; i++)
            //    {
            //        Console.WriteLine(j);
            //        Console.WriteLine(".");
            //    }

        }
        public List<Position> Neighbors(Position pos)
        {
            /*bool F(int i, int j)
            {
                Predicate<Position> iJIsWater11 = delegate (Position posPredic)
                {
                    return posPredic.X + 1 == i && posPredic.Y == j;
                };
                Predicate<Position> iJIsWater12 = delegate (Position posPredic)
                { 
                    return posPredic.X == i && posPredic.Y - 1== j; 
                };
                Predicate<Position> iJIsWater21 = delegate (Position posPredic)
                {
                    return posPredic.X - 1 == i && posPredic.Y == j; 
                };
                Predicate<Position> iJIsWater22 = delegate (Position posPredic)
                {
                    return posPredic.X == i && posPredic.Y -1 == j;
                };
                Predicate<Position> iJIsWater31 = delegate (Position posPredic)
                {
                    return posPredic.X == i && posPredic.Y == j +1;
                };
                Predicate<Position> iJIsWater32 = delegate (Position posPredic)
                {
                    return posPredic.X -1 == i && posPredic.Y == j;
                };
                Predicate<Position> iJIsWater41 = delegate (Position posPredic)
                {
                    return posPredic.X == i - 1 && posPredic.Y == j;
                };
                Predicate<Position> iJIsWater42 = delegate (Position posPredic)
                {
                    return posPredic.X == i && posPredic.Y == j - 1;
                };

                if (water.pos.Exists(iJIsWater11) && water.pos.Exists(iJIsWater12))
                    return false;
                else if (water.pos.Exists(iJIsWater21) && water.pos.Exists(iJIsWater22))
                    return false;
                else if (water.pos.Exists(iJIsWater31) && water.pos.Exists(iJIsWater32))
                    return false;
                else if (water.pos.Exists(iJIsWater41) && water.pos.Exists(iJIsWater42))
                    return false;
                return true;

            }*/
            int iiter = pos.X - 1;
            List<Position> neighbors = new List<Position>();
            for (int i = pos.X - 1; i <= pos.X + 1; i++)
            {
                for (int j = pos.Y - 1; j <= pos.Y + 1; j++)
                {
                    Predicate<Position> iJIsWater = delegate (Position posPredic) { return posPredic.X == i && posPredic.Y == j; };
                    Predicate<Position> iJIsBridge = delegate (Position posPredic) { return !(posPredic.X == i && posPredic.Y == j); };
                    if ((i == pos.X && j == pos.Y || i < 0 || j < 0 || water.pos.Exists(iJIsWater)) && bridge.pos.Exists(iJIsBridge)) //&F(i,j)
                        continue;
                    Position p = new Position(i, j);
                    neighbors.Add(p);
                    //var c = String.Format("{0}  {1}", p.X, p.Y);
                    //Console.WriteLine(c);

                }
            }
            return neighbors;
        }
    }
}
