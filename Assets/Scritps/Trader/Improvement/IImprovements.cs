namespace DungeonEternal.TrayderImprovement
{
    public interface IImprovementDamage
    {
        void SetNewDamage(float newDamage);
        void IncreaseDamageBy(float damage);
        void IncreaseDamageByInPercentage(float percentage);
    }
    public interface IImprovementRateOfFire
    {
        void SetNewRate(float newRate);
        void IncreaseRateBy(float rate);
        void IncreaseRateByInPercentage(float percentage);
    }
    public interface IImprovementStoreCapacity
    {
        void SetNewCapacity(int newCapacity);
        void IncreaseCapacityBy(int capacity);
        void IncreaseCapacityByInPercentage(float percentage);
    }
    public interface IImprovementReloadSpeed
    {
        void SetNewReloadSpeed(float newReloadSpeed);
        void IncreaseReloadSpeedBy(float reloadSpeed);
        void IncreaseReloadSpeedByInPercentage(float percentage);
    }

    public interface IImprovementHealth
    {
        void SetNewHealth(float newHealth);
        void IncreaseHealthBy(float health);
        void IncreaseHealthByInPercentage(float percentage);
    }
    public interface IImprovementSpeedRun
    {
        void SetNewSpeed(float newSpeed);
        void IncreaseSpeedBy(float speed);
        void IncreaseSpeedByInPercentage(float percentage);
    }

    public interface IImprovementAward
    {
        void SetNewAward(float newAward);
        void IncreaseAwardBy(float award);
        void IncreaseAwardByInPercentage(float percentage);
    }
}
