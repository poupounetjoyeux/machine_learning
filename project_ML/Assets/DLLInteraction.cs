using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLLInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var spheres = GameObject.FindGameObjectsWithTag("mySphere");
        var defaultModel = ClassificationLibrary.create_model(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
