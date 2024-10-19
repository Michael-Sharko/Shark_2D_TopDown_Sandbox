using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shark.Utilities
{
    public static class CommonUtils
    {
        public static void LoadComponent<T>(GameObject gameObject, out T instance) where T : Component
        {
            instance = LoadComponent<T>(gameObject);
        }

        public static T LoadComponent<T>(GameObject gameObject) where T : Component
        {
            if (!gameObject.TryGetComponent(out T instance))
            {
                instance = gameObject.AddComponent<T>();
            }
            return instance;
        }
    }
}