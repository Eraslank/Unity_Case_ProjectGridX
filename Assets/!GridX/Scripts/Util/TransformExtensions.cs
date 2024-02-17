using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    private static IEnumerable<GameObject> GetNextChild(Transform transform)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            yield return transform.GetChild(i).gameObject;
        }
    }

    public static void DestroyAllChildren(this Transform transform, bool immediate = false)
    {
        foreach(var c in GetNextChild(transform))
        {
            if (immediate)
                Object.DestroyImmediate(c);
            else
                Object.Destroy(c);
        }
    }
    public static void DestroyAllChildrenExcept(this Transform transform, Transform except, bool immediate = false)
    {
        foreach (var c in GetNextChild(transform))
            if (c.transform != except)
            {
                if (immediate)
                    Object.DestroyImmediate(c);
                else
                    Object.Destroy(c);
            }
    }
    public static void DestroyAllChildren(this GameObject gameObject, bool immediate = false)
    {
        gameObject.transform.DestroyAllChildren(immediate);
    }

    public static RectTransform GetRectTransform(this Component c)
    {
        return c.GetComponent<RectTransform>();
    }
    public static RectTransform GetRectTransform(this GameObject gameObject)
    {
        return gameObject.GetComponent<RectTransform>();
    }

    public static T GetComponentAndAddIfNotExist<T>(this Component c) where T : Component
    {
        T component = c.GetComponent<T>();
        if (component)
            return component;
        return c.gameObject.AddComponent<T>();
    }
    public static T GetComponentAndAddIfNotExist<T>(this GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (component)
            return component;
        return gameObject.AddComponent<T>();
    }
}
