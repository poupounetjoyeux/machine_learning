using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
    public class LinearClassification : MonoBehaviour
    {
        public const int Dimensions = 2;

        [SerializeField]
        private int _numberOfIterations = 10000;

        public int NumberOfIterations
        {
            get => _numberOfIterations;
            set => _numberOfIterations = value;
        }

        [SerializeField]
        private double _learnStep = 0.001;

        private GameObject[] spheresPlan;
        private GameObject[] spheres;
        private List<double> inputs;
        private IntPtr model;
        private IEnumerable<int> expectedSigns;

        public double LearnStep
        {
            get => _learnStep;
            set => _learnStep = value;
        }

        private void Start()
        {
            spheresPlan = GameObject.FindGameObjectsWithTag("plan");
            spheres = GameObject.FindGameObjectsWithTag("sphere");

            Debug.Log($"Sphere number : {spheres.Length}");
            Debug.Log($"PlanSphere number : {spheresPlan.Length}");
            Debug.Log("Starting to call library for a LinearClassification");

            model = ClassificationLibrary.createModel(Dimensions);

            expectedSigns = spheres.Select(sp => sp.transform.position.y < 0 ? -1 : 1);
            inputs = new List<double>();
            foreach (var sphere in spheres)
            {
                inputs.Add(sphere.transform.position.x);
                inputs.Add(sphere.transform.position.z);
            }
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                ClassificationLibrary.trainModelLinearClassification(model, inputs.ToArray(), Dimensions, spheres.Length, expectedSigns.ToArray(), LearnStep, 1);

                foreach (var sphere in spheresPlan)
                {
                    var position = sphere.transform.position;
                    var point = new double[] {position.x, position.z};
                    var newY = ClassificationLibrary.predictClassificationModel(model, point, Dimensions);
                    sphere.transform.position = new Vector3(position.x, newY, position.z);
                }
            }
        }

        private void OnApplicationQuit()
        {
            ClassificationLibrary.releaseModel(model);
        }
    }
}
