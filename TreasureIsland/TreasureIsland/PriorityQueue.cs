using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureIsland
{
    public class PriorityQueue<Positiion>
    {
        private List<Tuple<Position, int>> elements = new List<Tuple<Position, int>>();

        public int GetCount()
        {
            return elements.Count;
        }

        public void AddElem(Position pos, int priority)
        {
            elements.Add(Tuple.Create(pos, priority));
        }

        public Position FindMinAndDeleteElem()
        {
            int bestPriority = 0;

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Item2 < elements[bestPriority].Item2) //приоритеты
                {
                    bestPriority = i;
                }
            }

            Position bestPosition = elements[bestPriority].Item1;
            elements.RemoveAt(bestPriority);
            return bestPosition;
        }

    }
}