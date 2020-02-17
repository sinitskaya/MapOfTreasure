using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureIsland
{
    class Algorithm
    {
        //путь
        static List<Position> closed = new List<Position>();
        //(from)
        //static List<Position> cameFrom = new List<Position>(); //путь из начала в текущую позицию
        //cameFrom.Add(start); // путь из начала в начало
        public static int GetHeuristicEval(Position a, Position b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }
        public static List<Position> ApplyAlgorithm(Position start, Position goal, Map map)
        {
            int FindCostInPosition(List<Tuple<Position, int>> t, Position p)
            {
                Predicate<Tuple<Position, int>> isExists = delegate (Tuple<Position, int> t1) { return t1.Equals(t); };
                for (int i=0; i<t.Count; i++)
                {
                    if (t.Exists(isExists) == true)
                        return t[i].Item2;
                }
                return 0;
            }
            bool NextIsNotInClosed(List<Position> listPositions, Position pos) //next не в списке посещенных
            {
                for (int i = 0; i < listPositions.Count; i++)
                {
                    if (listPositions[i] == pos) // => next есть в списке посещенных
                        return false;
                }
                return true;
            }
            //(open)
            PriorityQueue<Position> priorityQueuePositions = new PriorityQueue<Position>();
            priorityQueuePositions.AddElem(start, 0); //добавили элемент (начальную позицию и приоритет) в приоритетную очередь (open)

            //(from)
            //List<Position> cameFrom = new List<Position>(); //путь из начала в текущую позицию
            //cameFrom.Add(start); // путь из начала в начало

            //(closed)
            ///List<Position> closed = new List<Position>();

            //(pos, start -> current)
            List<Tuple<Position, int>> costStartToCurrent = new List<Tuple<Position, int>>(); // пара текущая и стоимость
            //Tuple.Create(start, 0); //стоимость движения из начальной точки в текущую
            costStartToCurrent.Add(Tuple.Create(start, 0)); //стоимость движения из начальной точки в текущую)

            Position current = new Position(0, 0);
            while (priorityQueuePositions.GetCount() > 0) //пока не пусто
            {
                current = priorityQueuePositions.FindMinAndDeleteElem(); //удалить из приоритетной очереди с 
                closed.Add(current); //пометить как посещенную

                if (current == goal)
                {
                    break;
                }

                foreach (Position next in map.Neighbors(current))
                {
                    int newCost = FindCostInPosition(costStartToCurrent, current) + 1;//graph.Cost(current, next);
                    if (NextIsNotInClosed(closed, next) == true )//&& newCost <= FindCostInPosition(costStartToCurrent, next))
                    {
                        costStartToCurrent.Add(Tuple.Create(next, newCost));
                        //costStartToCurrent[next] = newCost;
                        int priority = newCost + GetHeuristicEval(next, goal); 
                        priorityQueuePositions.AddElem(next, priority); //добавить в открытую
                        //cameFrom.Add(current);
                    }
                }
            }
            return closed;
        }
        static public void PrintWay(Map map)
        {
            /*for (int i = 0; i < closed.Count; i++)
            {
                Console.WriteLine();
                Console.Write(closed[i].X);
                Console.Write(" ");
                Console.Write(closed[i].Y);
                Console.WriteLine();

            }*/
            for (int k = 0; k < closed.Count; k++)
            {
                Console.SetCursorPosition(closed[k].X, closed[k].Y);
                Console.Write("&");

            }
            //Print Treasure
            Console.SetCursorPosition(map.treasure.pos[0].X, map.treasure.pos[0].Y);
            Console.Write("+");
            //Print bridge
            Console.SetCursorPosition(map.bridge.pos[0].X, map.bridge.pos[0].Y);
            Console.Write("#");
        }
    }
}
