using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
   
    public class LinearClassification : MonoBehaviour
    {
        public int dimensions = 2;
        
        private GameObject[] _spheresPlan;
        private GameObject[] _spheres;
        private List<double> _inputs;
        private IntPtr _model;
        private IEnumerable<int> _expectedSigns;

        [SerializeField]
        private int _numberOfIterations = 1;

        public int NumberOfIterations
        {
            get => _numberOfIterations;
            set => _numberOfIterations = value;
        }

        [SerializeField]
        private double _learnStep = 0.001;

        public CustomMode mode = CustomMode.Default;
        private float centerZ;
        private float centerX;

        public double LearnStep
        {
            get => _learnStep;
            set => _learnStep = value;
        }

        private void Start()
        {
            _spheresPlan = GameObject.FindGameObjectsWithTag("plan");
            _spheres = GameObject.FindGameObjectsWithTag("sphere");

            Debug.Log($"Sphere number : {_spheres.Length}");
            Debug.Log($"PlanSphere number : {_spheresPlan.Length}");
            Debug.Log("Starting to call library for a LinearClassification");

            _model = ClassificationLibrary.createModel(dimensions);

            if (mode == CustomMode.Circle)
            {
                var allPointsWith1 = _spheres.Where(sp => sp.transform.position.y > 0).ToList();
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
                var allPointsWith1 = _spheresPlan;
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
            
            _expectedSigns = _spheres.Select(sp => sp.transform.position.y < 0 ? -1 : 1);
            _inputs = new List<double>();
            foreach (var sphere in _spheres)
            {
                var position = sphere.transform.position;
                if (mode != CustomMode.Xor)
                {
                    _inputs.Add(mapPositionX(position.x));
                    _inputs.Add(mapPositionZ(position.z));   
                }
                else
                {
                    _inputs.Add(mapPositionX(position.x) * mapPositionZ(position.z));
                }
            }
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

        private void Update()
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                return;
            }
            ClassificationLibrary.trainModelLinearClassification(_model, _inputs.ToArray(), dimensions, _spheres.Length, _expectedSigns.ToArray(), LearnStep, NumberOfIterations);

            foreach (var sphere in _spheresPlan)
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

                var newY = ClassificationLibrary.predictClassificationModel(_model, point, dimensions);
                sphere.transform.position = new Vector3(position.x, newY, position.z);
            }
        }

        private void OnApplicationQuit()
        {
            ClassificationLibrary.releaseModel(_model);
        }
    }
}
