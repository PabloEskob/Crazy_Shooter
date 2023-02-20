using InfimaGames.LowPolyShooterPack;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    private SkinnedMeshRenderer _skinnedMeshRenderer;

    private void Awake() => 
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

    public void Disable()
    {
        _inventory.gameObject.SetActive(false);
        _skinnedMeshRenderer.enabled = false;
    }
}
