using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
    public class DllInteraction : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            const int dimensions = 2;
            var spheresPlan = GameObject.FindGameObjectsWithTag("plan");
            var spheres = GameObject.FindGameObjectsWithTag("sphere");

            Debug.Log($"Sphere number : {spheres.Length}");
            Debug.Log($"PlanSphere number : {spheresPlan.Length}");

            var model = ClassificationLibrary.create_model(dimensions);

            var expectedSigns = spheres.Select(sp => sp.transform.position.y < 0 ? -1 : 1);
            var inputs = new List<double>();
            foreach (var sphere in spheres)
            {
                inputs.Add(sphere.transform.position.x);
                inputs.Add(sphere.transform.position.z);
            }

            ClassificationLibrary.train_model(model, inputs.ToArray(), 2, spheres.Length, expectedSigns.ToArray(), 0.001, 100);

            foreach (var sphere in spheresPlan)
            {
                var position = sphere.transform.position;
                var point = new double[] {position.x, position.z};
                var newY = ClassificationLibrary.predict(model, point, dimensions);
                sphere.transform.position = new Vector3(position.x, newY, position.z);
            }

            ClassificationLibrary.release_model(model);
        }

        // Update is called once per frame
        private void Update()
        {
        
        }
    }
}
