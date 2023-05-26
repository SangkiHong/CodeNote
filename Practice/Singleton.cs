using UnityEngine;

namespace SK.Practice
{
    public class Singleton<T> : MonoBehaviour where T : class, new()
    {
        protected static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                    instance = new T();

                return instance;
            }
        }
    }
}