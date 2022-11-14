using RetroAstro.Levels;
using RetroAstro.Pooling;
using UnityEngine;

namespace RetroAstro.Entities.PhysicsEntities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PhysicsEntity : Entity
    {
        private Rigidbody2D _rigidbody2D;

        protected override void Awake()
        {
            base.Awake();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

    

        public override void OnEntitySpawn(BaseLevel level)
        {
            base.OnEntitySpawn(level);
            _rigidbody2D.velocity = Vector2.zero;
        }

        private void Start()
        {
            _rigidbody2D.gravityScale = 0;
        }

        public void SetVelocity(Vector2 vel)
        {
            _rigidbody2D.velocity = vel;

        }
    }
}