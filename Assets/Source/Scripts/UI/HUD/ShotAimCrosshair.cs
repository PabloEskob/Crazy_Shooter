using InfimaGames.LowPolyShooterPack.Interface;
using UnityEngine;

public class ShotAimCrosshair : MonoBehaviour
{
    [SerializeField] private Crosshair _crosshair;

    public void Shot() =>
        _crosshair.TurnOnAnimation();
}