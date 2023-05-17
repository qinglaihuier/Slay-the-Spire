using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//开始界面
public class LoginUI : UIBase
{
    // Start is called before the first frame update
    private void Awake()
    {
        Register("bg/startBtn").onClick = onStartGameBtn; 
    }
    private void onStartGameBtn(GameObject obj,PointerEventData pData)
    {
        //关闭login界面
        Close();
        FightManager.Instance.ChangeType(FightType.Init);
    }
}
