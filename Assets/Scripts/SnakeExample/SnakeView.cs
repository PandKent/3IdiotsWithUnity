using System;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeExample
{
    public class SnakeView:MonoBehaviour
    {
        [SerializeField]
        
        private SnakeNode1 m_head;
        
        private List<SnakeNode1> m_bodiesList = new List<SnakeNode1>();

        public SnakeNode1 Head
        {
            get { return m_head; }
            set { m_head = value; }
        }
        private void Awake()
        {
//            Head = this.gameObject.GetComponent<SnakeNode1>();
            SnakeCtrol.Instance.BindInstance(this);
        }

        public void ChangeMat(Material mat, int index = -1)
        {
            if (index == -1)
            {
                changeAllMat(mat);
            }
            else
            {
                if (m_bodiesList.Count > index && m_bodiesList.Count > 0)
                {
                    m_bodiesList[index].SetMaterial(mat);
                }
            }
        }

        private void changeAllMat(Material mat)
        {
            if(m_bodiesList.Count<=0)
                return;
            
            for (int i = 0; i < m_bodiesList.Count; i++)
            {
                m_bodiesList[i].SetMaterial(mat);
            }
        }
    }
}