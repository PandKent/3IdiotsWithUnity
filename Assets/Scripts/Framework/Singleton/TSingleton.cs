using System;
using UnityEngine;

namespace Framework.Singleton
{
    public class TSingleton<T> : MonoBehaviour where T : MonoBehaviour, new()
    {
        private static T m_instance;
        private static object m_lock = new object();

        public static T Instance
        {
            get
            {
                lock (m_lock)
                {
                    if (m_instance == null)
                    {
                        m_instance = (T) FindObjectOfType(typeof(T));
//                        if (FindObjectsOfType(typeof(T)).Length > 1)//如果找到的对象数量大于1，只返回之前找到的第一个
//                        {
//                            return m_instance;
//                        }

                        if (m_instance == null) //未找到
                        {
                            GameObject singleton = new GameObject();//造一个对象
                            m_instance = singleton.AddComponent<T>();//将这个对象转变为类型为k的实例，m_instance引用这个singleton实例
                            singleton.name = "(Singleton of)" + typeof(T).ToString();//给这个实例对象命名
                            
                        }
                        DontDestroyOnLoad(m_instance.gameObject);//无论场景如何转换都不得删除这个实例；
                    }
                }

                return m_instance;
            }
        }
    }
}