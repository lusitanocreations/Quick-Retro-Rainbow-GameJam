using System;
using RetroAstro.Core;
using RetroAstro.Levels;
using RetroAstro.Pooling;
using RetroAstro.Utils;
using UnityEngine;

namespace RetroAstro.Entities
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Entity : MonoBehaviour
    {

        [SerializeField] private EntityProperties _properties;
        private SpriteRenderer _renderer;
        public World World { get; private set; }
        private QuickEntityPool _quickEntityPool;


        
        
        protected int CurrentHealth { get; private set; }

        private bool isDeath;

        protected virtual void Awake()
        {
          
            _renderer = GetComponent<SpriteRenderer>();
            
        }

        protected virtual void OnGameEnd()
        {
            
        }



        public virtual void OnTick()
        {
            SetEntityColor(ColorUtils.RandomColor());
        }

        public void Kill()
        {
            ReduceHealth(CurrentHealth);
        }

        public void ReduceHealth(int amount)
        {
            if(isDeath) return;
            
            CurrentHealth -= amount;

            if (CurrentHealth <= 0)
            {
                isDeath = true;
                OnDeath();
            }
           
            
        }

        public void OnEntityCache()
        {
            gameObject.SetActive(false);
        }


        public void SetEntityColor(Color color)
        {
            _renderer.color = color;
        }

        public  virtual void Construct(World world, QuickEntityPool quickEntityPool)
        {
            this.World = world;
            isDeath = false;
            this._quickEntityPool = quickEntityPool;
            CurrentHealth = _properties.BaseHealth;

        }

        protected abstract void OnCustomEntityPoolAdd(QuickEntityPool quickEntityPool);
       

        public virtual void OnEntitySpawn(BaseLevel baseLevel)
        {
            isDeath = false;
            ResetHP();
            gameObject.SetActive(true);
            
        }

        public void ResetHP()
        {
            CurrentHealth = _properties.BaseHealth;

        }

        protected virtual void OnDeath()
        {
            gameObject.SetActive(false);
            OnCustomEntityPoolAdd(_quickEntityPool);
            World.RemoveEntity(this);
            
        }

        
    }
}