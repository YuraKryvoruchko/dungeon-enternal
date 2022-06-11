using System;
using UnityEngine;
using DungeonEternal.Player;

using Random = UnityEngine.Random;

namespace DungeonEternal.TrayderImprovement
{
    public class Trader : MonoBehaviour
    {
        [SerializeField] private TraderCell[] _improvementsList;

        [SerializeField] private GameObject _traderMenu;
        [SerializeField] private GameObject _container;

        private Purse _purse;

        public static event Action OnStartTrade;
        public static event Action OnEndTrade;

        private void Start()
        {
            CreateImprovements();
        }

        public void StartTrade(Purse playerPurse)
        {
            _purse = playerPurse;

            _traderMenu.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

            OnStartTrade?.Invoke();
        }
        public void EndTrade()
        {
            _traderMenu.SetActive(false);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            OnEndTrade?.Invoke();
        }

        private void BuyCell(TraderCell cell)
        {
            if(_purse.Coins >= cell.Price)
            {
                _purse.Coins -= cell.Price;

                cell.Buy();
            }
        }
        private void CreateImprovements()
        {
            int countImprovement = Random.Range(0, _improvementsList.Length);

            if (countImprovement == _improvementsList.Length)
                countImprovement = _improvementsList.Length - 1;

            int[] createImprovements = new int[countImprovement];

            for (int i = 0; i < countImprovement; i++)
            {
                int numberOfImprovements = Random.Range(0, _improvementsList.Length);

                if (numberOfImprovements == _improvementsList.Length)
                    numberOfImprovements = _improvementsList.Length - 1;

                if (NumberMatches(createImprovements, numberOfImprovements) == false)
                {
                    TraderCell cell = Instantiate(_improvementsList[numberOfImprovements], 
                        _container.transform, false);

                    cell.OnTryBuyCell += BuyCell;

                    createImprovements[i] = numberOfImprovements;
                }
                else
                {
                    i--;
                }
            }
        }
        private bool NumberMatches(int[] array, int number)
        {
            bool matchFound = false;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == number)
                {
                    matchFound = true;

                    return matchFound;
                }
            }

            return matchFound;
        }
    }
}
