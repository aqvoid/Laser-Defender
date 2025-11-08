using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScoreSO", menuName = "Enemy Score Config")]
public class EnemyScoreSO : ScriptableObject
{
    [System.Serializable]
    public class EnemyScorePair
    {
        public GameObject enemyPrefab;
        public int score;
    }

    public List<EnemyScorePair> enemyScores = new List<EnemyScorePair>();

    public int GetScoreByEnemy(GameObject enemy)
    {
        foreach (EnemyScorePair pair in enemyScores)
        {
            if (enemy.name.Contains(pair.enemyPrefab.name))
                return pair.score;
        }
        return 0;
    }
}
