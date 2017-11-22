using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Skill : MonoBehaviour {
    private Image cloneImage;
    private Button cloneBtn;
    public float coolDown;
    private float currentCoolDown;
    
	// Use this for initialization
	void Start () {

        cloneImage = transform.Find("CloneImage").GetComponent<Image>();
        cloneBtn = GetComponent<Button>();
        cloneBtn.onClick.AddListener(OnCloneClicked);
        currentCoolDown = coolDown;
	}
	
	// Update is called once per frame
	void Update () {

        
        if (currentCoolDown <= coolDown)
        {
            currentCoolDown += Time.deltaTime;
            cloneImage.fillAmount = currentCoolDown / coolDown;
            
        }
        
    }

    void OnCloneClicked()
    {
        if(currentCoolDown > coolDown)
        {
            Debug.Log("德玛西亚！");
            currentCoolDown = 0f;
        }
    }
}
