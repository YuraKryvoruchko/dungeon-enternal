using UnityEngine;
using DungeonEternal.AI;

public class PanelWin : MonoBehaviour
{
    [SerializeField] private GameObject _panelWin;

    private void OnEnable()
    {
        Boss.Death += CheckingWin;
    }
    private void OnDisable()
    {
        Boss.Death -= CheckingWin;
    }

    private void CheckingWin(Boss boss)
    {
        if (boss.GetComponent<Boss>())
            Win();
    }

    private void Win()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _panelWin.SetActive(true);
    }
}
