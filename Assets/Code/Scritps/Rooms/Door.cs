using UnityEngine;

namespace DungeonEternal.Rooms
{
    [RequireComponent(typeof(Animator))]
    public class Door : MonoBehaviour
    {
        [SerializeField] private string _nameParameterOpen = "Open";
        [SerializeField] private string _nameParameterClose = "Close";

        [SerializeField] private Door�ondition _�ondition = Door�ondition.Open;

        private Animator _animation;

        private void Awake()
        {
            _animation = GetComponent<Animator>();
        }

        public void OpenDoor()
        {
            if(_�ondition == Door�ondition.�losed)
            {
                _�ondition = Door�ondition.Open;

                _animation.SetTrigger(_nameParameterOpen);
            }
        }
        public void CloseDoor()
        {
            if (_�ondition == Door�ondition.Open)
            {
                _�ondition = Door�ondition.�losed;

                _animation.SetTrigger(_nameParameterClose);
            }
        }
        public void Set�ondition(Door�ondition condition)
        {
            if (_�ondition != condition)
            {
                switch (condition)
                {
                    case Door�ondition.Open:
                        _�ondition = Door�ondition.Open;

                        _animation.SetTrigger(_nameParameterOpen);

                        break;

                    case Door�ondition.�losed:
                        _�ondition = Door�ondition.�losed;

                        _animation.SetTrigger(_nameParameterClose);

                        break;
                }
            }
            else
            {
                Debug.LogWarning("The door is already in this state");
            }
        }
    }

    public enum Door�ondition
    {
        Open,
        �losed
    }
}
