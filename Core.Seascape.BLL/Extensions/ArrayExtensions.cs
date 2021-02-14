using System;

namespace Core.Seascape.BLL.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Resizes a 2-dimensional array
        /// </summary>
        /// <returns>A resized copy of the array in it's original type</returns>
        public static T[,] Resize<T>(this T[,] original, int x, int y)
        {
            T[,] newArray = new T[x, y];

            if (original == null) return newArray;

            int minX = Math.Min(original.GetLength(0), newArray.GetLength(0));
            int minY = Math.Min(original.GetLength(1), newArray.GetLength(1));

            for (int i = 0; i < minY; ++i)
            {
                Array.Copy(original, i * original.GetLength(0), newArray, i * newArray.GetLength(0), minX);
            }

            return newArray;
        }
    }
}
