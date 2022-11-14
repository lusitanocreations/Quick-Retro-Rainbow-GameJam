using System;
using System.Collections;
using System.Collections.Generic;
using Lusofinn.Audio;
using RetroAstro.Databases;
using RetroAstro.Entities;
using RetroAstro.Entities.EffectsEntity;
using RetroAstro.Entities.PhysicsEntities;
using RetroAstro.Entities.PlayerEntity;
using RetroAstro.Levels;
using RetroAstro.Pooling;
using RetroAstro.QuickEvents;
using UnityEngine;


namespace RetroAstro.Core
{
    public class World : MonoBehaviour
    {
        private GameState _state;

        public DifficultyLevel WorldDifficulty { get; private set; }
        private SoundSystem _system;
        private QuickEntityPool _quickEntityPool;
        private BaseLevel level;
        public Player Player { get; private set; }

        private List<Entity> EntitiesUpdates = new List<Entity>();


        
        private void Awake()
        {
            
            _quickEntityPool = FindObjectOfType<QuickEntityPool>();
            _system = FindObjectOfType<SoundSystem>();
        }

        private void Update()
        {
            if(_state != GameState.Running) return;

            for (var i = 0; i < EntitiesUpdates.Count; i++)
            {
                EntitiesUpdates[i].OnTick();
                
            }
        }

        public void PlaySound(string tag)
        {
            _system.PlaySound(tag);
        }

        public IEnumerator Innit()
        {
            DifficultyLevel difficultyLoaded = (DifficultyLevel) PlayerPrefs.GetInt("difficulty");
            WorldDifficulty = difficultyLoaded;
            level = new BaseLevel(this,15,difficultyLoaded);
            SpawnPlayer();
            yield return InnitWorld();
            

        }
        private void SpawnPlayer()
        {
            GameEntitiesDatabase gameEntity = (GameEntitiesDatabase) GameEntitiesDatabase.Instance;
            Player p = gameEntity.GetNewInstance<Player>();
            Player = p;
            p.Construct(this,_quickEntityPool);
            p.transform.position = Vector2.zero;

        }
        IEnumerator InnitWorld()
        {
            yield return _quickEntityPool.CacheEntities<Projectile>(this,150);
            yield return _quickEntityPool.CacheEntities<Asteroid>(this, 30);
            yield return _quickEntityPool.CacheEntities<BigAsteroid>(this, 10);
            yield return _quickEntityPool.CacheEntities<Explosion>(this, 30);
            
        }

        private void OnEnable()
        {
            GameEventPS.OnGameEnd += EndWorld;
            GameEventPS.OnGameStart += StartWorld;
        }

        private void StartWorld()
        {
            _state = GameState.Running;
            level.StartLevel();
        }

        private void EndWorld()
        {
            _state = GameState.Ended;
            for (var i = 0; i < EntitiesUpdates.Count; i++)
            {
                RemoveEntity(EntitiesUpdates[i]);
                
            }
            level.StopLevel();
        }
        private void OnDisable()
        {
            GameEventPS.OnGameEnd -= EndWorld;
            GameEventPS.OnGameStart -= StartWorld;

        }

        public T SpawnEntity<T>() where T : Entity
        {
            T t = _quickEntityPool.Get<T>(this);
            t.OnEntitySpawn(level);
            EntitiesUpdates.Add(t);
            return t;
        }
        public void RemoveEntity<T>(T k) where T : Entity
        {
            EntitiesUpdates.Remove(k);
        }
    }
}
