﻿using System;
using System.Collections;
using System.Collections.Generic;
using UdpKit;
using UnityEngine;
using UnityEngine.UI;
public class Menu : Bolt.GlobalEventListener
{
    public Button joinGameButtonPrefab;
    public GameObject serverListPanel;
    private List<Button> joinServerButtons = new List<Button>();
    public float  buttonsSpacing;
    ///HOST
    public void startServer()
    {
        BoltLauncher.StartServer();
    }
    //JOINING GAME
    public void startClient()
    {
        BoltLauncher.StartClient();
    }

    public override void BoltStartDone()
    {
        if (BoltNetwork.IsServer)
        {   
            int random = UnityEngine.Random.Range(0, 99);
            string matchName = "Testing" + random;
            BoltNetwork.SetServerInfo(matchName, null);
            BoltNetwork.LoadScene("Game");
        }
    }
    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        clearSessions();
        int totalSessions = 0;
        print("Total Sessions" + totalSessions);
        foreach(var session in sessionList)
        {
            UdpSession photonSession = session.Value as UdpSession;

            Button joinGameButtonClone = Instantiate(joinGameButtonPrefab);
            joinGameButtonClone.transform.parent = serverListPanel.transform;
            joinGameButtonClone.transform.localPosition = new Vector3(0, buttonsSpacing * joinServerButtons.Count, 0);
            joinGameButtonClone.gameObject.SetActive(true);
            joinGameButtonClone.onClick.AddListener(() => joinGame(photonSession));

            joinServerButtons.Add(joinGameButtonClone);
        }
    }
    private void joinGame(UdpSession photonsession)
    {
        BoltNetwork.Connect(photonsession);
    }
    
    private void clearSessions()
    {
        foreach(Button button in joinServerButtons)
        {
            Destroy(button.gameObject);
        }
        joinServerButtons.Clear();

    }
}
