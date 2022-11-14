using RetroAstro.Levels;
using RetroAstro.Pooling;

namespace RetroAstro.Entities.PhysicsEntities
{
    public class BigAsteroid : Asteroid
    {
        protected override void OnCustomEntityPoolAdd(QuickEntityPool quickEntityPool)
        {
            quickEntityPool.Enlist(this);
        }

        protected override float GetVelocityStrength()
        {
            return UnityEngine.Random.Range(1f, 2f);
        }


        public override int GetPointsGiven()
        {
            return 3;
        }

        public override void OnEntitySpawn(BaseLevel baseLevel)
        {
            base.OnEntitySpawn(baseLevel);
            
            
        }
    }
}