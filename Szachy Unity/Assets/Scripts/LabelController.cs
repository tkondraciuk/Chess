using System.Collections;
using System.Collections.Generic;
using Szachy;
using UnityEngine;
using UnityEngine.UI;
using ChessColor = Szachy.Color;

public class LabelController : MonoBehaviour {
    Text text;
    

    King KingInCheck { get { return MainController.KingInCheck; } }
    King KingInMate { get { return MainController.KingInMate; } }
    


	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {
        text.text = MainController.Turn == ChessColor.Black ? "Ruch Czarnych" : "Ruch Białych";

		if(KingInCheck!=null)
        {
            text.text += " Szach!";
        }

        if (KingInMate!=null)
        {
            text.text = "Szach Mat!";
        }
        
	}

    
}
