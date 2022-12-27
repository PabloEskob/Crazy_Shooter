using System;
using InfimaGames.LowPolyShooterPack.Legacy;
using UnityEngine;

public class HeadShot : MonoBehaviour, IShot
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private Collider _collider;
    public event Action<int, Collision> Hitted;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Projectile>())
        {
            if (_skinnedMeshRenderer != null)
            {
                _skinnedMeshRenderer.enabled = false;
                _collider.enabled = false;
            }

            Hitted?.Invoke(1, collision);
        }
    }
}