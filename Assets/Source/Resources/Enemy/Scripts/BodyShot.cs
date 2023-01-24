using System;
using InfimaGames.LowPolyShooterPack.Legacy;
using UnityEngine;

public class BodyShot : MonoBehaviour, IShot
{
    public event Action<int, Collision> Hitted;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Projectile projectile))
        {
            Debug.Log($"BodyShot Damage - {projectile.Damage}");
            Hitted?.Invoke((int)projectile.Damage, collision);
        }

        if (collision.collider.TryGetComponent(out GrenadeScript grenade))
        {
            grenade.ReduceExplosionTime();
            grenade.RestartCoroutine();
            Hitted?.Invoke(grenade.Damage, collision);
        }
    }
}