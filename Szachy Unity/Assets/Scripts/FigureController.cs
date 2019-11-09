using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Szachy;
using ChessColor = Szachy.Color;
using Color = UnityEngine.Color;


public class FigureController : MonoBehaviour {

    public static FigureController Instance { get; set; }

    const float whiteBrightnessFactor= 1.3f;
    const float blackBrightnessFactor = 2.3f;
    readonly Color SelectedColor = Color.yellow;
    readonly Color KingInCheckColor = Color.red;
    readonly Color WhiteColor = new Color(1, 1, 1);
    readonly Color BlackColor = new Color(0.118f, 0.105f, 0.105f);

    public ChessColor color;
    public FiguresEnum figureType;

    Figure figure;
    float brightnessFactor;
    Color defaultColor;
    BoardController boardControl;
    Vector3 targetPosition;
    MeshRenderer MeshRenderer;
    private Color currentColor;

    Board Board
    {
        get { return MainController.Board; }
    }
    King KingInCheck { get { return Board.KingInCheck; } }
    ChessColor Turn { get { return MainController.Turn; } }

    internal Figure Figure
    {
        get
        {
            return figure;
        }

        set
        {
            figure = value;
        }
    }
    
    // Use this for initialization
    void Start() {
        Instance = this;
        boardControl = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardController>();
        Position p = GetPosition();
        targetPosition = transform.position;
        Figure=Board.AddFigure(figureType, p, color);
        defaultColor = figure.Color == ChessColor.White ? WhiteColor : BlackColor;
        if (color == ChessColor.Black) { brightnessFactor = blackBrightnessFactor; }
        else { brightnessFactor = whiteBrightnessFactor; }
        MeshRenderer = GetComponent<MeshRenderer>();
        currentColor = defaultColor;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position != targetPosition)
        {
            transform.position = targetPosition;
        }
        if (KingInCheck == figure) currentColor = KingInCheckColor;
        else if (currentColor == KingInCheckColor) setDefaultColor();

        MeshRenderer.material.SetColor("_Color", currentColor);

        if (GetPosition() == "H7")
        {

        }

    }

    private void OnMouseEnter()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        currentColor *= brightnessFactor;
    }

    private void OnMouseExit()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        currentColor /= brightnessFactor;
    }

    private void OnMouseDown()
    {
        bool isWhite = FindObjectOfType<Client>().isHost;
        if (Turn == Figure.Color && isWhite == (Figure.Color == ChessColor.White))
        {
            Debug.Log(MeshRenderer.material.color);

            GameObject[] FigureObjects = GameObject.FindGameObjectsWithTag("Figure");
            foreach (var figure in FigureObjects)
            {
                figure.GetComponent<FigureController>().setDefaultColor();
            }
            MainController.SelectedFigure = Figure;
            markPossibleMoves();
            currentColor = SelectedColor;
            
        }
        else
        {
            GameObject marker = GameObject.FindGameObjectsWithTag("Marker")
                .Where(m => m.transform.position.Equals(transform.position))
                .FirstOrDefault();

            if(marker!=null)    marker.GetComponent<MarkerController>().ExecuteOnMouseDown();
        }

    }

    public Position GetPosition()
    {
        var unityCords = MainController.UnityCords;
        return unityCords.Keys
            .Where(k => unityCords[k].Equals(transform.position))
            .FirstOrDefault();
    }

    internal Figure GetFigure(Position p)
    {
        Figure Figure = Board.GetFigure(p);
        Debug.Log("Łańcuch: " + Figure.Position.ToString() + " =? " + p.ToString());
        Debug.Log("Wybrano figurę: " + Figure);
        return Figure;
    }

    public void setDefaultColor()
    {
        currentColor = defaultColor;
    }

    private void markPossibleMoves()
    {
        boardControl.destroyAllMarkers();
        var possibleMoves = Figure.getPossibleMoves();
        Figure.SavePossibleMoves(possibleMoves);
        foreach (var move in possibleMoves)
        {
            if (move.GetType().Name == "Move") boardControl.putMoveMarker(move.Destination);
            if (move is Attack) boardControl.putAttackMarker(move.Destination);
            if (move is Castling) boardControl.putSpecialMarker(move.Destination);
        }
        
    }

    public void SetTargetPosition(Position targetPosition)
    {
        SetTargetPosition(Position.GetTransform(targetPosition));
    }
    public void SetTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
