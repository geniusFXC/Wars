using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFood : MonoBehaviour {
    private GamePanel gamePanel;
    private PlayerManager playerMng;
    void Start()
    {
        gamePanel = GameObject.FindGameObjectWithTag("GamePanel").GetComponent<GamePanel>();
    }
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        //gamePanel.GetRandomSkill();
        playerMng.Eat();
    }
    public void SetPlayerManger(PlayerManager playerManager)
    {
        this.playerMng = playerManager;
    }
}
