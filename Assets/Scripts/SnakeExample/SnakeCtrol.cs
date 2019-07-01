using System;
using Framework.Singleton;
using UnityEngine;

namespace SnakeExample
{
    public class SnakeCtrol:TSingleton<SnakeCtrol>
    {
        private SnakeView m_snake;
        private Quaternion m_rotationValue = Quaternion.Euler(new Vector3(0, 0, 0));
        
        public void BindInstance(SnakeView snakeView)
        {
            if(snakeView!=null)
                m_snake = snakeView;
        }


        private Material mat = null;
        private void OnGUI()
        {
            
            if (GUILayout.Button("ChangeColor"))
            {
                m_snake.ChangeMat(mat);
            }
        }

        private void Update()
        {
            if (m_snake==null&&m_snake.Head==null)
                return; 
            SnakeAutoMove();
            if (Input.GetKey(KeyCode.W))
            {
                x = 0; y = step;
                m_rotationValue = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            if (Input.GetKey(KeyCode.S))
            {
                x = 0; y = -step;
                m_rotationValue = Quaternion.Euler(new Vector3(0, 0, 180));
            }
            if (Input.GetKey(KeyCode.A))
            {
                x = -step; y = 0;
                m_rotationValue = Quaternion.Euler(new Vector3(0, 0, 90));
            }
            if (Input.GetKey(KeyCode.D))
            {
                x = step; y = 0;
                m_rotationValue = Quaternion.Euler(new Vector3(0, 0, 270));
            }
            
            m_snake.Head.SetRotation(m_rotationValue);
            Move();
//            SnakeHedaMove();
        }

        private void SnakeAutoMove()
        {
            m_snake.Head.Transform.localPosition += new Vector3(1f,0,0);
        }

        private float step = 0.5f;
        private float x;
        private float y;
        private void SnakeHedaMove()
        {
            
        }
        
        void Move()
        {    
            m_snake.Head.Transform.localPosition  = Vector3.Lerp(m_snake.Head.Transform.localPosition, m_snake.Head.Transform.localPosition + new Vector3(x, y, 0), 5  * Time.deltaTime);
        }
    }
}