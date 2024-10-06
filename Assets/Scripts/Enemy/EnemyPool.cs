using CosmicCuration.Enemy;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private EnemyView enemyView;
    private EnemyData enemyData;
    private List<PooledEnemy> pooledEnemies;

    public EnemyPool(EnemyView enemyPrefab, EnemyData enemyData)
    {
        this.enemyView = enemyPrefab;
        this.enemyData = enemyData;
    }

    public EnemyController GetEnemy()
    {
        if (pooledEnemies.Count > 0)
        {
            PooledEnemy enemy = pooledEnemies.Find(enemy => !enemy.isUsed);
            if (enemy != null)
            {
                enemy.isUsed = true;
                return enemy.Enemy;
            }
        }

        return CreateNewPooledEnemy();
    }

    public EnemyController CreateNewPooledEnemy()
    {
        PooledEnemy newEnemy = new PooledEnemy();
        newEnemy.Enemy = CreateEnemy();
        newEnemy.isUsed = true;
        pooledEnemies.Add(newEnemy);
        return newEnemy.Enemy;
    }

    private EnemyController CreateEnemy() => new EnemyController(enemyView, enemyData);

    public void ReturnEnemy(EnemyController enemy)
    {
        PooledEnemy pooledEnemy = pooledEnemies.Find(enemy => enemy.Equals(enemy));
        pooledEnemy.isUsed = false;
    }
    public class PooledEnemy
    {
        public EnemyController Enemy;
        public bool isUsed;
    }
}
