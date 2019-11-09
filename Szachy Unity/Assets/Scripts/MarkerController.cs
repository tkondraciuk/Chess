using System;
using System.Collections;
using System.Collections.Generic;
using Szachy;
using UnityEngine;
using Color = UnityEngine.Color;
using ChessColor = Szachy.Color;
using System.Linq;

public class MarkerController : MonoBehaviour {
    
    Color defaultColor;
    Color mouseEnteredColor;
    const float brightnessFactor = 2f;
    BoardController boardController;

    Figure SelectedFigure {
        get { return MainController.SelectedFigure; }
        set { MainController.SelectedFigure = value; }
    }
    Board Board { get { return MainController.Board; } }

	// Use this for initialization
	void Start () {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        defaultColor = mr.material.color;
        mouseEnteredColor = defaultColor * brightnessFactor;
        boardController = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        ChangeColor(mouseEnteredColor);
    }

    private void OnMouseExit()
    {
        ChangeColor(defaultColor);
    }

    private void OnMouseDown()
    {
        Move move = SelectedFigure.PossibleMoves
            .Where(m => Position.GetTransform(m.Destination).Equals(transform.position))
            .FirstOrDefault();
        if(move!=null)   move.ReadMovement();
        //MainController.NextTurn();
        Board.isCheck();
        GameObject.FindGameObjectsWithTag("Figure").ToList().ForEach(go => go.GetComponent<FigureController>().setDefaultColor());
        boardController.destroyAllMarkers();
    }

    private void ChangeColor(Color color)
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.material.color = color;
    }

    public void ExecuteOnMouseDown()
    {
        OnMouseDown();
    }
}
