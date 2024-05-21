using System.Collections;
using UnityEngine;

namespace OctanGames.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}