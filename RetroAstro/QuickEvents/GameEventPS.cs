using System;

namespace RetroAstro.QuickEvents
{
    public static class GameEventPS
    {
        public static event Action OnGameStart;
        public static event Action OnGameEnd;


        public static void CallOnGameStart()
        {
            OnGameStart?.Invoke();
        }
        public static void CallOnGameEnd()
        {
            OnGameEnd?.Invoke();
        }
        

    }
}