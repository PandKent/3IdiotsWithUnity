using UnityEngine;

namespace SnakeExample
{
    public class SnakeNode1:Node1
    {
        private int m_id;
        private string m_name;
        private Material m_material;

        public int ID
        {
            get { return ID; }
            set { m_id = value; }
        }
        
        public string Name
        {
            get { return m_name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }

                this.name = value;
                m_name = value;
            }
        }

        public Transform Transform
        {
            get { return this.transform; }
        }

        public void SetPos(Vector3 vec)
        {
            this.transform.position = vec;
        }

        public void SetRotation(Quaternion a)
        {
            this.transform.localRotation = a;
        }

        public Material GetMaterial()
        {
            return m_material;
        }

        public void SetMaterial(Material mat)
        {
            if (mat != null)
                m_material = mat;
        }
    }
}