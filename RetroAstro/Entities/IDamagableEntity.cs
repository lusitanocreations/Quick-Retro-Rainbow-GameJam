namespace RetroAstro.Entities
{
    public interface IDamagableEntity
    {
        public void OnDamageTaken(int amount);
        public Team Team { get; }
    }
}