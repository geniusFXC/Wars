using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;

public class GamePanel : BasePanel {

    private Text timer;
    private int time = -1;
    private Button successBtn;
    private Button failBtn;
    private Button exitBtn;
    private Canvas easyTouchControlsCanvas;

    //随机技能框
    private RectTransform randomSkillPanel;
    //随机获得三个技能的按钮
    private Button skillOneBtn;
    private Button skillTwoBtn;
    private Button skillThreeBtn;

    //test 模拟升级获得技能的按钮
    private Button learnSkillBtn;

    //已获得的技能
    private Text learnSkillOneText;
    private Text learnSkillTwoText;
    private Text learnSkillThreeText;

    public List<int> skillArr;//初始所有技能的数组
    public List<int> newSkillArr;//随机出三个技能的新数组
    public List<string> learnSkillArr = new List<string>();//已经学习到的技能数组

    //已获得技能的等级
    private int skillOneLeve = 0;
    private int skillTwoLeve = 0;
    private int skillThreeLeve = 0;

    private QuitBattleRequest quitBattleRequest;
    private void Start()
    {
        timer = transform.Find("Timer").GetComponent<Text>();
        timer.gameObject.SetActive(false);
        successBtn = transform.Find("SuccessButton").GetComponent<Button>();
        successBtn.onClick.AddListener(OnResultClick);
        successBtn.gameObject.SetActive(false);
        failBtn = transform.Find("FailButton").GetComponent<Button>();
        failBtn.onClick.AddListener(OnResultClick);
        failBtn.gameObject.SetActive(false);
        exitBtn = transform.Find("ExitButton").GetComponent<Button>();
        exitBtn.onClick.AddListener(OnExitClick);
        exitBtn.gameObject.SetActive(false);

        easyTouchControlsCanvas = transform.Find("EasyTouchControlsCanvas").GetComponent<Canvas>();
        easyTouchControlsCanvas.gameObject.SetActive(true);

        skillOneBtn = transform.Find("RandomSkillPanel/FirstRandomBtn").GetComponent<Button>();
        skillTwoBtn = transform.Find("RandomSkillPanel/SecondRandomBtn").GetComponent<Button>();
        skillThreeBtn = transform.Find("RandomSkillPanel/ThirdRandomBtn").GetComponent<Button>();

        randomSkillPanel = transform.Find("RandomSkillPanel").GetComponent<RectTransform>();
        randomSkillPanel.gameObject.SetActive(false);
        //测试用的模拟升级获取技能
        //learnSkillBtn = transform.Find("GetSkillBtn").GetComponent<Button>();
        //learnSkillBtn.onClick.AddListener(GetRandomSkill);
        learnSkillOneText = transform.Find("FirstLearnText").GetComponent<Text>();
        learnSkillTwoText = transform.Find("SecondLearnText").GetComponent<Text>();
        learnSkillThreeText = transform.Find("ThirdLearnText").GetComponent<Text>();

        quitBattleRequest = GetComponent<QuitBattleRequest>();

        
        skillOneBtn.onClick.AddListener(delegate () { OnLearnSkillClicked(newSkillArr[0].ToString()); });
        skillTwoBtn.onClick.AddListener(delegate () { OnLearnSkillClicked(newSkillArr[1].ToString()); });
        skillThreeBtn.onClick.AddListener(delegate () { OnLearnSkillClicked(newSkillArr[2].ToString()); });

        Debug.Log("初始化完毕");
    }
    public override void OnEnter()
    {
        gameObject.SetActive(true);
    }
    public override void OnExit()
    {
        successBtn.gameObject.SetActive(false);
        failBtn.gameObject.SetActive(false);
        exitBtn.gameObject.SetActive(false);
        gameObject.SetActive(false);
        //隐藏虚拟摇杆
        easyTouchControlsCanvas.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (time > -1)
        {
            ShowTime(time);
            time = -1;
            //倒计时结束后，显示虚拟摇杆
            //easyTouchControlsCanvas.transform.Find("PlayerJoystick").GetComponent<ETCJoystick>().visible = true;
            //attackBtn.visible = true;
        }
    }
    private void OnResultClick()
    {
        uiMng.PopPanel();
        uiMng.PopPanel();
        facade.GameOver();
    }
    private void OnExitClick()
    {
        quitBattleRequest.SendRequest();
        
    }
    //学习其中一个技能
    private void OnLearnSkillClicked(string skillName)
    {
        int temp = 0;

        if (learnSkillArr.Contains(skillName))
        {
            Debug.Log("已有");
        }
        else
        {
            learnSkillArr.Add(skillName);
        }


        for (int i = 0; i < learnSkillArr.Count; i++)
        {
            if (learnSkillArr[i] == skillName)
            {
                temp = i + 1;
            }
        }
        switch (temp)
        {
            case 1:
                skillOneLeve += 1;
                learnSkillOneText.text = skillName + GetUltimateSkill(skillOneLeve);
                break;
            case 2:
                skillTwoLeve += 1;
                learnSkillTwoText.text = skillName + GetUltimateSkill(skillTwoLeve);
                break;
            case 3:
                skillThreeLeve += 1;
                learnSkillThreeText.text = skillName + GetUltimateSkill(skillThreeLeve);
                break;
        }
        randomSkillPanel.gameObject.SetActive(false);
    }
    public void OnExitResponse()
    {
        OnResultClick();
    }
    public void ShowTimeSync(int time)
    {
        this.time = time;
    }
    public void ShowTime(int time)
    {
        if (time == 3)
        {
            exitBtn.gameObject.SetActive(true);
        }
        timer.gameObject.SetActive(true);
        timer.text = time.ToString();
        timer.transform.localScale = Vector3.one;
        Color tempColor = timer.color;
        tempColor.a = 1;
        timer.color = tempColor;
        timer.transform.DOScale(2, 0.3f).SetDelay(0.3f);
        timer.DOFade(0, 0.3f).SetDelay(0.3f).OnComplete(() => timer.gameObject.SetActive(false));
        facade.PlayNormalSound(AudioManager.Sound_Alert);
    }
    public void OnGameOverResponse(ReturnCode returnCode)
    {
        Button tempBtn = null;
        switch (returnCode)
        {
            case ReturnCode.Success:
                tempBtn = successBtn;
                break;
            case ReturnCode.Fail:
                tempBtn = failBtn;
                break;
        }
        tempBtn.gameObject.SetActive(true);
        tempBtn.transform.localScale = Vector3.zero;
        tempBtn.transform.DOScale(1, 0.5f);
    }

    //获得随机的三个技能
    public void GetRandomSkill()
    {
        randomSkillPanel.gameObject.SetActive(true);
        RandomThreeSkill();
    }
    //随机三个技能
    private void RandomThreeSkill()
    {
        skillArr.Clear();
        newSkillArr.Clear();
        for (int i = 0; i < 10; i++)
        {
            skillArr.Add(i);
        }



        int a = Random.Range(0, skillArr.Count);
        newSkillArr.Add(skillArr[a]);
        skillArr.RemoveAt(a);

        int b = Random.Range(0, skillArr.Count - 1);
        newSkillArr.Add(skillArr[b]);
        skillArr.RemoveAt(b);

        int c = Random.Range(0, skillArr.Count - 2);
        newSkillArr.Add(skillArr[c]);

        skillOneBtn.GetComponentInChildren<Text>().text = newSkillArr[0].ToString();
        skillTwoBtn.GetComponentInChildren<Text>().text = newSkillArr[1].ToString();
        skillThreeBtn.GetComponentInChildren<Text>().text = newSkillArr[2].ToString();

        for (int i = 0; i < newSkillArr.Count; i++)
        {
            Debug.Log("随机出的三个技能>>>>>" + newSkillArr[i]);
        }


    }
    //习得技能的等级得到终极技能
    private string GetUltimateSkill(int i)
    {
        string skillLeve = "";
        if (i < 5)
        {
            skillLeve = "技能等级" + i.ToString();
        }
        else
        {
            skillLeve = "终极技能";
            i = 5;
        }

        return skillLeve;
    }
}
