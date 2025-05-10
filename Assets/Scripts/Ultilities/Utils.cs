using UnityEngine;

public static class Utils
{
    public static Transform FindChildInTransform(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
                return child;

            Transform result = FindChildInTransform(child, childName);
            if (result != null)
                return result;
        }
        return null;
    }
}