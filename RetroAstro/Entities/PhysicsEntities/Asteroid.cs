using System;
using Lusofinn.Audio;
using RetroAstro.Entities.EffectsEntity;
using RetroAstro.Entities.PlayerEntity;
using RetroAstro.Levels;
using RetroAstro.Pooling;
using RetroAstro.QuickEvents;
using RetroAstro.Utils.Math;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RetroAstro.Entities.PhysicsEntities
{
    public class Asteroid : PhysicsEntity, IDamagableEntity
    {
      
        protected override void OnCustomEntityPoolAdd(QuickEntityPool quickEntityPool)
        {
            quickEntityPool.Enlist(this);

        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            Player p = col.GetComponent<Player>();
            if (p != null)
            {
                GameEventPS.CallOnGameEnd();
            }
        }

        protected override void OnDeath()
        {
            base.OnDeath();
            World.Player.GivePoints(GetPointsGiven());
            World.SpawnEntity<Explosion>().transform.position = transform.position;
            World.PlaySound("Explosion");
            SetVelocity(Vector2.zero);
        }

        public virtual int GetPointsGiven()
        {
            return 1;
        }
        public override void OnEntitySpawn(BaseLevel baseLevel)
        {
            base.OnEntitySpawn(baseLevel);

            Player p = World.Player;
            Vector2 point = GeometryMath2D.RandomPointOnCircleEdge(baseLevel.LevelRadius);
            transform.position = point;
            float velocity = GetVelocityStrength();
            Vector3 dir = p.transform.position - transform.position ;
            SetVelocity(dir.normalized * velocity   );

        }

        protected virtual float GetVelocityStrength()
        {
            return Random.Range(2f, 5f);
        }

      

        public void OnDamageTaken(int amount)
        {
            ReduceHealth(amount);
        }

        public Team Team => Team.AbsolutelyNotFriendlyAtAll;
    }
}
