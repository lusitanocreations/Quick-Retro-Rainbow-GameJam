using UnityEngine;

namespace RetroAstro.Entities
{
    [System.Serializable]
    public struct EntityProperties
    {
        [SerializeField] private int health;

        public int BaseHealth => health;

    }
}