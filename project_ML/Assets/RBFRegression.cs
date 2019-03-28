using UnityEngine;

namespace Assets
{
    public class RbfRegression : MonoBehaviour
    {
        [SerializeField]
        private double _gama = 0.01;

        public double Gama
        {
            get => _gama;
            set => _gama = value;
        }


    }
}
