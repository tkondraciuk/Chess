using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Szachy;
using System;

public class GameController : MonoBehaviour {

    public GameObject board;
    public GameObject figure;
    public GameObject BoardObject {
        get { return MainController.Board.BoardObject; }
        set { MainController.Board.BoardObject = value; }
    }
    
    // Use this for initialization
    void Awake () {
        MainController.InitializeUnityCords();
   
	}

    private void Start()
    {
             BoardObject = Instantiate(board);
             SetCamera();
    }

    private void SetCamera()
    {
        
    }

    // Update is called once per frame
    void Update () {
		
	}

   
    
}
