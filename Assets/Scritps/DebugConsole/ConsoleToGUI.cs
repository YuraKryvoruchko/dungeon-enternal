using System.IO;
using UnityEngine;

namespace Assets.Scritps.DebugConsole
{
    #if DEVELOPMENT_BUILD || UNITY_EDITOR
    public class ConsoleToGUI : MonoBehaviour
    {
        [SerializeField] private string _nameFile = "DebugText.txt";

        [Space]
        [SerializeField] private int _maxLenght = 1000;

        [Space]
        [SerializeField] private bool _doShow = true;

        [Space]
        [SerializeField] private KeyCode _openOrClose = KeyCode.Z;
        [SerializeField] private KeyCode _saveButton = KeyCode.X;

        private string _path;

        private string _overLog;
        private string _myLog;

        private void Awake()
        {
            _path = Application.dataPath + _nameFile;
        }

        private void OnEnable()
        {
            Application.logMessageReceived += Write;
        }
        private void OnDisable()
        {
            Application.logMessageReceived -= Write;
        }

        private void Update()
        {
            if (Input.GetKeyDown(_openOrClose))
                _doShow = !_doShow;

            if (Input.GetKeyDown(_saveButton))
            {
                StreamWriter fileStream = new StreamWriter(_path, false);

                fileStream.Write(_overLog);

                fileStream.Close();
            }
        }

        private void Write(string condition, string stackTrace, LogType type)
        {
            _overLog += "\n\n" + stackTrace + ": " + condition;
            _myLog += "\n\n" + condition;
        }

        private void OnGUI()
        {
            if (_doShow == false)
                return;

            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity,
                new Vector3(Screen.width / 1200.0f, Screen.height / 800.0f, 1.0f));

            GUI.BeginScrollView(new Rect(10, 300, 540, 400), Vector2.zero, new Rect(0, 0, 1000, _myLog.Length));

            GUI.TextArea(new Rect(10, 10, 540, _myLog.Length), _myLog);

            GUI.EndScrollView();
        }
    }
    #endif
}
