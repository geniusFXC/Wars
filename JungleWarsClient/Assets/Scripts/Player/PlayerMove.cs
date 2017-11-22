using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float forward = 0;

    private float speed = 3;
    private Animator anim;
    private ETCJoystick mJoystick;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        mJoystick = GameObject.Find("PlayerJoystick").GetComponent<ETCJoystick>();
        mJoystick.onMoveEnd.AddListener(ResetForward);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Grounded") == false) return;
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        float h = mJoystick.axisX.axisValue;
        float v = mJoystick.axisY.axisValue;

        //Debug.Log(mJoystick.axisX.axisValue + "....." + Input.GetAxis("Horizontal"));

        if (Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0)
            //if (mJoystick.isOnDrag)
            {
            //Debug.Log(h+"........"+v);
            transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime, Space.World);

            transform.rotation = Quaternion.LookRotation(new Vector3(h, 0, v));

            float res = Mathf.Max(Mathf.Abs(h), Mathf.Abs(v));
            forward = res;
            anim.SetFloat("Forward", res);
        }
        
           
            
        
	}
    //摇杆不动时，重置人物为静止状态
    private void ResetForward()
    {
        anim.SetFloat("Forward", 0);
    }

}
