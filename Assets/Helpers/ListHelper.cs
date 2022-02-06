using System.Collections.Generic;
using UnityEngine;

namespace TWHelpers
{
    public static class ListHelper
    {
        public static T GetRandomElement<T>(this List<T> list)
        {
            return list.Count==0 ? default(T) : list.ToArray()[Random.Range(0, list.Count - 1)];
        }

        public static T GetRandomElement<T>(this T[] list)
        {
            return list.Length == 0 ? default(T) : list[Random.Range(0, list.Length - 1)];
        }
    }
}