using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoutineGenerator.ENTITY;
using RoutineGenerator.DAL;

namespace Test
{
    class TestMain
    {

        static void Main(string[] args)
        {
            string x = "temp-oga";
            string[] temp = x.Split('-');
            Console.WriteLine(temp[0] + " next " +temp[1]);
        }
    }
}