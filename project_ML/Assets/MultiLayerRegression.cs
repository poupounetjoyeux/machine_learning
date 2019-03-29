using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets
{
    public class MultiLayerRegression : MonoBehaviour
    {
        private GameObject[] _spheresPlan;
        private GameObject[] _spheres;
        private List<double> _inputs;
        private IntPtr? _model;
        private double[] _expectedSigns;

        [SerializeField]
        private MultilayerMode _mode = MultilayerMode.yPosition;

        public MultilayerMode Mode
        {
            get => _mode;
            set => _mode = value;
        }
        
        [SerializeField] private int[] _nplParams;

        public int[] NplParams
        {
            get => _nplParams;
            set => _nplParams = value;
        }

        [SerializeField]
        private int _iterations = 1;
        
        public int Iterations
        {
            get => _iterations;
            set => _iterations = value;
        }

        [SerializeField]
        private double _learnStep = 0.001;

        public double LearnStep
        {
            get => _learnStep;
            set => _learnStep = value;
        }

        private void Start()
        {
            if (NplParams.Length == 0)
            {
                Debug.LogError("You need to have at least 2 layers in you NPL Params");
                return;
            }
            _spheresPlan = GameObject.FindGameObjectsWithTag("plan");
            _spheres = GameObject.FindGameObjectsWithTag("sphere");

            Debug.Log($"Sphere number : {_spheres.Length}");
            Debug.Log($"PlanSphere number : {_spheresPlan.Length}");
            Debug.Log("Starting to call library for a MultilayerLinearClassification");

            _model = ClassificationLibrary.createMultilayerModel(NplParams, NplParams.Length, LearnStep);

            _inputs = new List<double>();
            var expectedSignsList = new List<double>();
            
            if (Mode == MultilayerMode.yPosition)
            {
                expectedSignsList = _spheres.Select(sp => (double) sp.transform.position.y).ToList();    
            }

            foreach (var sphere in _spheres)
            {
                
                _inputs.Add(sphere.transform.position.x);
                _inputs.Add(sphere.transform.position.z);
                if (Mode == MultilayerMode.Color)
                {
                    var color = sphere.GetComponent<Renderer>().material.color;
                    expectedSignsList.Add(color.r);
                    expectedSignsList.Add(color.g);
                    expectedSignsList.Add(color.b);
                }
            }
            _expectedSigns = expectedSignsList.ToArray();
        }

        private void Update()
        {
            if (!Input.GetKey(KeyCode.Space) || !_model.HasValue)
            {
                return;
            }
            ClassificationLibrary.trainModelMultilayerRegression(_model.Value, _inputs.ToArray(), _spheres.Length, _expectedSigns, Iterations);

            foreach (var sphere in _spheresPlan)
            {
                var position = sphere.transform.position;
                var point = new double[] { position.x, position.z };
                var output = new double[NplParams[NplParams.Length - 1]];
                var result = ClassificationLibrary.predictMultilayerRegressionModel(_model.Value, point);
                Marshal.Copy(result, output, 0, NplParams[NplParams.Length - 1]);
                if (Mode == MultilayerMode.yPosition)
                {
                    sphere.transform.position = new Vector3(position.x, (float)output[0], position.z);    
                }
                else
                {
                    sphere.GetComponent<Renderer>().material.color =
                        new Color((float) output[0], (float) output[1], (float) output[2]);
                }
            }
        }

        private void OnApplicationQuit()
        {
            if (_model.HasValue)
            {
                ClassificationLibrary.releaseMultilayerModel(_model.Value);
            }
        }
    }
}
