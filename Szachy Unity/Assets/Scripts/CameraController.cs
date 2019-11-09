using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform whiteCamPos;
    public Transform blackCamPos;
    

	// Use this for initialization
	void Start () {
        Client client = GameObject.FindObjectOfType<Client>();
        if (client.isHost)
        {
            transform.position = whiteCamPos.position;
            transform.rotation = whiteCamPos.rotation;
        }
        else
        {
            transform.position = blackCamPos.position;
            transform.rotation = blackCamPos.rotation;
        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
