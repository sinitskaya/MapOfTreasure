using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureIsland
{
    class ConsoleWindow
    {
        private string emptyScreenshot =
@"0123456789012345678901234567890123456789
1......................................#
2......................................#
3......................................#
4......................................#
5......................................#
6......................................#
7......................................#
8......................................#
9......................................#
0......................................#
1......................................#
2......................................#
3......................................#
4......................................#
5......................................#
6......................................#
7......................................#
8......................................#
9#######################################";
        public int Rows { get; set; } = 20;
        public int Columns { get; set; } = 40;

        public void ChangeAndPrintEmptyScreenshot() //заменить 
        {
            for (int i = 0; i <= Columns ; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write(i%10);
            }
            for (int j = 0; j <= Rows; j++)
            {
                Console.SetCursorPosition(0, j);
                Console.Write(j%10);
            }
            for (int i = 1; i < Columns; i++)
                for (int j = 1; j < Rows; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(".");
                }
            for (int i = 1; i <= Columns; i++)
            {
                Console.SetCursorPosition(i, Rows);
                Console.Write("#");
            }
            for (int j = 1; j <= Rows; j++)
            {
                Console.SetCursorPosition(Columns, j);
                Console.Write("#");
            }
        }
        public void SetupConsoleWindow(int x, int y)
        {
            Console.SetWindowSize(Columns, Rows);
            Console.CursorVisible = false;

            // Мы можем поменять цвет фона и символов
            Console.ForegroundColor = System.ConsoleColor.DarkGray;
            Console.BackgroundColor = System.ConsoleColor.White;
            // Установленные цвета влияют на то, как будут выводится символы в консоль
            // после установки цветов, но не меняют её текущий вид

            // Метод .Clear очищает консоль и заполняет её фон выбранным цветом.
            Console.Clear();

            // Мы можем выводить строку на экран, без перевода курсора на следующую строку методом Write
            Console.SetCursorPosition(0, 0); // Так можно задать позицию курсора - с этого места начнётся
                                             // вывод на консоль следующей командой .Write**
            if (x > Columns || y > Rows)
            {
                Columns = x;
                Rows = y;
                Console.SetWindowSize(Columns, Rows);
                Console.SetBufferSize(Columns + 2, Rows + 2);
                ChangeAndPrintEmptyScreenshot();
            }
            else
            {
                Console.Write(emptyScreenshot);
            }
        }
    }
}
