using System;

namespace DungeonEternal.Weapons
{
    public abstract class СoldSteel : Weapon
    {
        private StateСoldSteel _stateСoldSteel;

        protected StateСoldSteel StateSteel { get => _stateСoldSteel; set => _stateСoldSteel = value; }

        public abstract override event Action OnAttack;

        private void OnEnable()
        {
            _stateСoldSteel = StateСoldSteel.None;
        }
        private void OnDisable()
        {
            _stateСoldSteel = StateСoldSteel.None;
        }

        public abstract override void Attack();

        protected enum StateСoldSteel
        {
            None,
            Attack
        }
    }
}
