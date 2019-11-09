using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using Szachy;
using UnityEngine;

public class Client : MonoBehaviour
{
    public static Client Instance { get; set; }
    public string clientName;
    public bool isHost;


    private bool socketReady;
    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;

    private List<GameClient> players = new List<GameClient>();

    Position fromPosition;
    Position toPosition;
    Position fromCastlePosition;
    Position toCastlePosition;

    Figure selectedFigure;
    Rook selectedCastleFigure;
    Figure targetFigure;

    Move move;
    Attack attack;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public bool ConnectToServer(string host, int port)
    {
        if (socketReady)
            return false;

        try
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);

            socketReady = true;
        }
        catch(Exception e)
        {
            Debug.Log("Socket error: " + e.Message);
        }

        return socketReady;
    }
    private void Update()
    {
        if(socketReady)
        {
            if(stream.DataAvailable)
            {
                string data = reader.ReadLine();
                if (data != null)
                    OnIncomingData(data);
            }
        }
    }
    //Sending messages to the server
    public void Send(string data)
    {
        if (!socketReady)
            return;

        writer.WriteLine(data);
        writer.Flush();
    }

    //Read messages from the server
    private void OnIncomingData(string data)
    {
        Debug.Log("Client:" + data);
        string[] aData = data.Split('|');

        switch(aData[0])
        {
            case "SWHO":
                for(int i=1;i<aData.Length-1;i++)
                {
                    UserConnected(aData[i], false, false);
                }
                Send("CWHO|" + clientName + "|" + ((isHost) ? 1 : 0).ToString());
                break;
            case "SCNN":
                UserConnected(aData[1], false, false);
                break;
            case "SMOV":
                fromPosition = aData[1];
                toPosition = aData[2];
                selectedFigure = FigureController.Instance.GetFigure(fromPosition);
                Debug.Log("Wybrano figurę o wsp: " + selectedFigure.Position);
                move = new Move(toPosition, selectedFigure);
                move.ExecuteMovement();
                MainController.NextTurn();
                Debug.Log("Przemieszczono na: " + toPosition);
                break;
            case "SCMOV":
                fromPosition = aData[1];
                toPosition = aData[2];
                fromCastlePosition = aData[3];
                toCastlePosition = aData[4];
                selectedFigure = FigureController.Instance.GetFigure(fromPosition);
                Debug.Log("Wybrano Króla o wsp: " + selectedFigure.Position);
                selectedCastleFigure = FigureController.Instance.GetFigure(fromCastlePosition) as Rook;
                Debug.Log("Wybrano Wieżę o wsp: " + selectedCastleFigure.Position);
                move = new Move(toPosition, selectedFigure);
                move.ExecuteMovement();
                move = new Move(toCastlePosition, selectedCastleFigure);
                move.ExecuteMovement();
                MainController.NextTurn();
                Debug.Log("Przemieszczono na: " + toCastlePosition);
                break;
            case "SAMOV":
                fromPosition = aData[1];
                toPosition = aData[2];
                selectedFigure = FigureController.Instance.GetFigure(fromPosition);
                targetFigure = FigureController.Instance.GetFigure(toPosition);
                Debug.Log("Wybrano figurę o wsp: " + selectedFigure.Position);
                attack = new Attack(aData[2], selectedFigure, targetFigure);
                attack.ExecuteMovement();
                MainController.NextTurn();
                Debug.Log("Przemieszczono na: " + toPosition);
                break;
        }
    }

    private void UserConnected(string name,  bool host, bool isTurn)
    {
        GameClient c = new GameClient();
        c.name = name;
        c.isHost = isHost;
        players.Add(c);
        if (players.Count == 2)
            GameManager.Instance.StartGame();
    }

    private void OnApplicationQuit()
    {
        CloseSocket();
    }
    private void OnDisable()
    {
        CloseSocket();
    }
    private void CloseSocket()
    {
        if (!socketReady)
            return;

        writer.Close();
        reader.Close();
        socket.Close();
        socketReady = false;
    }

  
}



public class GameClient
{
    public string name;
    public bool isHost;
}