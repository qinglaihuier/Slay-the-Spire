using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private Transform canvasTf; //画布变换组件
    private List<UIBase> uiList;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        canvasTf = GameObject.Find("Canvas").transform;

        uiList = new List<UIBase>();

    }

    // Update is called once per frame
    public UIBase ShowUI<T>(string uiNmae) where T : UIBase
    {
        UIBase ui = Find(uiNmae);
        if(ui == null)
        {
            //从resourse文件夹中找
            GameObject obj = Instantiate(Resources.Load("UI/" + uiNmae), canvasTf) as GameObject;

            obj.name = uiNmae;

            ui = obj.AddComponent<T>();

            uiList.Add(ui);
        }
        else
        {
            ui.Show();
        }
        return ui;
    }
    public void HideUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if (ui != null)
        {
            ui.Hide();
        }
    }

    public void CloseAllUI()
    {
        for(int i = uiList.Count; i >= 0; i--)
        {
            Destroy(uiList[i].gameObject);
        }
        uiList.Clear();
    }
    public void CloseUI(string uiName)
    {
        UIBase ui = Find(uiName);
        
        if (ui != null)
        {
            uiList.Remove(ui);
            Debug.Log(uiName);
            Destroy(ui.gameObject);
        }
        else
        {
            Debug.Log("你想要关闭不存在的UI" + uiName);
        }
    }
    public T GetUI<T>(string uiName) where T:UIBase
    {
       UIBase ui = Find(uiName);
        if (ui != null)
        {
            return ui.GetComponent<T>();
        }
        return null;
    }
    //从集合中找名字对应集合
    public UIBase Find(string uiName)
    {
        for(int i = 0; i < uiList.Count; i++)
        {
            if(uiList[i].name == uiName)
            {
                return uiList[i];
            }
        }
        return null;
    }
    public GameObject CreateActionIcon()
    {
        GameObject obj = Instantiate(Resources.Load("UI/actionIcon"), canvasTf) as GameObject;
        obj.transform.SetAsLastSibling();//设置在父级最后一位
        return obj;
    }
    public GameObject CreateHpItem()
    {
        GameObject obj = Instantiate(Resources.Load("UI/HpItem"), canvasTf) as GameObject;
        obj.transform.SetAsLastSibling();//设置在父级最后一位
        return obj;
    }
    public void ShowTip(string msg,Color color,System.Action callback = null)
    {
        GameObject obj = Instantiate(Resources.Load("UI/Tips"), canvasTf) as GameObject;

        Text text = obj.transform.Find("bg/Text").GetComponent<Text>();
        text.color = color;
        text.text = msg;
        Tween scale1 = obj.transform.Find("bg").DOScale(1, 0.4f);
        Tween scale2 = obj.transform.Find("bg").DOScale(0, 0.5f);
        Sequence se = DOTween.Sequence();
        se.Append(scale1);
        se.AppendInterval(0.8f);
        se.Append(scale2);
        se.AppendCallback(delegate ()
        {
            if (callback != null)
            {
                callback();
            }
        });
        Destroy(obj, 1.5f);
    }

   
}
