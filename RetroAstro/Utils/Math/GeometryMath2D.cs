using UnityEngine;

namespace RetroAstro.Utils.Math
{
    public static class GeometryMath2D
    {
        
        public static Vector2 RandomPointOnCircleEdge(float radius) =>  Random.insideUnitCircle.normalized * radius;

    }
}