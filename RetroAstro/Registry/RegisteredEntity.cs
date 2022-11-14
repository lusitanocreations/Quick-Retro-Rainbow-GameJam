using RetroAstro.Entities;
using UnityEngine;

namespace RetroAstro.Registry
{
  public class RegisteredEntity : ScriptableObject
  {
    [SerializeField] private Entity _entity;
    [SerializeField] private string entityTag;


    public Entity Entity => _entity;
    public string EntityTag => entityTag;
  }
}
