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

            var model = ClassificationLibrary.create_model(Dimensions);

            var expectedSigns = spheres.Select(sp => sp.transform.position.y < 0 ? -1 : 1);
            var inputs = new List<double>();
            foreach (var sphere in spheres)
            {
                inputs.Add(sphere.transform.position.x);
                inputs.Add(sphere.transform.position.z);
            }

            ClassificationLibrary.train_model_linear_regression(model, inputs.ToArray(), Dimensions, spheres.Length, expectedSigns.ToArray());

            foreach (var sphere in spheresPlan)
            {
                var position = sphere.transform.position;
                var point = new double[] { position.x, position.z };
                var newY = ClassificationLibrary.predict(model, point, Dimensions);
                sphere.transform.position = new Vector3(position.x, newY, position.z);
            }

            ClassificationLibrary.release_model(model);
        }
    }
}
