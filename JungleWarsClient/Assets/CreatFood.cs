using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatFood : MonoBehaviour {

    public GameObject foodCube;
    public int foodCount;
    public int randomMaxNum;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < foodCount; i++)
        {
            Vector3 newPos = new Vector3(Random.Range(0,randomMaxNum),0,Random.Range(0,randomMaxNum));
            GameObject.Instantiate(foodCube,newPos, foodCube.transform.rotation);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
