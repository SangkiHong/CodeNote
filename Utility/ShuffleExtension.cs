using System.Collections.Generic;
using UnityEngine;

public static class ShuffleExtension
{
    public static void Shuffle<T>(this T[] array, int shuffleAccuracy = -1)
    {
        if (shuffleAccuracy == -1)
            shuffleAccuracy = array.Length;

        for (int i = 0; i < shuffleAccuracy; i++)
        {
            int randomIndex = Random.Range(1, array.Length);

            T temp = array[randomIndex];
            array[randomIndex] = array[0];
            array[0] = temp;
        }
    }


    public static void Shuffle<T>(this List<T> list, int shuffleAccuracy = -1)
    {
        if (shuffleAccuracy == -1)
            shuffleAccuracy = list.Count;

        for (int i = 0; i < shuffleAccuracy; i++)
        {
            int randomIndex = Random.Range(1, list.Count);

            T temp = list[randomIndex];
            list[randomIndex] = list[0];
            list[0] = temp;
        }
    }
}
