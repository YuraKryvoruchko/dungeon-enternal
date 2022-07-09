using UnityEngine;
using DungeonEternal.ImprovementSystem;

public class ImprovementInstaller : MonoBehaviour
{
    [SerializeField] private WeaponImprovemetnSO _weaponImprovemetnSO;

    private void OnEnable()
    {
        _weaponImprovemetnSO.OnRunImprovement += RunImprovemet;
        _weaponImprovemetnSO.OnReturnImprovement += StopImprovemet;
    }
    private void OnDisable()
    {
        _weaponImprovemetnSO.OnRunImprovement -= RunImprovemet;
        _weaponImprovemetnSO.OnReturnImprovement -= StopImprovemet;
    }

    private void RunImprovemet(ImprovementSO improvement)
    {
        improvement.RunImprovement(gameObject);
    }
    private void StopImprovemet(ImprovementSO improvement)
    {
        improvement.StopImprovement(gameObject);
    }
}

public enum ImprovementType
{
    Weapon,
    Player,
    Enemy,
    Item
}
