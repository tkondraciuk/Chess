using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Szachy;
using Color = UnityEngine.Color;
using ChessColor = Szachy.Color;

public class TurnMarkerController : MonoBehaviour {

    private ChessColor Turn
    {
        get { return MainController.Turn; }
    }
    private CanvasRenderer canvasRenderer;
    private Color Color
    {
        get { return canvasRenderer.GetColor(); }
        set { canvasRenderer.SetColor(value); }
    }

    // Use this for initialization
    void Start () {
        canvasRenderer = GetComponentInChildren<CanvasRenderer>();
	}

    // Update is called once per frame
    void Update()
    {
        switch (Turn)
        {
            case ChessColor.Black:
                Color = Color.black;
                break;
            case ChessColor.White:
                Color = Color.white;
                break;
            default:
                break;
        }
    }
}
