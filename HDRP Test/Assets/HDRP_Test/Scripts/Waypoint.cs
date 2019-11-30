using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.AllPlayers[0].GetObject();
    }

    void Update()
    {
        if(player == null && Player.AllPlayers.Count > 0)
        {
            player = Player.AllPlayers[0].GetObject();
        }
        else if (Vector3.Distance(player.transform.position, this.transform.position) < 1)
        {
            Player.AllPlayers[0].AdvanceLevel();
            Player.AllPlayers[0].AdvanceLevel();
            UIManager.PlayerStateUI++;
            this.gameObject.SetActive(false);
        }
    }
}
