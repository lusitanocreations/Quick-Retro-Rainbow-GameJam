using System;
using System.Collections;
using System.Drawing;
using RetroAstro.Core;
using RetroAstro.Entities.PhysicsEntities;
using RetroAstro.Pooling;
using RetroAstro.QuickEvents;
using RetroAstro.Utils.Math;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RetroAstro.Levels
{

    public enum DifficultyLevel
    {
        Hard,
        Medium,
        Easy
    }
    public class BaseLevel
    {



        private DifficultyLevel _difficultyLevel;
        public float LevelRadius { get; }
        public World World { get;  }

        private Coroutine spawnCor;
        private Coroutine timerCor;

        public float TimePassed => timePassed;

        public BaseLevel(World world, int levelRadius,DifficultyLevel _difficultyLevel)
        {

            this.LevelRadius = levelRadius;
            this.World = world;
            SetDifficult(_difficultyLevel);
            
            
        }

       
        private void SetDifficult(DifficultyLevel difficultyLevel)
        {
            _difficultyLevel = difficultyLevel;

            switch (_difficultyLevel)
            {
                case DifficultyLevel.Easy:
                    timer = 0.9f;
                    break;
                case DifficultyLevel.Hard:
                    timer = 0.5f;
                    break;
                case DifficultyLevel.Medium:
                    timer =0.6f;
                    break;
            }
        }

        public void StartLevel()
        {
            spawnCor = World.StartCoroutine(StartSpawning());
            timerCor = World.StartCoroutine(Reduce());

        }

        public void StopLevel()
        {
            World.StopCoroutine(spawnCor);
            World.StopCoroutine(timerCor);
            
        }


        private float timer;
        private int timePassed;

        IEnumerator Reduce()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                
                timePassed += 1;
                Debug.Log(timePassed);
                
                if (timePassed >= 20)
                {
                    GameEventPS.CallOnGameEnd();
                    yield break;
                }

                switch (_difficultyLevel)
                {
                    case DifficultyLevel.Easy:
                        timer -= 0.02f;
                        break;
                    case DifficultyLevel.Medium:
                        timer -= 0.025f;
                        break;
                    case DifficultyLevel.Hard:
                        timer -= 0.03f;
                        break;
                }
              
            }
       
        }
        
        private IEnumerator StartSpawning()
        {
            while (true)
            {

                int rn = Random.Range(0, 100);


                int chanceToDrop = 0;
                int howManyToDrop = 0;
                
                switch (_difficultyLevel)
                {
                    case DifficultyLevel.Easy:
                        chanceToDrop = 5;
                        howManyToDrop = 1;
                        break;
                    case DifficultyLevel.Medium:
                        chanceToDrop = 15;
                        howManyToDrop = 2;
                        break;
                    case DifficultyLevel.Hard:
                        chanceToDrop = 20;
                        howManyToDrop = 3;
                        break;
                }
                
                if (rn >= chanceToDrop)
                {
                    for (int i = 0; i < UnityEngine.Random.Range(1,howManyToDrop); i++)
                    {
                        World.SpawnEntity<Asteroid>();
                    }
                }
                else
                {
                    World.SpawnEntity<BigAsteroid>();
                }
             
             
                yield return new WaitForSeconds(timer);

            }

        }


        
     

    }
}
