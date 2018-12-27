using System;
using System.Collections.Generic;

namespace Shuffler
{
    public static class Shuffler
    {
        public static void BumpShuffle(List<IShuffableItem> shuffableItems, bool inShuffle)
        {
            if (shuffableItems != null && 
                shuffableItems.Count > 0)
            {
                List<IShuffableItem> oddItems = new List<IShuffableItem>();
                List<IShuffableItem> evenItems = new List<IShuffableItem>();
                List<IShuffableItem> shuffledItems = new List<IShuffableItem>();
                int shuffableCount = shuffableItems.Count;
                for (int i = 0; i < shuffableCount; i++)
                {
                    if (i % 2 == 0)
                        evenItems.Add(shuffableItems[i]);
                    else
                        oddItems.Add(shuffableItems[i]);
                }

                for (int i = 0; i < shuffableCount; i++)
                {
                    if (inShuffle)
                    {
                        shuffledItems.Add(oddItems[0]);
                        oddItems.RemoveAt(0);
                    }
                    else
                    {
                        shuffledItems.Add(evenItems[0]);
                        evenItems.RemoveAt(0);
                    }
                    inShuffle = !inShuffle;
                }
                shuffableItems.Clear();
                shuffableItems.AddRange(shuffledItems);
            }
        }

        public static void RiffleShuffle(List<IShuffableItem> shuffableItems,
            int maxShuffableItemDropVariance = 1, int minShuffableItemSplitVariance = 0, int maxShuffableItemSplitVariance = 0)
        {
            Random rnd = new Random();
            int shuffableCount = shuffableItems.Count;
            int splittingVariance = 50;
            if (minShuffableItemSplitVariance > maxShuffableItemSplitVariance)
            {
                splittingVariance = rnd.Next(minShuffableItemSplitVariance, maxShuffableItemSplitVariance);
            }
            else
            {
                splittingVariance = rnd.Next(maxShuffableItemSplitVariance, minShuffableItemSplitVariance);
            }
            double splittingPercent = rnd.Next(50 - splittingVariance, 50 + splittingVariance);
            splittingPercent = splittingPercent / 100;
            int shuffableItemsTopSide = (int)Math.Floor(shuffableCount * splittingPercent);
            
            List<IShuffableItem> topHalf = new List<IShuffableItem>();
            List<IShuffableItem> botHalf = new List<IShuffableItem>();
            for (int i = 0; i < shuffableItemsTopSide; i++)
            {
                topHalf.Add(shuffableItems[0]);
                shuffableItems.RemoveAt(0);
            }
            botHalf.AddRange(shuffableItems);
            shuffableItems.Clear();
            int cardDropVariance = rnd.Next(1, maxShuffableItemDropVariance);
            int cardDropRunner = 0;
            bool topHalfDrop = false;
            for (int i = 0; i < shuffableCount; i++)
            {
                if (cardDropRunner < cardDropVariance)
                {
                    if (topHalfDrop)
                    {
                        shuffableItems.Add(topHalf[0]);
                        topHalf.RemoveAt(0);
                    }
                    else
                    {
                        shuffableItems.Add(botHalf[0]);
                        botHalf.RemoveAt(0);
                    }
                    cardDropRunner++;
                }
                else
                {
                    cardDropVariance = rnd.Next(1, maxShuffableItemDropVariance);
                    cardDropRunner = 0;
                    topHalfDrop = !topHalfDrop;
                    i--;
                }
            }
        }

        public static void Cut(List<IShuffableItem> shuffableItems, 
            int minSplitVariance = 0, int maxSplitVariance = 0)
        {
            Random rnd = new Random();
            int shuffableCount = shuffableItems.Count;
            int minCardToSplit = (50 - minSplitVariance > 0 && 50 - minSplitVariance <= 100) ? 50 - minSplitVariance : 0;
            int maxCardToSplit = (50 + maxSplitVariance >= 100 && 50 + maxSplitVariance > 0) ? 50 + maxSplitVariance : 100;
            double splittingPercent = rnd.Next(minCardToSplit, maxCardToSplit);
            splittingPercent = splittingPercent / 100;
            int shuffableItemsTopSide = (int)Math.Floor(shuffableCount * splittingPercent);
            List<IShuffableItem> topHalf = new List<IShuffableItem>();
            List<IShuffableItem> botHalf = new List<IShuffableItem>();
            for (int i = 0; i < shuffableItemsTopSide; i++)
            {
                topHalf.Add(shuffableItems[0]);
                shuffableItems.RemoveAt(0);
            }
            botHalf.AddRange(shuffableItems);
            shuffableItems.Clear();
            foreach (var item in botHalf)
            {
                shuffableItems.Add(item);
            }
            foreach (var item in topHalf)
            {
                shuffableItems.Add(item);
            }
        }

        public static void ComputerRandomizer(List<IShuffableItem> shuffableItems)
        {
            Random rnd = new Random();
            List<IShuffableItem> holdingCell = new List<IShuffableItem>();
            List<int> positionFound = new List<int>();
            for (int i = 0; i < shuffableItems.Count; i++)
            {
                int positionToAdd = rnd.Next(shuffableItems.Count);
                bool isAddable = true;
                foreach (int pos in positionFound)
                {
                    if (positionFound.Contains(positionToAdd))
                    {
                        isAddable = false;
                        i--;
                        break;
                    }
                }
                if (isAddable)
                {
                    positionFound.Add(positionToAdd); 
                }
            }
            foreach (int pos in positionFound)
            {
                holdingCell.Add(shuffableItems[pos]);
            }
            shuffableItems.Clear();
            shuffableItems.AddRange(holdingCell);
        }
    }
}
