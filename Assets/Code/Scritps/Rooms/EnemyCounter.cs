using System;
using DungeonEternal.AI;

namespace DungeonEternal.Rooms
{
    public class EnemyCounter
    {
        private int _enemyCount;

        private const int ZERO_COUNT = 0;

        public event Action AllEnemiesDead;

        public void StartCountEnemy(Enemy[] enemies)
        {
            for (int i = 0; i < enemies.Length; i++)
                enemies[i].OnDead += CountEnemy;

            _enemyCount = enemies.Length;
        }

        private void CountEnemy()
        {
            _enemyCount -= 1;

            if (_enemyCount == ZERO_COUNT)
                AllEnemiesDead?.Invoke();
        }
    }
}
