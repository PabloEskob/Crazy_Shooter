using UnityEngine;

public class UniqueId : MonoBehaviour
{
    [SerializeField] private string _id;

    public string Id
    {
        get => _id;
        set => _id = value;
    }
}