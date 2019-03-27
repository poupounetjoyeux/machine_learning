using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
    public class LinearRegression : MonoBehaviour
    {
        public const int Dimensions = 2;

        private void Start()
        {
            var spheresPlan = GameObject.FindGameObjectsWithTag("plan");
            var spheres = GameObject.FindGameObjectsWithTag("sphere");

            Debug.Log($"Sphere number : {spheres.Length}");
            Debug.Log($"PlanSphere number : {spheresPlan.Length}");
            Debug.Log("Starting to call library for a LinearRegression");

            var model = ClassificationLibrary.createModel(Dimensions);

            var expectedSigns = spheres.Select(sp => (double)sp.transform.position.y).ToArray();
            var inputs = new List<double>();
            foreach (var sphere in spheres)
            {
                inputs.Add(sphere.transform.position.x);
                inputs.Add(sphere.transform.position.z);
            }

            ClassificationLibrary.trainModelLinearRegression(model, inputs.ToArray(), Dimensions, spheres.Length, expectedSigns);

            foreach (var sphere in spheresPlan)
            {
                var position = sphere.transform.position;
                var point = new double[] { position.x, position.z };
                var newY = ClassificationLibrary.predictRegressionModel(model, point, Dimensions);
                sphere.transform.position = new Vector3(position.x, (float)newY, position.z);
            }

            ClassificationLibrary.releaseModel(model);
        }
    }
}
