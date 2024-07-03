// Есть лабиринт описанный в виде двумерного массива где 1 это стены,
// 0 - проход, 2 - искомая цель.
// Пример лабиринта:
// 1 1 1 1 1 1 1
// 1 0 0 0 0 0 1
// 1 0 1 1 1 0 1
// 0 0 0 0 1 0 2
// 1 1 0 0 1 1 1
// 1 1 1 1 1 1 1
// 1 1 1 1 1 1 1
// Напишите алгоритм определяющий наличие выхода из лабиринта и
// выводящий на экран координаты точки выхода если таковые имеются.

// ----------  ДОМАШНЕЕ ЗАДАНИЕ  -------------
// Доработайте приложение поиска пути в лабиринте,
// но на этот раз вам нужно определить сколько всего выходов
// имеется в лабиринте.
// Сигнатура метода: 
// static int HasExit(int startI, int startJ, int[,] l)
namespace Labirint
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] l = new int[,]
            {
                {1, 1, 1, 2, 1, 1, 1 },
                {1, 0, 0, 0, 0, 0, 1 },
                {1, 0, 1, 1, 1, 0, 1 },
                {2, 0, 0, 0, 1, 0, 2 },
                {1, 1, 0, 2, 1, 1, 1 },
                {1, 1, 1, 0, 1, 1, 1 },
                {1, 1, 1, 2, 1, 1, 1 }
            };

            Stack<Tuple<int, int>> _path = new Stack<Tuple<int, int>>();

            Console.WriteLine("Для поиска выходов из лабиринта, введите координаты начального размещения исследователя.");
            int startI = inputInt32("Введите координату Х = ");
            int startJ = inputInt32("Введите координату Y = ");

            Console.WriteLine("\nНаходим все выходы из лабиринта.\n");
//            Console.WriteLine($"Ответ получен за {FindAllExits(x, y, out exits, out goodEntry)} итераций.");
            Console.WriteLine($"В лабиринте найдено {HasExit(startI, startJ, l)} выходов.");

            //int FindAllExits(int i, int j, out int _exits, out bool _goodEntry)
            int HasExit(int i, int j, int[,] lbrnt)
            {
                int _exits = 0;
                if (i < 0 || j < 0 || i > lbrnt.GetLength(0) || j > lbrnt.GetLength(1))
                    throw new Exception("Вы телепортировались мимо лабиринта :(");
                if (lbrnt[i, j] == 2)
                {
                    PrintAddressOfExit(i, j, ref _exits);
                    _path.Push(Tuple.Create(i, j)); // Ибо преподаватель на лекции указал, что выход может быть и в середине
                                                    // лабиринта, в том числе в узком проходе.
                }
                else if (lbrnt[i, j] == 0)
                {
                    _path.Push(Tuple.Create(i, j));
                }
                else // if (lbrnt[i, j] == 1) // Считаем, что стены - всё что не 0 или 2
                {
                    Console.WriteLine($"Вы телепортировались прямо в стену: {i},{j}.\n" +
                        $"Не повезло... Попробуйте снова!");
                    return 0;
                }

                while (_path.Count > 0)
                {
                    var current = _path.Pop();

                    int _x = current.Item1;
                    int _y = current.Item2;
                    lbrnt[_x, _y] = 1;

                    if (_x + 1 < lbrnt.GetLength(0))       // вниз
                    {
                        if (lbrnt[_x + 1, _y] == 0)          // проход
                            _path.Push(Tuple.Create(_x + 1, _y));
                        if (lbrnt[_x + 1, _y] == 2)          // выход
                        {
                            _path.Push(Tuple.Create(_x + 1, _y));
                            PrintAddressOfExit(_x + 1, _y, ref _exits);
                        }
                    }
                    if (_y + 1 < lbrnt.GetLength(1))       // вправо
                    {
                        if (lbrnt[_x, _y + 1] == 0)
                            _path.Push(Tuple.Create(_x, _y + 1));
                        if (lbrnt[_x, _y + 1] == 2)
                        {
                            _path.Push(Tuple.Create(_x, _y + 1));
                            PrintAddressOfExit(_x, _y + 1, ref _exits);
                        }
                    }
                    if (_x - 1 >= 0)                             // вверх
                    {
                        if (lbrnt[_x - 1, _y] == 0)
                            _path.Push(Tuple.Create(_x - 1, _y));
                        if (lbrnt[_x - 1, _y] == 2)
                        {
                            _path.Push(Tuple.Create(_x - 1, _y));
                            PrintAddressOfExit(_x - 1, _y, ref _exits);
                        }
                    }
                    if (_y - 1 >= 0)                             // влево
                    {
                        if (lbrnt[_x, _y - 1] == 0)
                            _path.Push(Tuple.Create(_x, _y - 1));
                        if (lbrnt[_x, _y - 1] == 2)
                        {
                            _path.Push(Tuple.Create(_x, _y - 1));
                            PrintAddressOfExit(_x, _y - 1, ref _exits);
                        }
                    }
                }
                return _exits;
            }
        }
        private static int inputInt32(string msg)
        {
            Console.Write(msg);
            return Convert.ToInt32(Console.ReadLine());
        }
        private static void PrintAddressOfExit(int x, int y, ref int numberOfExit)
        {
            Console.WriteLine($"Найден выход №{++numberOfExit}: {x},{y}");
        }
    }
}
