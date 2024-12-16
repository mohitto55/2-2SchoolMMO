using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.BT.Singleton
{
    public abstract class SerializedMonoSingleton<T> : SerializedMonoBehaviour where T : SerializedMonoSingleton<T>
    {
        private static object syncObject = new object();

        private static T m_instance;

        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {

                    lock (syncObject)
                    {
                        m_instance = FindObjectOfType<T>();

                        if (m_instance == null)
                        {
                            GameObject obj = new GameObject();
                            obj.name = typeof(T).Name;
                            m_instance = obj.AddComponent<T>();
                        }
                    }
                }

                return m_instance;
            }
        }

        protected virtual void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this as T;
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }


        private void OnDestroy()
        {
            if (m_instance != this)
                return;

            m_instance = null;
        }

        public static bool HasInstance()
        {
            return m_instance ? true : false;
        }
    }
}