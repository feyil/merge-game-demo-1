using UnityEngine;

namespace _game.Scripts.Utility
{
    public class Singleton<T> where T : class, new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();

                    if (_instance == null)
                    {
                        Debug.LogWarning("An instance of " + typeof(T) +
                                         " is needed in the scene, but there is none.");
                    }
                }

                return _instance;
            }
        }
#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetInstance()
        {
            _instance = null;
        }
#endif
    }
}