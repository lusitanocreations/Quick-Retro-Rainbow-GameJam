using System.Collections.Generic;
using RetroAstro.Entities;
using UnityEngine;

namespace RetroAstro.Registry
{
   [CreateAssetMenu(menuName = "Create RegisteredEntities", fileName = "RegisteredEntities", order = 0)]
   public class RegisteredEntities : ScriptableObject
   {
      [SerializeField] private List<Entity> entities = new List<Entity>();


      public List<Entity> AllRegisteredEntities => entities;

   }
}
