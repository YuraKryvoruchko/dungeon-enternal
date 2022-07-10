using UnityEngine;
using DungeonEternal.ImprovementSystem;

public class ImprovementInstaller : MonoBehaviour
{
    [SerializeField] private ImprovementsInventory _inventoryForImprovemetSO;

    private void OnEnable()
    {
        _inventoryForImprovemetSO.OnRunImprovement += RunImprovemet;
        _inventoryForImprovemetSO.OnReturnImprovement += StopImprovemet;
    }
    private void OnDisable()
    {
        _inventoryForImprovemetSO.OnRunImprovement -= RunImprovemet;
        _inventoryForImprovemetSO.OnReturnImprovement -= StopImprovemet;
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
