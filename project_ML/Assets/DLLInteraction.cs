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
            var spheresPlan = GameObject.FindGameObjectsWithTag("plan");
            var spheres = GameObject.FindGameObjectsWithTag("sphere");
            var defaultModel = ClassificationLibrary.create_model(2);

            var expectedSigns = spheres.Select(sp => sp.transform.position.y < 0 ? -1 : 1);
            var inputs = new List<double>();
            foreach (var sphere in spheres)
            {
                inputs.Add(sphere.transform.position.x);
                inputs.Add(sphere.transform.position.z);
            }

            ClassificationLibrary.train_model(defaultModel, inputs.ToArray(), 2, spheres.Length, expectedSigns.ToArray(), 0.001, 100);
        }

        // Update is called once per frame
        private void Update()
        {
        
        }
    }
}
