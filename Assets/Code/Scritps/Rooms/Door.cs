using UnityEngine;

namespace DungeonEternal.Rooms
{
    [RequireComponent(typeof(Animator))]
    public class Door : MonoBehaviour
    {
        [SerializeField] private string _nameParameterOpen = "Open";
        [SerializeField] private string _nameParameterClose = "Close";

        [SerializeField] private Door—ondition _Òondition = Door—ondition.Open;

        private Animator _animation;

        private void Awake()
        {
            _animation = GetComponent<Animator>();
        }

        public void OpenDoor()
        {
            if(_Òondition == Door—ondition.—losed)
            {
                _Òondition = Door—ondition.Open;

                _animation.SetTrigger(_nameParameterOpen);
            }
        }
        public void CloseDoor()
        {
            if (_Òondition == Door—ondition.Open)
            {
                _Òondition = Door—ondition.—losed;

                _animation.SetTrigger(_nameParameterClose);
            }
        }
        public void Set—ondition(Door—ondition condition)
        {
            if (_Òondition != condition)
            {
                switch (condition)
                {
                    case Door—ondition.Open:
                        _Òondition = Door—ondition.Open;

                        _animation.SetTrigger(_nameParameterOpen);

                        break;

                    case Door—ondition.—losed:
                        _Òondition = Door—ondition.—losed;

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

    public enum Door—ondition
    {
        Open,
        —losed
    }
}
