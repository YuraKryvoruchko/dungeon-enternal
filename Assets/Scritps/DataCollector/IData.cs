using UnityEngine;

namespace DungeonEternal.AI.DataCollector
{
    public interface IData
    {
        Vector3 SendData();
        void PrintData(Vector3 positionInTime);
    }
}
