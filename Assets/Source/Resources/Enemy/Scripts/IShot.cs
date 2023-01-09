using System;
using UnityEngine;

public interface IShot
{
    event Action<int, Collision> Hitted;
    void OnCollisionEnter(Collision collision);
}