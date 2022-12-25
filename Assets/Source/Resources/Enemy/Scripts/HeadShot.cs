using System;
using InfimaGames.LowPolyShooterPack.Legacy;
using UnityEngine;

public class HeadShot : MonoBehaviour, IShot
{
    public event Action<int, Collision> Hitted;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Projectile>())
        {
            Hitted?.Invoke(1, collision);
        }
    }
}