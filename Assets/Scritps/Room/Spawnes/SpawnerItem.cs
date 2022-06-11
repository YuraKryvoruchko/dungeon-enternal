using UnityEngine;

public class SpawnerItem : MonoBehaviour
{
    [SerializeField] private GameObject[] _itemPrefabs;

    [SerializeField] private Transform[] _points;

    private void Start()
    {
        CreateItems();
    }

    private void CreateItems()
    {
        for(int i = 0; i < _points.Length; i++)
        {
            int additionalnumber = 1; 

            int indexItem = Random.Range(0, _itemPrefabs.Length);
            if(indexItem == _itemPrefabs.Length)
            {
                indexItem = _itemPrefabs.Length - additionalnumber;
            }

            Instantiate(_itemPrefabs[indexItem], _points[i], false);
        }
    }
}
