using System;
using UnityEngine;
using DungeonEternal.Inventories;
using DungeonEternal.Weapons;
using DungeonEternal.MiniMap;
using DungeonEternal.UI;
using DungeonEternal.TrayderImprovement;

namespace DungeonEternal.Player
{
    [RequireComponent(typeof(Inventory))]
    public class HandsPlayer : MonoBehaviour
    {
        [SerializeField] private Transform _bulletPoint;

        [SerializeField] private Transform _handsTransform;

        [SerializeField] private bool _isAutomaticReload = false;
        [SerializeField] private bool _isBlockPlayerOnStart = true;
        private PlayerInput _input;
    
        private Inventory _inventory;

        private Weapon _weaponInHand;

        private int _indexWeapon = -1;

        private float _mouseScrollWheel;

        private bool _blockHands = false;

        public static event Action<int, int> UpdateBulletText;
        public Transform HandsTransform { get => _handsTransform; }
        public Inventory Inventory { get => _inventory; }

        private void Awake()
        {
            _input = new PlayerInput();

            _input.PlayerShoot.Shoot.performed += context => Shoot();
            _input.PlayerShoot.Reload.performed += context => Reload();

            _inventory = GetComponent<Inventory>();

            if (_inventory.WaypoinsInInventory.Count != 0)
            {
                _weaponInHand = _inventory.WaypoinsInInventory[0];

                if(_isAutomaticReload == true && _weaponInHand.TryGetComponent(out IReload oldWeapon))
                    oldWeapon.WeaponEmpty += Reload;
            }
        }

        private void OnEnable()
        {
            _input.Enable();

            BigMiniMap.OnEnableBigMap += BlockTheHands;
            BigMiniMap.OnDisableBigMap += UnlockHands;
            InstructionMenu.OnShowInstructionMenu += BlockTheHands;
            InstructionMenu.OnCloseInstructionMenu += UnlockHands;
            Trader.OnStartTrade += BlockTheHands;
            Trader.OnEndTrade += UnlockHands;
        }
        private void OnDisable()
        {
            _input.Disable();

            BigMiniMap.OnEnableBigMap -= BlockTheHands;
            BigMiniMap.OnDisableBigMap -= UnlockHands;
            InstructionMenu.OnShowInstructionMenu -= BlockTheHands;
            InstructionMenu.OnCloseInstructionMenu -= UnlockHands;
            Trader.OnStartTrade -= BlockTheHands;
            Trader.OnEndTrade -= UnlockHands;
        }

        private void Start()
        {
            EnableWeaponInHands();
            UpdateUIStats();

            if (_isBlockPlayerOnStart == true)
                BlockTheHands();
        }

        private void Update()
        {
            if(_blockHands == false)
                ChoiceOfWeapons();
        }

        public void PickUpAWeapon(Weapon weapon)
        {
            _inventory.AddWeaponToInventory(weapon);

            UpdateUIStats();
        }

        private void Shoot()
        {
            if (_blockHands == false)
                _weaponInHand.Attack();
        }
        private void Reload()
        {
            if (_blockHands == false)
            {
                if (_weaponInHand.TryGetComponent(out IFirearms weapon) && weapon.WeaponForState == WeaponState.Free)
                    weapon.Reload(_inventory.GetBullets(weapon.UsedTypeOfBullets, weapon.GetBullets()));
            }
        }
        private void ChoiceOfWeapons()
        {
            _mouseScrollWheel = Input.GetAxisRaw("Mouse ScrollWheel");

            if (_mouseScrollWheel != 0)
            {
                if (Inventory.WaypoinsInInventory.Count > 1)
                {
                    if (_mouseScrollWheel > 0)
                    {
                        if (_indexWeapon + 1 <= Inventory.WaypoinsInInventory.Count - 1)
                        {
                            _indexWeapon += 1;

                            EnableAndDisableWaypon(_indexWeapon);
                        }
                    }
                    else if (_mouseScrollWheel < 0)
                    {
                        if (_indexWeapon - 1 >= 0)
                        {
                            _indexWeapon -= 1;

                            EnableAndDisableWaypon(_indexWeapon);
                        }
                    }

                    UpdateUIStats();
                }
            }
        }
        private void EnableAndDisableWaypon(int indexWeapon)
        {
            if (_weaponInHand != null)
            {
                DisableWeaponInHands();

                _weaponInHand = Inventory.WaypoinsInInventory[indexWeapon];

                EnableWeaponInHands();
            }
            else
            {
                _weaponInHand = Inventory.WaypoinsInInventory[indexWeapon];

                EnableWeaponInHands();
            }
        }
        private void EnableWeaponInHands()
        {
            _weaponInHand.gameObject.SetActive(true);

            if (_weaponInHand.TryGetComponent(out IReload newWeapon))
            {
                if(_isAutomaticReload == true)
                    newWeapon.WeaponEmpty += Reload;

                newWeapon.OnReload += UpdateUIStats;
                newWeapon.ReturnBullets += AddBullets;
            }

            _weaponInHand.OnAttack += UpdateUIStats;
        }
        private void DisableWeaponInHands()
        {
            _weaponInHand.gameObject.SetActive(false);

            if (_weaponInHand.TryGetComponent(out IReload oldWeapon))
            {
                if(_isAutomaticReload == true)
                    oldWeapon.WeaponEmpty -= Reload;

                oldWeapon.OnReload -= UpdateUIStats;
                oldWeapon.ReturnBullets -= AddBullets;
            }

            _weaponInHand.OnAttack -= UpdateUIStats;
        }
        private void UpdateUIStats()
        {
            if(_weaponInHand.TryGetComponent(out IFirearms weapon))
            {
                UpdateBulletText?.Invoke(weapon.Stor—apacity,
                    _inventory.GetBulletCount(weapon.UsedTypeOfBullets));
            }
        }
        private void AddBullets(int countBullets)
        {
            if (_weaponInHand.TryGetComponent(out IFirearms weapon))
                _inventory.AddBullets(weapon.UsedTypeOfBullets, weapon.GetBullets());
        }
        private void BlockTheHands()
        {
            _blockHands = true;
        }
        private void UnlockHands()
        {
            _blockHands = false;
        }
    }
}
