using Shuffler;
using System.Collections.Generic;

namespace ShufflerTest
{
    public class Program
    {
        public class MyStuff : IShuffableItem
        {
            public int Id = 0;
            public string Name = "";

            public override string ToString()
            {
                return "Id: " + Id + " Name: " + Name;
            }
        }

        static void Main(string[] args)
        {
            List<IShuffableItem> AllStuff = new List<IShuffableItem>();
            for (int i = 0; i < 20; i++)
            {
                AllStuff.Add(new MyStuff() { Id = i + 1, Name = "♦" + i });
                System.Console.WriteLine(AllStuff[i].ToString());
            }

            for (int i = 0; i < 7; i++)
            {
                Shuffler.Shuffler.ComputerRandomizer(AllStuff);
            }

            foreach (var item in AllStuff)
            {
                System.Console.WriteLine(item.ToString());
            }
        }
    }
}
