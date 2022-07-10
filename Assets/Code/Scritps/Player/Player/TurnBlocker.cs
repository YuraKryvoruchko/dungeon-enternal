using UnityEngine;
using StarterAssets;
using DungeonEternal.MiniMap;
using DungeonEternal.UI;
using DungeonEternal.TrayderImprovement;

namespace DungeonEternal.Player
{
    public class TurnBlocker : MonoBehaviour
    {
        [SerializeField] private bool _isBlockPlayerOnStart = true;

        private FirstPersonController _firstPersonController;

        private float _rotationSpeed = 1f;

        private const float MIN_SPEED_ROTATION = 0f;

        private void OnEnable()
        {
            BigMiniMap.OnEnableBigMap += BlockTheTurn;
            BigMiniMap.OnDisableBigMap += UnlockTurn;
            InstructionMenu.OnShowInstructionMenu += BlockTheTurn;
            InstructionMenu.OnCloseInstructionMenu += UnlockTurn;
            Trader.OnStartTrade += BlockTheTurn;
            Trader.OnEndTrade += UnlockTurn;
        }
        private void OnDisable()
        {
            BigMiniMap.OnEnableBigMap -= BlockTheTurn;
            BigMiniMap.OnDisableBigMap -= UnlockTurn;
            InstructionMenu.OnShowInstructionMenu -= BlockTheTurn;
            InstructionMenu.OnCloseInstructionMenu -= UnlockTurn;
            Trader.OnStartTrade -= BlockTheTurn;
            Trader.OnEndTrade -= UnlockTurn;
        }

        private void Start()
        {
            _firstPersonController = GetComponent<FirstPersonController>();

            _rotationSpeed = _firstPersonController.RotationSpeed;

            if (_isBlockPlayerOnStart == true)
                BlockTheTurn();
        }

        private void BlockTheTurn()
        {
            _rotationSpeed = _firstPersonController.RotationSpeed;

            _firstPersonController.RotationSpeed = MIN_SPEED_ROTATION;
        }
        private void UnlockTurn()
        {
            _firstPersonController.RotationSpeed = _rotationSpeed;
        }
    }
}
