using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RandomSkill : MonoBehaviour
{
    public List<int> skillArr;//初始所有技能的数组
    public List<int> newSkillArr;//随机出三个技能的新数组
    public List<string> learnSkillArr = new List<string>();//已经学习到的技能数组

    //随机获得三个技能的按钮
    private Button skillOneBtn;
    private Button skillTwoBtn;
    private Button skillThreeBtn;

    //模拟升级获得技能的按钮
    private Button learnSkillBtn;

    //已获得的技能
    private Text learnSkillOneText;
    private Text learnSkillTwoText;
    private Text learnSkillThreeText;

    //已获得技能的等级
    private int skillOneLeve = 0;
    private int skillTwoLeve = 0;
    private int skillThreeLeve = 0;

    
    // Use this for initialization
    void Awake()
    {
            
    }
    void Start()
    {
        learnSkillBtn.onClick.AddListener(GetRandomSkill);
        skillOneBtn.onClick.AddListener(delegate () { OnLearnSkillClicked(newSkillArr[0].ToString()); });
        skillTwoBtn.onClick.AddListener(delegate () { OnLearnSkillClicked(newSkillArr[1].ToString()); });
        skillThreeBtn.onClick.AddListener(delegate () { OnLearnSkillClicked(newSkillArr[2].ToString()); });
    }
    //习得技能
    void OnLearnSkillClicked(string skillName)
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

        //foreach(string i in learnSkillArr)
        //{
        //    if(learnSkillArr[int.Parse(i)] == skillName)
        //    {
        //        temp = int.Parse(i);
        //    }
        //}

        for (int i = 0;i<learnSkillArr.Count;i++)
        {
            if (learnSkillArr[i] == skillName)
            {
                temp = i +1;
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
    }
    //弹出随机的三个技能
    void GetRandomSkill()
    {
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
    string GetUltimateSkill(int i)
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
