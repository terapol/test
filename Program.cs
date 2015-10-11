using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberSeq
{
    class Program
    {

        static void Main(string[] args)
        {
			Random random = new Random();
            List<int> numberList = new List<int>();

			int listCount;
			Console.Write("Enter random number's count: ");
            listCount = int.Parse(Console.ReadLine());


            for (int i = 0; i < listCount; i++)
            {
				numberList.Add(random.Next(1, 8));
            }

			int index = 0;
			int lastIndex = 0;
			int curSequence = 0;
			int maxSequence = 0;
			bool startBracket = false;
			for (int i = 0; i < numberList.Count - 1; i++)
            {
				if (numberList[i] + 1 == numberList[i + 1])
				{
					if (index == 0)
					{
						index = i;
						Console.ForegroundColor = ConsoleColor.Red;
						Console.Write("[");
						startBracket = true;
					}
					Console.Write("{0}, ", numberList[i]);
					curSequence++;
				}
				else if(startBracket)
				{
					if (curSequence > maxSequence)
					{
						maxSequence = curSequence;
						lastIndex = index;
					}
					Console.Write("{0}]", numberList[i]);
					Console.ForegroundColor = ConsoleColor.White;
					Console.Write(", ");
					curSequence = 0;
					index = 0;
					startBracket = false;
				}
				else if(curSequence == 0)
				{
					Console.Write("{0}, ", numberList[i]);
				}
			}

		if (startBracket)
			{
				Console.Write("{0}].", numberList.Last());
			}
		else
			{
				Console.Write("{0}.", numberList.Last());
			}

			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine();
			Console.WriteLine("Max sequence: {0}, Starting index: {1}", maxSequence + 1, lastIndex);
        }
    }
}
