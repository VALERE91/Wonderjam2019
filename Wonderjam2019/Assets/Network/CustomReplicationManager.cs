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

    public CustomNetworkManager m_networkManager;
    
    // Start is called before the first frame update
    void Start()
    {
        m_networkManager = FindObjectOfType<CustomNetworkManager>();

        if(m_networkManager == null)
        {
            Debug.LogError("You need a CustomNetworkManager in your scene");
            return;
        }

        m_myself = SpawnCharacter(m_networkManager.MatchMakingResult.Role, m_networkManager.MatchMakingResult.TeamID, true);

        Instantiate(PlayerPawn, CookerSpawner2.transform.position, Quaternion.identity);
        Instantiate(PlayerPawn, WaiterSpawner1.transform.position, Quaternion.identity);
        Instantiate(PlayerPawn, WaiterSpawner2.transform.position, Quaternion.identity);
    }

    private GameObject SpawnCharacter(PlayerRole role, uint team, bool isLocal)
    {
        GameObject obj = null;
        switch (m_networkManager.MatchMakingResult.TeamID)
        {
            case 0:
                switch (m_networkManager.MatchMakingResult.Role)
                {
                    case Assets.Network.Models.PlayerRole.Cooker:
                        obj = Instantiate(PlayerPawn, CookerSpawner1.transform.position, Quaternion.identity);
                        break;
                    case Assets.Network.Models.PlayerRole.Waiter:
                        obj = Instantiate(PlayerPawn, WaiterSpawner1.transform.position, Quaternion.identity);
                        break;
                }
                break;
            case 1:
                switch (m_networkManager.MatchMakingResult.Role)
                {
                    case Assets.Network.Models.PlayerRole.Cooker:
                        obj = Instantiate(PlayerPawn, CookerSpawner2.transform.position, Quaternion.identity);
                        break;
                    case Assets.Network.Models.PlayerRole.Waiter:
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
