using System.Collections;
using UnityEngine;

namespace Source.Scripts
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}