using System.Collections;
using UnityEngine;


namespace Runtime.BT.Singleton
{
    public abstract class Singleton<T> where T : Singleton<T>, new()
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
                        if (m_instance == null)
                        {
                            m_instance = new T();
                            m_instance.Init();
                        }
                    }
                }

                return m_instance;
            }
        }

        // 싱글톤 객체의 초기화 함수
        public virtual void Init()
        {

        }

        public static bool HasInstance()
        {
            return (m_instance != null) ? true : false;
        }
    }
}
