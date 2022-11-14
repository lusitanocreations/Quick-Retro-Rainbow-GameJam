using UnityEngine;

namespace RetroAstro.Utils
{
    public static class ColorUtils
    {
        public static Color RandomColor()
        {
            byte r =  (byte) Random.Range(0, 255);
            byte g =  (byte) Random.Range(0, 255);
            byte b =  (byte) Random.Range(0, 255);
            Color32 c32 = new Color32(r, g, b, 255);
            return c32;
        }
    
    }
}