using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
    public class MultiLayerRegression : MonoBehaviour
    {
        [SerializeField]
        private string _superParams = "2;1";

        public string SuperParams
        {
            get => _superParams;
            set => _superParams = value;
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
            var superParam = SuperParams.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                .ToArray();

            var spheresPlan = GameObject.FindGameObjectsWithTag("plan");
            var spheres = GameObject.FindGameObjectsWithTag("sphere");

            Debug.Log($"Sphere number : {spheres.Length}");
            Debug.Log($"PlanSphere number : {spheresPlan.Length}");
            Debug.Log("Starting to call library for a LinearClassification");

            var model = ClassificationLibrary.createMultilayerModel(superParam, superParam.Length, LearnStep);

            var expectedSigns = spheres.Select(sp => sp.transform.position.y < 0 ? -1 : 1);
            var inputs = new List<double>();
            foreach (var sphere in spheres)
            {
                inputs.Add(sphere.transform.position.x);
                inputs.Add(sphere.transform.position.z);
            }

            ClassificationLibrary.trainModelMultilayerRegression(model, inputs.ToArray(), spheres.Length, expectedSigns.ToArray(), Iterations);

            foreach (var sphere in spheresPlan)
            {
                var position = sphere.transform.position;
                var point = new double[] { position.x, position.z };
                var newY = ClassificationLibrary.predictMultilayerRegressionModel(model, point);
                sphere.transform.position = new Vector3(position.x, (float)newY, position.z);
            }

            ClassificationLibrary.releaseMultilayerModel(model);
        }
    }
}
