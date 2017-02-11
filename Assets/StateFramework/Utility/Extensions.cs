using System;
using System.Collections.ObjectModel;

public static class Extensions
{
    /// <summary>
    /// A small extension for the readonly collection so some methods for linq can be used
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="compare"></param>
    /// <returns></returns>
    public static bool Any<T>(this ReadOnlyCollection<T> source, Predicate<T> compare)
    {
        foreach (var item in source)
        {
            if (compare.Invoke(item))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// A small extension for the readonly collection so you can find a specified object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="compare"></param>
    /// <returns></returns>
    public static T Find<T>(this ReadOnlyCollection<T> source, Predicate<T> compare)
    {
        foreach (var item in source)
        {
            if (compare.Invoke(item))
            {
                return item;
            }
        }

        throw new NullReferenceException("The item wasn't in the readonlycollection");
    }
}
