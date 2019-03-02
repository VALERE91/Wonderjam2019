using Assets.Network.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomReplicationManager : MonoBehaviour
{
    public GameObject PlayerPawn;

    public GameObject CookerSpawner1;
    public GameObject CookerSpawner2;

    public GameObject WaiterSpawner1;
    public GameObject WaiterSpawner2;   

    private GameObject m_myself;

    private Dictionary<string, GameObject> m_replica;

    public CustomNetworkManager m_networkManager;
    
    // Start is called before the first frame update
    void Start()
    {
        m_networkManager = FindObjectOfType<CustomNetworkManager>();

        if(m_networkManager == null || m_networkManager.MatchMakingResult == null)
        {
            Debug.LogError("You need an initialized CustomNetworkManager in your scene");
            return;
        }

        m_myself = SpawnCharacter(m_networkManager.MatchMakingResult.Role, m_networkManager.MatchMakingResult.TeamID, true);

        foreach (var player in m_networkManager.MatchMakingResult.Players)
        {
            if(player.Key == m_networkManager.MatchMakingResult.PlayerID)
            {
                continue;
            }

            m_replica.Add(player.Key, SpawnCharacter(player.Value.Role, player.Value.Team, false));
        }
    }

    private GameObject SpawnCharacter(PlayerRole role, uint team, bool isLocal)
    {
        GameObject obj = null;
        switch (team)
        {
            case 0:
                switch (role)
                {
                    case PlayerRole.Cooker:
                        obj = Instantiate(PlayerPawn, CookerSpawner1.transform.position, Quaternion.identity);
                        break;
                    case PlayerRole.Waiter:
                        obj = Instantiate(PlayerPawn, WaiterSpawner1.transform.position, Quaternion.identity);
                        break;
                }
                break;
            case 1:
                switch (role)
                {
                    case PlayerRole.Cooker:
                        obj = Instantiate(PlayerPawn, CookerSpawner2.transform.position, Quaternion.identity);
                        break;
                    case PlayerRole.Waiter:
                        obj = Instantiate(PlayerPawn, WaiterSpawner2.transform.position, Quaternion.identity);
                        break;
                }
                break;
        }

        if(isLocal)
        {
            var rend = obj.GetComponent<Renderer>();
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", Color.green);
        }

        return obj;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
