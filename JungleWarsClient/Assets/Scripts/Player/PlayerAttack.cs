#define ANDROID
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{

    public GameObject arrowPrefab;
    private Animator anim;
    private Transform leftHandTrans;
    private Vector3 shootDir;
    private Vector3 lastShootDir;
    private PlayerManager playerMng;
    private EventSystem eventSystem;
    private ETCButton attackBtn;
    private ETCJoystick mJoystick;

    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();
        //找到左手的位置，用于初始化弓箭的位置
        leftHandTrans = transform.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 L Clavicle/Bip001 L UpperArm/Bip001 L Forearm/Bip001 L Hand");
        //eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        attackBtn = GameObject.Find("AttackBtn").GetComponent<ETCButton>();
        attackBtn.onPressed.AddListener(OnAttackPressed);

        mJoystick = GameObject.Find("PlayerJoystick").GetComponent<ETCJoystick>();
        //在摇杆移动的时候，回到原点之前调用
        mJoystick.onMoveSpeed.AddListener(SetDir);
    }

    // Update is called once per frame
    void Update()
    {
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
        //{
        //    //if (Input.touchCount == 1)
        //    //Debug.Log(IsTouchOnUI());
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        //判断是否点击到UI，如果点击到UI，不进行攻击动作
        //        //#if IPHONE || ANDROID
        //        //                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) || Input.touchCount > 1)
        //        //#else
        //        //			if (!EventSystem.current.IsPointerOverGameObject())
        //        //#endif
        //        {
        //            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //            RaycastHit hit;
        //            bool isCollider = Physics.Raycast(ray, out hit);
        //            if (isCollider)
        //            {
        //                Vector3 targetPoint = hit.point;
        //                targetPoint.y = transform.position.y;
        //                shootDir = targetPoint - transform.position;
        //                transform.rotation = Quaternion.LookRotation(shootDir);
        //                anim.SetTrigger("Attack");
        //                Invoke("Shoot", 0.1f);
        //            }
        //        }
        //    }
        //}
       
    }

    private void OnAttackPressed()
    {
        ;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
        {

            anim.SetTrigger("Attack");
            Invoke("Shoot", 0.1f);


        }
    }

    public void SetPlayerMng(PlayerManager playerMng)
    {
        this.playerMng = playerMng;
    }
    private void Shoot()
    {
        playerMng.Shoot(arrowPrefab, leftHandTrans.position, Quaternion.LookRotation(shootDir));
        
    }

    private void SetDir(Vector2 dir)
    {
        
        shootDir = new Vector3(dir.x, 0, dir.y);
    }

}
