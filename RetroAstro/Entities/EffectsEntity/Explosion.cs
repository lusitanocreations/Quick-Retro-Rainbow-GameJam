using System.Collections;
using RetroAstro.Levels;
using RetroAstro.Pooling;
using UnityEngine;

namespace RetroAstro.Entities.EffectsEntity
{
    
    [RequireComponent(typeof(Animator))]
    public class Explosion : EffectsEntity
    {
        private Animator _animator;
        private static readonly int Explode = Animator.StringToHash("Explode");

        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
        }

        public override void OnEntitySpawn(BaseLevel baseLevel)
        {
            base.OnEntitySpawn(baseLevel);
            _animator.SetTrigger(Explode);
            StartCoroutine(SelfRighteousSuicide());
        }

        IEnumerator SelfRighteousSuicide()
        {
            yield return new WaitForSeconds(1.5f);
            Kill();
        }
        protected override void OnCustomEntityPoolAdd(QuickEntityPool quickEntityPool)
        {
            quickEntityPool.Enlist(this);
        }
        
    }
}