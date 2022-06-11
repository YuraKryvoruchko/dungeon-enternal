using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DungeonEternal.AI;

namespace DungeonEternal.AI.DataCollector
{
    public class DataCollector : MonoBehaviour
    {
        [SerializeField] private float _timeToAddData;
        [SerializeField] private float _timeToPrintData;

        [SerializeField] private bool _isContinue = false;

        private Dictionary<IData, List<Vector3>> _listIDataObject = new Dictionary<IData, List<Vector3>>();

        private void Start()
        {
            StartCoroutine(AddDataPosition());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StopAllCoroutines();

                StartCoroutine(PrintDataPosition());
            }
        }

        private IEnumerator AddDataPosition()
        {
            while (true)
            {
                IData[] enemys = FindObjectsOfType<Enemy>().OfType<IData>().ToArray();

                foreach (IData enemy in enemys)
                {
                    if (!_listIDataObject.ContainsKey(enemy))
                    {
                        List<Vector3> positionToTime = new List<Vector3>();

                        Vector3 positionObject = enemy.SendData();

                        positionToTime.Add(positionObject);

                        _listIDataObject.Add(enemy, positionToTime);
                    }
                    else
                    {
                        if (_listIDataObject.TryGetValue(enemy, out List<Vector3> vectors))
                        {
                            Vector3 positionObject = enemy.SendData();

                            vectors.Add(positionObject);
                        }
                    }
                }

                yield return new WaitForSeconds(_timeToAddData);
            }
        }
        private IEnumerator PrintDataPosition()
        {
            Enemy[] enemys1 = FindObjectsOfType<Enemy>();
            foreach (var enemy in enemys1)
            {
                enemy.enabled = false;
            }

            while (true)
            {
                IData[] enemys = FindObjectsOfType<Enemy>().OfType<IData>().ToArray();

                _isContinue = false;

                foreach (IData enemy in enemys)
                {
                    if (_listIDataObject.TryGetValue(enemy, out List<Vector3> vectors))
                    {
                        if (vectors.Count == 0)
                        {
                            continue;
                        }

                        _isContinue = true;

                        enemy.PrintData(vectors.Last());

                        vectors.Remove(vectors.Last());
                    }
                }

                if (_isContinue)
                {
                    yield return new WaitForSeconds(_timeToPrintData);
                }
                else
                {
                    StartCoroutine(AddDataPosition());

                    foreach (var enemy in enemys1)
                    {
                        enemy.enabled = true;
                    }

                    break;
                }
            }
        }

        private void EnableMove(GameObject[] disconnectableObjects, bool isActive)
        {
            foreach (var disconnectableObject in disconnectableObjects)
                disconnectableObject.active = isActive;
        }
    }
}