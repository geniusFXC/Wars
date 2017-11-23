using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFood : MonoBehaviour {
    private GamePanel gamePanel;
    private int eatFoodCount = 0;
    
    void Start()
    {
        gamePanel = GameObject.FindGameObjectWithTag("GamePanel").GetComponent<GamePanel>();
    }
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        //每吃三个食物，得到一个技能
        //if (++eatFoodCount % 3 == 0)
        //{
        //    gamePanel.ShowRandomSkill();
        //}


        gamePanel.ShowRandomSkill();
    }
    
}
