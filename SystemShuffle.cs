using System.Collections;
using System.Collections.Generic;
using System;

namespace SystemShuffle
{
    public class List 
    {
        public static void shuffle<T>(List<T> data)
        {
            for(int i = 0; i<data.Count; i++)
            {
                Random ran = new Random();
                int randomValue = ran.Next(0, data.Count);
                T temp = data[i];
                data[i] = data[randomValue];
                data[randomValue] = temp;
            }
        }
    }
    public class Array
    {
        public static void shuffle<T>(T[] data)
        {
            for(int i=0; i<data.Length; i++)
            {
                Random ran = new Random();
                int randomValue = ran.Next(0, data.Length);
                T temp = data[i];
                data[i] = data[randomValue];
                data[randomValue] = temp;
            }
        }
    }
}


