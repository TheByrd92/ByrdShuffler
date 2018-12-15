using System;
using System.Collections.Generic;

namespace Shuffler
{
    public class Shuffler
    {
        private List<object> shuffableItems;

        public Shuffler(IEnumerable<object> shuffableItems)
        {
            this.shuffableItems = (List<object>)shuffableItems;
        }

        public void PerfectRiffleShuffle()
        {
            if (shuffableItems.Count > 0)
            {
                List<object> oddItems = new List<object>();
                List<object> evenItems = new List<object>();
                List<object> shuffledItems = new List<object>();
                int shuffableCount = shuffledItems.Count;
                for (int i = 0; i < shuffableCount; i++)
                {
                    if (i % 2 == 0)
                        evenItems.Add(shuffledItems[i]);
                    else
                        oddItems.Add(shuffledItems[i]);
                }

                int firstDrop = new Random().Next(0, 1);
                bool isDropOdd = false;
                if (firstDrop % 2 == 0)
                {
                    shuffledItems.Add(evenItems[0]);
                }
                else
                {
                    shuffledItems.Add(oddItems[0]);
                    isDropOdd = true;
                }

                for (int i = 1; i < shuffableCount / 2; i++)
                {
                    if (isDropOdd)
                        shuffledItems.Add(oddItems[i]);
                    else
                        shuffledItems.Add(evenItems[i]);
                }
                shuffableItems.Clear();
                shuffableItems.AddRange(shuffledItems);
            }
        }

        public void HumanRiffleShuffle()
        {

        }
    }
}
