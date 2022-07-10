using System;
using UnityEngine;
using DungeonEternal.Health;

namespace DungeonEternal.Player
{
    public class HealthPlayer : MonoBehaviour, IHealth
    {
        [SerializeField] private float _health;
        [Space]
        [SerializeField] private PartOfTheBody[] _partsOfTheBody;

        private readonly float MAKS_HEALTH = 100;

        public static event Action<float> MyHitPointsWasTakenAway;
        public static event Action OnDead;

        public float MaksHealth { get => MAKS_HEALTH; }
        public float Health { get => _health; }

        private void OnEnable()
        {
            for (int i = 0; i < _partsOfTheBody.Length; i++)
                _partsOfTheBody[i].OnTakeDamage += TakeDamage;
        }
        private void OnDisable()
        {
            for (int i = 0; i < _partsOfTheBody.Length; i++)
                _partsOfTheBody[i].OnTakeDamage -= TakeDamage;
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;

            float healthCount = _health / MAKS_HEALTH;

            MyHitPointsWasTakenAway?.Invoke(healthCount);

            if (_health > MAKS_HEALTH)
                _health = MAKS_HEALTH;
            else if (_health <= 0)
                Dead();
        }
        private void Dead()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            OnDead?.Invoke();

            Destroy(gameObject);
        }
    }
}
