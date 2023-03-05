using UnityEngine;

namespace Source.Infrastructure
{
    public interface IAssets : IService
    {
        GameObject InstantiatePlayer(string path, Vector3 initPoint);
        GameObject InstantiatePlayer(string path);
    }
}