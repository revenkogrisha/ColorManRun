namespace ColorManRun.Characters
{
    public struct Statistics
    {
        public float HealthAmount;
        public float DamageAmount;
        public float DamageResistance;
        public float StunDuration;
        public float MovementSpeed;

        public Statistics(StatsConfig config)
        {
            HealthAmount = config.HealthAmount;
            DamageAmount = config.DamageAmount;
            DamageResistance = config.DamageResistance;
            StunDuration = config.StunDuration;
            MovementSpeed = config.MovementSpeed;
        }
    }
}
