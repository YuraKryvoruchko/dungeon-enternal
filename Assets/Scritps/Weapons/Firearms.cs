using System.Collections;
using System;
using UnityEngine;

using Random = UnityEngine.Random;

namespace DungeonEternal.Weapons
{
    public interface IFirearms : IReload
    {
        public int GetBullets();
        public BulletType UsedTypeOfBullets { get; }
        public int Stor�apacity { get; }
        public int BulletsConversionRate { get; }
        public WeaponState WeaponForState { get; }
    }
    public interface IReload
    {
        public event Action OnReload;
        public event Action<int> ReturnBullets;

        public abstract event Action WeaponEmpty;

        public void Reload(int bulletInventroy);
    }
    public enum WeaponState
    {
        Free,
        OnShoot,
        OnReload
    }

    [RequireComponent(typeof(AudioSource), typeof(Animator), typeof(BulletEjector))]
    public abstract class Firearms : Weapon, IFirearms
    {
        [Header("Base properties")]
        [SerializeField] private int _stor�apacity;
        [SerializeField] private int _maxCountStorBullets;
        [SerializeField] private int _bulletsNumberInOneShoot = 1;
        [SerializeField] private int _intbulletsEjectionInOneShot = 1;
        [SerializeField] private int _bulletsConversionRate;

        [SerializeField] private float _reloadTime = 3f;
        [SerializeField] private float _timetimeBetweenShots = 0.1f;

        [SerializeField] private float _xSpread = 0;
        [SerializeField] private float _ySpread = 0;

        [Header("Audio properties")]
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private AudioClip _audioReload;
        [SerializeField] private AudioClip _audioShoot;

        [Header("Animator properties")]
        [SerializeField] private Animator _animator;

        [SerializeField] private string _nameReloadParameters = "Reload";
        [SerializeField] private string _nameShootParameters = "Shoot";

        [Header("Other properties")]
        [SerializeField] private Transform _bulletPoint;

        [SerializeField] private Bullet _bulletPrefab;

        [SerializeField] private BulletType _usedTypeOfBullets;

        [SerializeField] private BulletEjector _bulletEjector;

        private int _numberOfBulletsFromInventory = 0;
        [SerializeField] private WeaponState _weaponState;

        private bool _canShoot = true;

        public event Action OnReload;
        public event Action<int> ReturnBullets;

        public abstract override event Action OnAttack;
        public abstract event Action WeaponEmpty;

        public int Stor�apacity { get => _stor�apacity; protected set => _stor�apacity = value; }

        public int BulletsConversionRate { get => _bulletsConversionRate; }

        public BulletType UsedTypeOfBullets { get => _usedTypeOfBullets; }

        public WeaponState WeaponForState { get => _weaponState; protected set => _weaponState = value; }

        protected int BulletsNumberInOneShoot { get => _bulletsNumberInOneShoot; }
        protected int IntbulletsEjectionInOneShot { get => _intbulletsEjectionInOneShot; }

        protected float ReloadTime { get => _reloadTime; }
        protected float TimetimeBetweenShots { get => _timetimeBetweenShots; }

        protected float XSpread { get => _xSpread; }
        protected float YSpread { get => _ySpread; }

        protected AudioSource AudioSource { get => _audioSource; set => _audioSource = value; }

        protected AudioClip AudioReload { get => _audioReload; }
        protected AudioClip AudioShoot { get => _audioShoot; }

        protected Animator Animator { get => _animator; }
        protected string NameReloadParameters { get => _nameReloadParameters; }
        protected string NameShootParameters { get => _nameShootParameters; }

        protected Transform BulletPoint { get => _bulletPoint; }

        protected Bullet BulletPrefab { get => _bulletPrefab; }

        protected BulletEjector BulletEjector { get => _bulletEjector; }
        
        protected bool CanShoot { get => _canShoot; set => _canShoot = value; }

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
            _bulletEjector = GetComponent<BulletEjector>();
        }

        private void OnEnable()
        {
            _weaponState = WeaponState.Free;

            _canShoot = true;
        }
        private void OnDisable()
        {
            if (_weaponState == WeaponState.OnReload)
                ReturnBullets?.Invoke(_numberOfBulletsFromInventory);

            _canShoot = false;
        }

        public override abstract void Attack();
        public override void Attack(Transform target) { }

        public int GetBullets()
        {
            return _maxCountStorBullets - Stor�apacity;
        }
        public virtual void Reload(int bulletInventroy)
        {
            if(_weaponState == WeaponState.Free && _weaponState != WeaponState.OnReload)
            {
                _numberOfBulletsFromInventory = bulletInventroy;

                StartCoroutine(CheckingForTheTypeOfBullet(bulletInventroy));
            }
        }

        protected Vector3 CalculateDirection(float xSpread, float ySpread)
        {
            Vector3 starPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);

            starPosition.x += Random.Range(-xSpread, xSpread);
            starPosition.y += Random.Range(-ySpread, ySpread);

            Ray ray = Camera.main.ScreenPointToRay(starPosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000, 1, QueryTriggerInteraction.Ignore))
            {
                Vector3 direction = hit.point - BulletPoint.position;

                Debug.DrawRay(BulletPoint.position, direction, Color.red, 1000);

                return direction.normalized;
            }
            else
            {
                Debug.LogWarning("Ray did not collide with any object");

                return ray.direction.normalized;
            }
        }
        protected IEnumerator ToDelayOnFiringootTymer()
        {
            CanShoot = false;

            yield return new WaitForSeconds(TimetimeBetweenShots);

            CanShoot = true;
        }

        private IEnumerator CheckingForTheTypeOfBullet(int typeBullets)
        {
            if (typeBullets > 0 && Stor�apacity != _maxCountStorBullets)
            {
                WeaponForState = WeaponState.OnReload;

                AudioSource.PlayOneShot(_audioReload);

                _animator.SetTrigger(NameReloadParameters);

                yield return new WaitForSeconds(_reloadTime);

                int bullets = _maxCountStorBullets - Stor�apacity;

                if (typeBullets < bullets)
                {
                    bullets = typeBullets;
                }

                typeBullets -= bullets;

                Stor�apacity += bullets;

                WeaponForState = WeaponState.Free;

                Debug.Log("Reload " + _usedTypeOfBullets);

                OnReload?.Invoke();
            }
        }
    }
}