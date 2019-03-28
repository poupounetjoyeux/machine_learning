using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
    public class LinearRegression : MonoBehaviour
    {
        public int dimensions = 2;

        public CustomMode mode = CustomMode.Default;
        private float centerZ;
        private float centerX;
        
        private void Start()
        {
            var spheresPlan = GameObject.FindGameObjectsWithTag("plan");
            var spheres = GameObject.FindGameObjectsWithTag("sphere");

            Debug.Log($"Sphere number : {spheres.Length}");
            Debug.Log($"PlanSphere number : {spheresPlan.Length}");
            Debug.Log("Starting to call library for a LinearRegression");

            var model = ClassificationLibrary.createModel(dimensions);

            if (mode == CustomMode.Circle)
            {
                var allPointsWith1 = spheres.Where(sp => sp.transform.position.y > 0).ToList();
                float totalX = 0, totalZ = 0;
                foreach (var p in allPointsWith1)
                {
                    var position = p.transform.position;
                    totalX += position.x;
                    totalZ += position.z;
                }
                centerX = totalX / allPointsWith1.Count();
                centerZ = totalZ / allPointsWith1.Count();
            }
            
            if (mode == CustomMode.Xor)
            {
                var allPointsWith1 = spheresPlan;
                float totalX = 0, totalZ = 0;
                foreach (var p in allPointsWith1)
                {
                    var position = p.transform.position;
                    totalX += position.x;
                    totalZ += position.z;
                }
                centerX = totalX / allPointsWith1.Count();
                centerZ = totalZ / allPointsWith1.Count();
            }
            
            Debug.Log("Found center at X = "+centerX+" | Z = "+centerZ);
            
            var expectedSigns = spheres.Select(sp => (double)sp.transform.position.y).ToArray();
            var inputs = new List<double>();
            foreach (var sphere in spheres)
            {
                var position = sphere.transform.position;
                if (mode != CustomMode.Xor)
                {
                    inputs.Add(mapPositionX(position.x));
                    inputs.Add(mapPositionZ(position.z));   
                }
                else
                {
                    inputs.Add(mapPositionX(position.x) * mapPositionZ(position.z));
                    Debug.Log("Checking for point X = "+mapPositionX(position.x)+" | Z = "+mapPositionZ(position.z));
                }
            }

            ClassificationLibrary.trainModelLinearRegression(model, inputs.ToArray(), dimensions, spheres.Length, expectedSigns);

            foreach (var sphere in spheresPlan)
            {
                var position = sphere.transform.position;
                double[] point;
                if (mode != CustomMode.Xor)
                {
                    point = new double[] {mapPositionX(position.x), mapPositionZ(position.z)};    
                }
                else
                {
                    point = new double[] {mapPositionX(position.x) * mapPositionZ(position.z)};
                }
                
                var newY = ClassificationLibrary.predictRegressionModel(model, point, dimensions);
                sphere.transform.position = new Vector3(position.x, (float)newY, position.z);
            }

            ClassificationLibrary.releaseModel(model);
        }
        
        private double mapPositionX(double x)
        {
            switch (mode)
            {
                case CustomMode.Xor:
                    return x - centerX;
                case CustomMode.Default:
                    return x;
                case CustomMode.Circle:
                    return Math.Pow(x - centerX, 2);
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
        
        private double mapPositionZ(double z)
        {
            switch (mode)
            {
                case CustomMode.Xor:
                    return z - centerZ;
                case CustomMode.Default:
                    return z;
                case CustomMode.Circle:
                    return Math.Pow(z - centerZ, 2);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
