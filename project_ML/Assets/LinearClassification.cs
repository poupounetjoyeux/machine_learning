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

        public double LearnStep
        {
            get => _learnStep;
            set => _learnStep = value;
        }
        
        private void Start()
        {
            var spheresPlan = GameObject.FindGameObjectsWithTag("plan");
            var spheres = GameObject.FindGameObjectsWithTag("sphere");

            Debug.Log($"Sphere number : {spheres.Length}");
            Debug.Log($"PlanSphere number : {spheresPlan.Length}");
            Debug.Log("Starting to call library for a LinearClassification");

            var model = ClassificationLibrary.createModel(Dimensions);

            var expectedSigns = spheres.Select(sp => sp.transform.position.y < 0 ? -1 : 1);
            var inputs = new List<double>();
            foreach (var sphere in spheres)
            {
                inputs.Add(sphere.transform.position.x);
                inputs.Add(sphere.transform.position.z);
            }

            ClassificationLibrary.trainModelLinearClassification(model, inputs.ToArray(), Dimensions, spheres.Length, expectedSigns.ToArray(), LearnStep, NumberOfIterations);

            foreach (var sphere in spheresPlan)
            {
                var position = sphere.transform.position;
                var point = new double[] {position.x, position.z};
                var newY = ClassificationLibrary.predictClassificationModel(model, point, Dimensions);
                sphere.transform.position = new Vector3(position.x, newY, position.z);
            }

            ClassificationLibrary.releaseModel(model);
        }
    }
}
