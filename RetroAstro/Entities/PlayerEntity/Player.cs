using System;
using RetroAstro.Entities.PhysicsEntities;
using RetroAstro.Levels;
using RetroAstro.Pooling;
using RetroAstro.QuickEvents;
using RetroAstro.Utils;
using UnityEngine;

namespace RetroAstro.Entities.PlayerEntity
{
    public class Player : Entity, IDamagableEntity
    {
        private Camera _camera;
        private bool canUpdate;

        private int points;
   
        protected override void Awake()
        {
            base.Awake();
            _camera = FindObjectOfType<Camera>();
        }

        public void GivePoints(int points)
        {
            this.points += points;
        }
        private void OnEnable()
        {
            GameEventPS.OnGameStart += Innit;
            GameEventPS.OnGameEnd += Dinnit;
        }

        private void OnDisable()
        {
            GameEventPS.OnGameStart -= Innit;
            GameEventPS.OnGameEnd -= Dinnit;
        }
        
        public int GetPoints() => points;

        private void Innit()
        {
            canUpdate = true;
        }

        private void Dinnit()
        {
            canUpdate = false;

        }

        protected override void OnCustomEntityPoolAdd(QuickEntityPool quickEntityPool)
        {
            quickEntityPool.Enlist(this);
        }


        private float t = 0;
        void Update()
        {

            if(!canUpdate) return;

            t += Time.deltaTime;
            Vector2 point = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = point -  (Vector2) transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.AngleAxis(angle -90, Vector3.forward);
            transform.rotation = rot;


            float dl = 0.1f;

            if (World.WorldDifficulty == DifficultyLevel.Hard)
            {
                dl = 0.02f;
            }
            if (t >= dl)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    Projectile e = World.SpawnEntity<Projectile>();
                    Color rn = ColorUtils.RandomColor();

                    e.TargetTeam = Team.AbsolutelyNotFriendlyAtAll;
                    e.SetEntityColor(rn);
                    e.transform.position = transform.position;
                    e.SetVelocity(direction.normalized * 10);
                    t = 0;

                }
            }

          
        
        }


        public void OnDamageTaken(int amount)
        {
            ReduceHealth(amount);
        }

        protected override void OnDeath()
        {
            base.OnDeath();
            GameEventPS.CallOnGameEnd();
            
        }

        public Team Team { get; }
    }
}
