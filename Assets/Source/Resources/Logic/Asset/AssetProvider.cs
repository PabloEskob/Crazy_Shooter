using Source.Infrastructure;
using UnityEngine;

public class AssetProvider : IAssets
{
    public GameObject Instantiate(string path)
    {
        var prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab);
    }

    public  GameObject Instantiate(string path, Transform at)
    {
        var prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab, at.position, at.rotation);
    }

    public GameObject InstantiatePlayer(string path, Vector3 initPoint)
    {
        var prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab, initPoint, Quaternion.identity);
    }

    public GameObject InstantiatePlayer(string path)
    {
        var prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab);
    }
}