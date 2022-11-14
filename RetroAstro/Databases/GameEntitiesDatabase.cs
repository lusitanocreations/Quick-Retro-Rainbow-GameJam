using System;
using System.Collections.Generic;
using RetroAstro.Entities;
using RetroAstro.Registry;
using UnityEngine;

namespace RetroAstro.Databases
{

    public abstract class FastSingleThreadMonoSingleton<T>: MonoBehaviour 
    {
        public static FastSingleThreadMonoSingleton<T> Instance { get; private set; }
        
       
        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
    }
    public class GameEntitiesDatabase : FastSingleThreadMonoSingleton<GameEntitiesDatabase>
    {
        [SerializeField] private RegisteredEntities entities;

        private Dictionary<Type, Entity> _mapTagToEntity = new();
        


        protected override void Awake()
        {
            base.Awake();

        }

        private void Start()
        {
            Innit();
        }

        private void Innit()
        {
            List<Entity> allRegisteredEntities = entities.AllRegisteredEntities;
            
            for (var i = 0; i < allRegisteredEntities.Count; i++)
            {
                Type entType = allRegisteredEntities[i].GetType();
                _mapTagToEntity[entType] = allRegisteredEntities[i];

            }
        }

        public T GetNewInstance<T>() where T : Entity
        {
            return  Instantiate(_mapTagToEntity[typeof(T)]) as T;
        }
    }
}