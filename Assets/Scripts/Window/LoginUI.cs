using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//��ʼ����
public class LoginUI : UIBase
{
    // Start is called before the first frame update
    private void Awake()
    {
        Register("bg/startBtn").onClick = onStartGameBtn; 
    }
    private void onStartGameBtn(GameObject obj,PointerEventData pData)
    {
        //�ر�login����
        Close();
        FightManager.Instance.ChangeType(FightType.Init);
    }
}
