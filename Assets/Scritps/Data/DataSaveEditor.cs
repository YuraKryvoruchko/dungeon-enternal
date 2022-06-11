using System.IO;
using UnityEngine;

namespace DungeonEternal.Data
{
    public class DataSaveEditor
    {
        public static void Save(object data, string pathName)
        {
            string path = Application.dataPath + "/" + pathName;

            string json = JsonUtility.ToJson(data);

            File.WriteAllText(path, json);
        }
        public static T GetData<T>(string pathName)
        {
            string path = Application.dataPath + "/" + pathName;

            T data;

            if (File.Exists(path) == true)
            {
                var jsonString = File.ReadAllText(path);

                data = JsonUtility.FromJson<T>(jsonString);
            }
            else
            {
                data = default;
            }

            return data;
        }
    }
}
