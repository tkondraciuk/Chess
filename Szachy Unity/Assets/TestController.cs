using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestController : MonoBehaviour {


    private FigureController figureC;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		    figureC = GameObject.FindGameObjectsWithTag("Figure")
            .Select(f => f.GetComponent<FigureController>())
            .FirstOrDefault(f => f.GetPosition() == "H7");
	}
}
