using System.Collections;
using UnityEngine;

public sealed class Coroutines : MonoBehaviour
{
    private static Coroutines _instance;

    private static Coroutines InstanceCoroutine
    {
        get
        {
            if (_instance != null) return _instance;
            var gameObject = new GameObject();
            _instance = gameObject.AddComponent<Coroutines>();

            return _instance;
        }
    }

    public static Coroutine StartRoutine(IEnumerator ieEnumerator) =>
        InstanceCoroutine.StartCoroutine(ieEnumerator);

    public static void StopRoutine(Coroutine coroutine)
    {
        if (coroutine!=null) 
            InstanceCoroutine.StopCoroutine(coroutine);
    }
}