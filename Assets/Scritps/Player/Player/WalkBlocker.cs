using UnityEngine;
using StarterAssets;
using DungeonEternal.MiniMap;
using DungeonEternal.UI;
using DungeonEternal.TrayderImprovement;

namespace DungeonEternal.Player
{
    public class WalkBlocker : MonoBehaviour
    {
        [SerializeField] private bool _isBlockPlayerOnStart = true;

        private FirstPersonController _firstPersonController;

        private float _walkSpeed = 6f;
        private float _sprintSpeed = 6f;

        private const float MIN_SPEED = 0f;

        private void OnEnable()
        {
            BigMiniMap.OnEnableBigMap += BlockTheWalk;
            BigMiniMap.OnDisableBigMap += UnlockWalk;
            InstructionMenu.OnShowInstructionMenu += BlockTheWalk;
            InstructionMenu.OnCloseInstructionMenu += UnlockWalk;
            Trader.OnStartTrade += BlockTheWalk;
            Trader.OnEndTrade += UnlockWalk;
        }
        private void OnDisable()
        {
            BigMiniMap.OnEnableBigMap -= BlockTheWalk;
            BigMiniMap.OnDisableBigMap -= UnlockWalk;
            InstructionMenu.OnShowInstructionMenu -= BlockTheWalk;
            InstructionMenu.OnCloseInstructionMenu -= UnlockWalk;
            Trader.OnStartTrade -= BlockTheWalk;
            Trader.OnEndTrade -= UnlockWalk;
        }

        private void Awake()
        {
            _firstPersonController = GetComponent<FirstPersonController>();

            if (_isBlockPlayerOnStart == true)
                BlockTheWalk();
        }

        private void BlockTheWalk()
        {
            _walkSpeed = _firstPersonController.MoveSpeed;
            _sprintSpeed = _firstPersonController.SprintSpeed;

            _firstPersonController.MoveSpeed = MIN_SPEED;
            _firstPersonController.SprintSpeed = MIN_SPEED;
        }
        private void UnlockWalk()
        {
            _firstPersonController.MoveSpeed = _walkSpeed;
            _firstPersonController.SprintSpeed = _sprintSpeed;
        }
    }
}
