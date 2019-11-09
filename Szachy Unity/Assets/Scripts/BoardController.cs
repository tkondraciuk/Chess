using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Szachy;

public class BoardController : MonoBehaviour {
    public GameObject whiteRook;
    public GameObject blackRook;
    public GameObject whiteKnight;
    public GameObject blackKnight;
    public GameObject whiteBishop;
    public GameObject blackBishop;
    public GameObject whiteQueen;
    public GameObject blackQueen;
    public GameObject whiteKing;
    public GameObject blackKing;
    public GameObject whitePawn;
    public GameObject blackPawn;
    public GameObject MoveMarker;
    public GameObject AttackMarker;
    public GameObject SpecialMarker;

    private GameObject BoardObject
    {
        get { return MainController.Board.BoardObject; }
        set { MainController.Board.BoardObject = value; }
    }
    private Board Board
    {
        get { return MainController.Board; }
    }

    // Use this for initialization

    void Start () {
        CreateFiguresOnTheirStartPositions();
        
    }

   

    // Update is called once per frame
    void Update () {
		
	}
    private void CreateFiguresOnTheirStartPositions()
    {
        putFigure(whiteRook, "A1");
        putFigure(whiteKnight, "B1");
        putFigure(whiteBishop, "C1");
        putFigure(whiteQueen, "D1");
        putFigure(whiteKing, "E1");
        putFigure(whiteBishop, "F1");
        putFigure(whiteKnight, "G1");
        putFigure(whiteRook, "H1");

        putFigure(blackRook, "A8");
        putFigure(blackKnight, "B8");
        putFigure(blackBishop, "C8");
        putFigure(blackQueen, "D8");
        putFigure(blackKing, "E8");
        putFigure(blackBishop, "F8");
        putFigure(blackKnight, "G8");
        putFigure(blackRook, "H8");

        for (int i = 0; i < 8; i++)
        {
            Position whitePos = new Position(i, 1);
            Position blackPos = new Position(i, 6);
            putFigure(whitePawn, whitePos);
            putFigure(blackPawn, blackPos);
        }
    }

    private GameObject putFigure(GameObject figurePrefab, Position p)
    {
        GameObject figure = Instantiate(figurePrefab);
        figure.transform.position = Position.GetTransform(p);
        return figure;
    }

    public void putMoveMarker(Position p)
    {
        putFigure(MoveMarker, p);
    }

    public void putAttackMarker(Position p)
    {
        putFigure(AttackMarker, p);
    }

    public void putSpecialMarker(Position p)
    {
        putFigure(SpecialMarker, p);
    }

    public void destroyAllMarkers()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("Marker"))
        {
            GameObject.Destroy(item);
        }
    }

    
}
