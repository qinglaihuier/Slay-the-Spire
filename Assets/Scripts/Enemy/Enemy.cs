using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ActionType { 
    None,
    Defend,
    Attack


}



/// <summary>
/// 敌人脚本
/// </summary>
public class Enemy : MonoBehaviour
{
    private Dictionary<string, string> data;

    public ActionType type;

    public GameObject hpItemObj;
    public GameObject actionObj;

    //ui相关
    public Transform attackTf;
    public Transform defendTf;
    public Text defendTxt;
    public Text hpTxt;
    public Image hpImg;

    public int Defend;
    public int Attack;
    public int MaxHp;
    public int CurHp;

    SkinnedMeshRenderer meshRender;
    Animator anim;
    public void Init(Dictionary<string,string> data)
    {
        this.data = data;
    }
    // Start is called before the first frame update
    private void Start()
    {
        meshRender = GetComponentInChildren<SkinnedMeshRenderer>();
        anim = GetComponent<Animator>();

        type = ActionType.None;
        hpItemObj = UIManager.Instance.CreateHpItem();
        actionObj = UIManager.Instance.CreateActionIcon();

        hpItemObj.transform.SetAsFirstSibling();
        actionObj.transform.SetAsFirstSibling();

        hpItemObj.transform.position = Camera.main.WorldToScreenPoint(transform.position+Vector3.down*0.2f);
        actionObj.transform.position = Camera.main.WorldToScreenPoint(transform.Find("head").position);
        //ui相关
        attackTf = actionObj.transform.Find("attack");
        defendTf = actionObj.transform.Find("defend");


        defendTxt = hpItemObj.transform.Find("fangyu/Text").GetComponent<Text>();
        hpTxt = hpItemObj.transform.Find("hpTxt").GetComponent<Text>();
        hpImg = hpItemObj.transform.Find("fill").GetComponent<Image>();



        SetRandomAction();
        //初始化数据
        Attack = int.Parse(data["Attack"]);
        CurHp = int.Parse(data["Hp"]);
        MaxHp = CurHp;
        Defend = int.Parse(data["Defend"]);

        UpdateHp();
        UpdateDefend();

        
    }
    public void SetRandomAction()
    {
        int ran = Random.Range(0, 3);
        type = (ActionType)ran;
        Debug.Log(type);
        switch (type)
        {
            case ActionType.None:
                break;
            case ActionType.Defend:
                attackTf.gameObject.SetActive(false);
                defendTf.gameObject.SetActive(true);
                break;
            case ActionType.Attack:
                attackTf.gameObject.SetActive(true);
                defendTf.gameObject.SetActive(false);
                break;
        }





    }
    public void UpdateHp()
    {
        hpTxt.text = CurHp + "/" + MaxHp;
        hpImg.fillAmount = (float)CurHp / (float)MaxHp;

    }
    //更新防御信息
    public void UpdateDefend()
    {
        defendTxt.text = Defend.ToString();
    }
    public void OnSelect()
    {
        meshRender.material.SetColor("_OtlColor", Color.green);
    }
    public void OnUnSelect()
    {
        meshRender.material.SetColor("_OtlColor", Color.black);
    }
    public void Hit(int damage)
    {
        if (damage <= Defend)
        {
            Defend -= damage;

            anim.Play("hit",0,0);
           // UIManager.Instance.GetUI
        }
        else
        {
            damage = damage - Defend;
            Defend = 0;
            CurHp -= damage;

            if (CurHp < 0)
            {
                CurHp = 0;
                anim.Play("die");
                EnemyManager.Instance.RemoveEnemy(this);
                Destroy(gameObject, 1);
                Destroy(actionObj);
                Destroy(hpItemObj);
            }
            else
            {
                anim.Play("hit", 0, 0);
            }
            

           
        }
        UpdateDefend();
        UpdateHp();
    }
    void HideAction()
    {
        attackTf.gameObject.SetActive(false);
        defendTf.gameObject.SetActive(false);
    }
   public IEnumerator DoAction()
    {
        HideAction();
        anim.Play("attack");
        yield return new WaitForSeconds(0.5f);
        switch (type) {
            case ActionType.None:
                    break;
            case ActionType.Attack:
                FightManager.Instance.BeHit(Attack);
                break;
            case ActionType.Defend:
                Defend += 3;
                UpdateDefend();
                break;


        }
        anim.Play("idle");
    }

}
