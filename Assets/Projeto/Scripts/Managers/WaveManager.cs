using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public EnemyController[] enemyPrefabs;
    public Transform player;
    public Transform[] spawnPoints;
    public Transform entranceTarget;
    public float spawnInterval = 0.8f;
    public float waveInterval = 2f;
    public int CurrentWave { get; private set; }
    public int AliveEnemies { get; private set; }
    private readonly int[] enemyCounts = { 5, 8, 12 };

    public void Begin() { StartCoroutine(RunWaves()); }
    public void EnemyDefeated() { AliveEnemies--; }

    private IEnumerator RunWaves()
    {
        for (int wave = 0; wave < enemyCounts.Length; wave++)
        {
            CurrentWave = wave + 1;
            GameManager.Instance.ui.UpdateWave(CurrentWave);
            for (int i = 0; i < enemyCounts[wave]; i++)
            {
                Transform spawn = spawnPoints[i % spawnPoints.Length];
                EnemyController enemy = Instantiate(enemyPrefabs[wave], spawn.position, Quaternion.identity);
                enemy.Initialize(player, this, entranceTarget.position);
                AliveEnemies++;
                yield return new WaitForSeconds(spawnInterval);
            }
            while (AliveEnemies > 0) yield return null;
            if (wave < enemyCounts.Length - 1) yield return new WaitForSeconds(waveInterval);
        }
        GameManager.Instance.Finish(true);
    }
}
