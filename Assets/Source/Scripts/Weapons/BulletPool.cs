using InfimaGames.LowPolyShooterPack.Legacy;
using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private int _capacity = 20;
        [SerializeField] private bool _isAutoExpand = true;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _container;

        private PoolBehaviour<Projectile> _pool;

        private void Awake()
        {
            _pool = new PoolBehaviour<Projectile>(_projectilePrefab, _capacity, _container);
            _pool.IsAutoExpand = _isAutoExpand;
        }

        public Projectile CreateProjectile() => 
            _pool.GetFreeElement();
    }
}