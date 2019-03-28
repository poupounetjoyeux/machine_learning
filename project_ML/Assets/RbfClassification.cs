using UnityEngine;

namespace Assets
{
    public class RbfClassification : MonoBehaviour
    {
        [SerializeField]
        private double _gama = 0.01;

        public double Gama
        {
            get => _gama;
            set => _gama = value;
        }

        private void Start()
        {

        }
    }
}
