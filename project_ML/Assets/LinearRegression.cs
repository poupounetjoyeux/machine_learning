using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
    public enum CustomModeRegression
    {
        Default,
        Circle,
    }
    
    public class LinearRegression : MonoBehaviour
    {
        private int _dimensions = 2;
        private float _centerZ;
        private float _centerX;

        [SerializeField]
        private CustomModeRegression _mode = CustomModeRegression.Default;

        public CustomModeRegression Mode
        {
            get => _mode;
            set => _mode = value;
        }
        
        private void Start()
        {
            var spheresPlan = GameObject.FindGameObjectsWithTag("plan");
            var spheres = GameObject.FindGameObjectsWithTag("sphere");

            Debug.Log($"Sphere number : {spheres.Length}");
            Debug.Log($"PlanSphere number : {spheresPlan.Length}");
            Debug.Log("Starting to call library for a LinearRegression");

            var model = ClassificationLibrary.createModel(_dimensions);

            if (_mode == CustomModeRegression.Circle)
            {
                var allPointsWith1 = spheres.Where(sp => sp.transform.position.y > 0).ToList();
                float totalX = 0, totalZ = 0;
                foreach (var p in allPointsWith1)
                {
                    var position = p.transform.position;
                    totalX += position.x;
                    totalZ += position.z;
                }
                _centerX = totalX / allPointsWith1.Count;
                _centerZ = totalZ / allPointsWith1.Count;
            }
            
            Debug.Log("Found center at X = "+_centerX+" | Z = "+_centerZ);
            
            var expectedSigns = spheres.Select(sp => (double)sp.transform.position.y).ToArray();
            var inputs = new List<double>();
            foreach (var sphere in spheres)
            {
                var position = sphere.transform.position;
                inputs.Add(MapPositionX(position.x));
                inputs.Add(MapPositionZ(position.z));   
            }

            ClassificationLibrary.trainModelLinearRegression(model, inputs.ToArray(), _dimensions, spheres.Length, expectedSigns);

            foreach (var sphere in spheresPlan)
            {
                var position = sphere.transform.position;
                var point = new[] {MapPositionX(position.x), MapPositionZ(position.z)};
                
                var newY = ClassificationLibrary.predictRegressionModel(model, point, _dimensions);
                sphere.transform.position = new Vector3(position.x, (float)newY, position.z);
            }

            ClassificationLibrary.releaseModel(model);
        }
        
        private double MapPositionX(double x)
        {
            switch (_mode)
            {
                case CustomModeRegression.Default:
                    return x;
                case CustomModeRegression.Circle:
                    return Math.Pow(x - _centerX, 2);
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
        
        private double MapPositionZ(double z)
        {
            switch (_mode)
            {
                case CustomModeRegression.Default:
                    return z;
                case CustomModeRegression.Circle:
                    return Math.Pow(z - _centerZ, 2);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
