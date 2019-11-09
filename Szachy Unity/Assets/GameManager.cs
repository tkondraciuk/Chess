using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { set; get; }
    public GameObject mainMenu;
    public GameObject serverMenu;
    public GameObject connectMenu;
    public GameObject waitingMenu;

    public GameObject serverPrefab;
    public GameObject clientPrefab;

    public InputField nameInput;
    private void Start()
    {
        Instance = this;
        serverMenu.SetActive(false);
        connectMenu.SetActive(false);
        waitingMenu.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    public void ConnectButton()
    {
        mainMenu.SetActive(false);
        connectMenu.SetActive(true);
    }
    public void HostButton()
    {
        mainMenu.SetActive(false);
        serverMenu.SetActive(true);
    }

    public void ConnectToServerButton()
    {
        try
        {
            string hostAddress = GameObject.Find("HostInput")
                .GetComponent<InputField>().text;
            if (hostAddress.Length == 0)
                hostAddress = "127.0.0.1";
            Debug.Log("Wybrano adres: " + hostAddress);
            string portText = GameObject.Find("ClientPortInput")
                .GetComponent<InputField>().text;
            if (portText.Length == 0)
                portText = "1";
            int port = Convert.ToInt32(portText);
            if (port <= 0)
                port = 1;
            if (port > 65535)
                port = 65535;
            Debug.Log("Wybrano port: " + port.ToString());
            Client c = Instantiate(clientPrefab)
                .GetComponent<Client>();
            c.clientName = nameInput.text;
            c.isHost = false;
            if (c.clientName.Length == 0)
                c.clientName = "Client";
            c.ConnectToServer(hostAddress, port);
            connectMenu.SetActive(false);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public void BackButton()
    {
        mainMenu.SetActive(true);
        serverMenu.SetActive(false);
        connectMenu.SetActive(false);
        waitingMenu.SetActive(false);

        Server s = FindObjectOfType<Server>();
        if (s != null)
            Destroy(s.gameObject);

        Client c = FindObjectOfType<Client>();
        if (c != null)
            Destroy(c.gameObject);
    }
    public void StartHostButton()
    {
        try
        {
            Server s = Instantiate(serverPrefab)
                .GetComponent<Server>();

            Dropdown dropdownList = GameObject.Find("ServerListDropdown")
                .GetComponent<Dropdown>();
            string hostAddress = dropdownList.options[dropdownList.value].text;
            Debug.Log("Wybrano adres: " + hostAddress);

            string portText = GameObject.Find("HostPortInput")
                .GetComponent<InputField>().text;
            if (portText.Length == 0)
                portText = "1";
            int port = Convert.ToInt32(portText);
            if (port <= 0)
                port = 1;
            if (port > 65535)
                port = 65535;
            Debug.Log("Wybrano port: " + port);
            Client c = Instantiate(clientPrefab)
                .GetComponent<Client>();
            c.clientName = nameInput.text;
            c.isHost = true;
            if (c.clientName.Length == 0)
                c.clientName = "Host";
            s.Init(hostAddress, port);
            c.ConnectToServer(hostAddress, port);
            serverMenu.SetActive(false);
            waitingMenu.SetActive(true);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("test");
    }
}
