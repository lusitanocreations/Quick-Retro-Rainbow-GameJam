using System;
using System.Collections;
using RetroAstro.Core;
using RetroAstro.Entities.PlayerEntity;
using RetroAstro.Levels;
using RetroAstro.Pooling;
using Unity.VisualScripting;
using UnityEngine;

namespace RetroAstro.Entities.PhysicsEntities
{
    public class Projectile : PhysicsEntity
    {
        public Team TargetTeam { get;  set; }
        [SerializeField] private int damage;

        
        
        private Coroutine killCor;
        private Entity owner;

        public void SetOwner(Entity entity)
        {
            owner = entity;
        }

        public override void Construct(World world, QuickEntityPool quickEntityPool)
        {
            base.Construct(world, quickEntityPool);
            TargetTeam = Team.Friendly;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            IDamagableEntity damagableEntity = col.GetComponent<IDamagableEntity>();
            if(damagableEntity != null)
            {
                if (damagableEntity.Team == TargetTeam)
                {
                    damagableEntity.OnDamageTaken(damage);
                    Kill();
                    if (killCor != null)
                    {
                        StopCoroutine(killCor);
                    }
                }
                
            }
        }

        public override void OnEntitySpawn(BaseLevel level)
        {
            base.OnEntitySpawn(level);

            SetVelocity(Vector2.zero);
            killCor = StartCoroutine(KillTimer());
        }

        IEnumerator KillTimer()
        {
            yield return new WaitForSeconds(2f);
            Kill();
        }

        protected override void OnCustomEntityPoolAdd(QuickEntityPool quickEntityPool)
        {
            quickEntityPool.Enlist(this);
            
        }

    }
}

