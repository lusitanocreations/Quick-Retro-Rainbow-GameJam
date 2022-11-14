using System;
using System.Collections;
using System.Collections.Generic;
using RetroAstro.Core;
using RetroAstro.Databases;
using RetroAstro.Entities;
using UnityEngine;

namespace RetroAstro.Pooling
{
    public class QuickEntityPool : MonoBehaviour
    {


        private Dictionary<Type, Queue<Entity>> _quickPool = new();


        public IEnumerator CacheEntities<T>(World world,int quantity = 1) where T: Entity
        {
            if (quantity <= 0) quantity = 1;
        
            GameEntitiesDatabase gameEntity = (GameEntitiesDatabase) GameEntitiesDatabase.Instance;

    
            for (int i = 0; i < quantity; i++)
            {
                T entity = gameEntity.GetNewInstance<T>();
                entity.Construct(world,this);
                entity.OnEntityCache();
                Enlist(entity);
                yield return null;
            }

        }

        public void Enlist<T>(T obj) where  T: Entity
        {
            if(_quickPool.TryGetValue(typeof(T),out Queue<Entity> q))
            {
                q.Enqueue(obj);
       
                return;
            
            }

            Queue<Entity> queue = new Queue<Entity>();

            _quickPool.Add(typeof(T),queue);
            queue.Enqueue(obj);
        
        }

        public T Get<T>(World world) where T: Entity
        {
            
            if (_quickPool.TryGetValue(typeof(T), out Queue<Entity> pool) && pool.Count != 0)
            {
                return pool.Dequeue() as T;

            }
       
            GameEntitiesDatabase gameEntity = (GameEntitiesDatabase) GameEntitiesDatabase.Instance;

            T entity = gameEntity.GetNewInstance<T>();
            entity.Construct(world,this);
            return entity;


        }
    }
}
