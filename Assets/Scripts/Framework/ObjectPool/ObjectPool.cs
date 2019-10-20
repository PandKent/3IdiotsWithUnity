using System.Collections.Generic;
using Framework.Singleton;
using UnityEngine;

namespace Framework.ObjectPool
{
    public interface ICacheAble
    {
        void OnCacheReset();

        bool cacheFlag
        {
            get;
            set;
        }
    }
    
    public interface CountObserverAble
    {
        int currentCount
        {
            get;
        }
    }
    
    public class ObjectPool<T> : TSingleton<ObjectPool<T>>, CountObserverAble where T : ICacheAble, new()
    {
        private int m_maxCount = 0;
        private Stack<T> m_CacheStack;
        private int m_createCount = 0;
        private int m_maxCreateCount = 0;

        public int currentCount
        {
            get
            {
                if (m_CacheStack == null)
                {
                    return 0;
                }

                return m_CacheStack.Count;
            }
        }

        public int maxCreateCount
        {
            get { return m_maxCreateCount; }
            set { m_maxCreateCount = value; }
        }

        public int maxCacheCount
        {
            get { return m_maxCount; }
            set
            {
                m_maxCount = value;
                if (m_CacheStack != null)
                {
                    if (m_maxCount > 0)
                    {
                        if (m_maxCount < m_CacheStack.Count)
                        {
                            int removeCount = m_maxCount - m_CacheStack.Count;
                            while (removeCount > 0)
                            {
                                m_CacheStack.Pop();
                                --removeCount;
                            }
                        }
                    }
                }
            }
        }

        public void Init(int maxCount, int initCount)
        {
            if (maxCount > 0)
            {
                initCount = Mathf.Min(maxCount, initCount);
            }

            m_maxCount = maxCount;

            if (currentCount < initCount)
            {
                for (int i = currentCount; i < initCount; ++i)
                {
                    Recycle(new T());
                }
            }
        }

        public T Allocate()
        {
            T result;
            if (m_CacheStack == null || m_CacheStack.Count == 0)
            {
                if (m_maxCreateCount == 0 || (m_maxCreateCount > 0 && m_createCount < m_maxCreateCount))
                {
                    ++m_createCount;
                    result=new T();
                }
                else
                {
                    return default(T);
                }
            }
            else
            {
                result = m_CacheStack.Pop();
            }

            result.cacheFlag = false;
            return result;
        }

        public bool Recycle(T t)
        {
            if (t == null || t.cacheFlag)
            {
                return false;
            }

            if (m_CacheStack == null)
            {
                m_CacheStack = new Stack<T>();
            }
            else if (m_maxCount > 0)
            {
                if (m_CacheStack.Count >= m_maxCount)
                {
                    t.OnCacheReset();
                    return false;
                }
            }

            t.cacheFlag = true;
            t.OnCacheReset();
            m_CacheStack.Push(t);
            return true; 
        }

    }
}