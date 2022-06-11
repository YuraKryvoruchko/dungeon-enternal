using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace DungeonEternal.UI
{
    public class SeterOnStart : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onStart;

        private void OnEnable()
        {
            _onStart?.Invoke();
        }
    }
}
