using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
    public class LinearClassification : MonoBehaviour
    {
        public const int Dimensions = 2;
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

            _model = ClassificationLibrary.createModel(Dimensions);

            _expectedSigns = _spheres.Select(sp => sp.transform.position.y < 0 ? -1 : 1);
            _inputs = new List<double>();
            foreach (var sphere in _spheres)
            {
                _inputs.Add(sphere.transform.position.x);
                _inputs.Add(sphere.transform.position.z);
            }
        }

        private void Update()
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                return;
            }
            ClassificationLibrary.trainModelLinearClassification(_model, _inputs.ToArray(), Dimensions, _spheres.Length, _expectedSigns.ToArray(), LearnStep, NumberOfIterations);

            foreach (var sphere in _spheresPlan)
            {
                var position = sphere.transform.position;
                var point = new double[] {position.x, position.z};
                var newY = ClassificationLibrary.predictClassificationModel(_model, point, Dimensions);
                sphere.transform.position = new Vector3(position.x, newY, position.z);
            }
        }

        private void OnApplicationQuit()
        {
            ClassificationLibrary.releaseModel(_model);
        }
    }
}
