using UnityEngine;

public class Point: MonoBehaviour
{
    private EnemyDeath _enemyDeath;
    
    private void OnDisable() => 
        _enemyDeath.Happened -= Remove;

    public void Construct(EnemyDeath enemyDeath)
    {
        _enemyDeath = enemyDeath;
        _enemyDeath.Happened += Remove;
    }

    private void Remove() => 
        gameObject.SetActive(false);
}