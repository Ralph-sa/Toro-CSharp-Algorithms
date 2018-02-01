using System;
using System.Diagnostics;

using DataStructures.Lists;
namespace DataStructuresTest
{
    public static class SkipListTest
    {
        public static void DoTest()
        {
            var skipList = new SkipList<int>();
            skipList.Add(12);
            skipList.Add(13);
            skipList.Add(15);
            skipList.Add(16);
            skipList.Add(17);
            var enumarator = skipList.GetEnumerator();
            Console.WriteLine(" [*] Skip-List elements:");
            Console.WriteLine("..... ");
            while (enumarator.MoveNext())
            {
                Console.WriteLine(enumarator.Current + "--->");
                //   Console.WriteLine("{0} ->", enumarator.Current);
            }
        }
    }
}