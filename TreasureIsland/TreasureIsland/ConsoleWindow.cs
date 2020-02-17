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

        public void ChangeEmptyScreenshot()
        {
            /*for (int i = 0; i < Columns; i++)
            {
                emptyScreenshot = String.Format("{0}", i);
            }
            for (int i = 0; i < Columns; i++)
                for (int j = 0; j < Rows; j++)
                {
                    emptyScreenshot = emptyScreenshot + String.Format("{0}", i);
                    for (int k = 0; k < Columns - 2; k++)
                    {
                        emptyScreenshot = emptyScreenshot + String.Format("{0}", ".");
                    }
                    emptyScreenshot = emptyScreenshot + String.Format("{0}", "#");
                }*/
        }
        public void SetupConsoleWindow(int x, int y)
        {
            // Статический метод WriteLine системного класса Console выводит строку в консоль
            //Console.WriteLine("Hello world!");

            // У консольного окна есть размер самого окна и размер буфера
            // Их можно установить так:
            if (x > Columns)
            {
                Columns = x;
                ChangeEmptyScreenshot();
            }
            if (y > Rows)
            {
                Rows = y;
                ChangeEmptyScreenshot();
            }
            
            Console.SetWindowSize(Columns, Rows);
            //Console.SetBufferSize(Columns + 1, Rows + 1); // задаём буфер с запасом, чтобы избежать нежелательной прокрутки окна
            // Убираем курсор, если он не нужен
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
            Console.Write(emptyScreenshot);

            // Статические методы класса Math могут пригодится для решения первого задания.
            // Так можно округлить число с плавающей точкой до целого по правилам арифметического округления.
            
        }
    }
}
