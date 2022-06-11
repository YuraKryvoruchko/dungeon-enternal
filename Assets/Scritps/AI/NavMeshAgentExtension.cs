using UnityEngine.AI;
public static class NavMeshAgentExtension
{
    public static bool CheckPathComplete(this NavMeshAgent navMeshAgent)
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            return true;
        else
            return false;
    }
}
