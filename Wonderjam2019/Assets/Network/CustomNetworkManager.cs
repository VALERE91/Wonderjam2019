using Assets.Network.Models;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;


public class CustomNetworkManager : MonoBehaviour
{
    private WebSocket m_ws;

    public MatchmakingResult MatchMakingResult;

    public string Pseudo;

    public string GameScene;
    public string LobbyScene;

    private bool m_displayDebugNetwork;

    // Start is called before the first frame update
    void Start()
    {
        MatchMakingResult = null;
        m_displayDebugNetwork = false;
        DontDestroyOnLoad(this);
    }

    private void OnWebSocketMessage(object sender, MessageEventArgs e)
    {
        if(e.IsText)
        {
            //Matchmaking or chat
            JamMessage m = JsonConvert.DeserializeObject<JamMessage>(e.Data);

            switch(m.Type)
            {
                case MessageType.MatchmakingResult:
                    MatchMakingResult = JsonConvert.DeserializeObject<MatchmakingResult>(e.Data);
                    break;
                case MessageType.TextChatMessage:
                    break;
            }
        }
        else if(e.IsBinary)
        {
            //Replication
        }
        else
        {
            //Ping
            Debug.Log(e.Data);
        }
    }

    private void OnWebSocketClose(object sender, CloseEventArgs e)
    {
        Debug.Log($"WS closed : {e.Reason}");
    }

    private void OnWebSocketError(object sender, ErrorEventArgs e)
    {
        Debug.Log($"Error in WS : {e.Message}");
    }

    private void OnWebSocketOpen(object sender, System.EventArgs e)
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F12))
        {
            m_displayDebugNetwork = !m_displayDebugNetwork;
        }

        if (MatchMakingResult != null && SceneManager.GetActiveScene().name == LobbyScene)
        {
            SceneManager.LoadScene(GameScene, LoadSceneMode.Single);
        }
    }

    public void DoMatchmaking(string pseudo)
    {
        if(m_ws != null)
        {
            m_ws.Close();
        }

        m_ws = new WebSocket($"ws://localhost:8080/wonderjam?pseudo={pseudo}");

        if (Debug.isDebugBuild)
        {
            m_ws.Log.Level = LogLevel.Debug;
            m_ws.Log.File = "wsLog.log";
        }

        m_ws.OnOpen += OnWebSocketOpen;
        m_ws.OnError += OnWebSocketError;
        m_ws.OnClose += OnWebSocketClose;
        m_ws.OnMessage += OnWebSocketMessage;

        m_ws.ConnectAsync();
    }

    void OnDestroy()
    {
        if(m_ws != null)
        {
            m_ws.Close();
        }
    }

    void OnGUI()
    {
        if(!m_displayDebugNetwork)
        {
            return;
        }

        // Wrap everything in the designated GUI Area
        GUILayout.BeginArea(new Rect(0, 0, 700, 200));
        // Begin the singular Horizontal Group
        GUILayout.BeginHorizontal();

        if (MatchMakingResult == null)
        {
            // Place a space between the button and the vertical area
            // so it fits the whole area
            GUILayout.FlexibleSpace();
            // Arrange two more Controls vertically beside the Button
            GUILayout.BeginVertical();
            Pseudo = GUILayout.TextField(Pseudo);
            if (GUILayout.Button("Do matchmaking") && m_ws == null)
            {
                DoMatchmaking(Pseudo);
            }
            GUILayout.EndVertical();
        }
        else
        {
            // Place a space between the button and the vertical area
            // so it fits the whole area
            GUILayout.FlexibleSpace();
            // Arrange two more Controls vertically beside the Button
            GUILayout.BeginVertical();

            GUILayout.Label($"SessionID : {MatchMakingResult.SessionID}");
            GUILayout.Label($"PlayerID : {MatchMakingResult.PlayerID}");
            GUILayout.EndVertical();

            // Place a space between the button and the vertical area
            // so it fits the whole area
            GUILayout.FlexibleSpace();
            // Arrange two more Controls vertically beside the Button
            GUILayout.BeginVertical();

            GUILayout.Label($"TeamID : {MatchMakingResult.TeamID}");
            GUILayout.Label($"Role : {((MatchMakingResult.Role == 0)?"Cooker":"Waiter")}");
            GUILayout.EndVertical();
        }

        GUILayout.EndArea();
    }
}
