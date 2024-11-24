using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    static class GetNumber
    {
        static public int Int()
        {
            int num;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out num))
                {
                    return num;
                }
                else
                {
                    Console.WriteLine("Введено некорректное значение, попробуйте снова");
                }
            }


        }
    }
}
