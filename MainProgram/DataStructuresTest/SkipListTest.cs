using System;
using System.Diagnostics;

using DataStructures.Lists;

using Ln.SkipList;
namespace DataStructuresTest
{
    public static class SkipListTest
    {
        public static void DoTest()
        {
            // var skipList = new SkipList<int>();
            // skipList.Add(12);
            // skipList.Add(13);
            // skipList.Add(15);
            // skipList.Add(16);
            // skipList.Add(17);
            // var enumarator = skipList.GetEnumerator();
            // Console.WriteLine(" [*] Skip-List elements:");
            // Console.WriteLine("..... ");
            // while (enumarator.MoveNext())
            // {
            //     Console.WriteLine(enumarator.Current + "--->");
            //     //   Console.WriteLine("{0} ->", enumarator.Current);
            // }
            Random ran = new Random();
            var skipList = new SkipList<int>();
            int j = 0;
            for (int i = 0; i <= 5; i++)
            {
                j = ran.Next(10, 100);
                skipList.Add(j);
                Console.WriteLine(j + "   " + skipList.Contains(j));
            }
            var enumarator = skipList.GetEnumerator();
            while (enumarator.MoveNext())
            {
                Console.WriteLine(enumarator.Current + "--->");
                //   Console.WriteLine("{0} ->", enumarator.Current);
            }
            Console.ReadLine();

        }

        public static void DoTest2()
        {
            Random ran = new Random();
            SkipList skipList = new SkipList();
            skipList.Init();
            int j;
            for (int i = 0; i < 5; i++)
            {
                j = ran.Next(10, 100);
                skipList.Insert(j);
                Console.WriteLine(j + "   " + skipList.Search(j));
            }
            Console.WriteLine(skipList.Display());
            Console.ReadLine();
        }
        public static void DoTest3()
        {
            SkipList3.SkipList<int, int> list = new SkipList3.SkipList<int, int>();
            Random ran = new Random();
            int j = 0;
            for (int i = 0; i < 8; i++)
            {
                j = ran.Next(10, 100);
                list.Add(i, j);
                // Console.WriteLine(j + "   " + list[i]);
            }


        }
    }
}