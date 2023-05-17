using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_PlayerTurn : FightUnit
{
    public override void Init()
    {
        FightManager.Instance.CurPowerCount = FightManager.Instance.MaxPowerCount;
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();



        UIManager.Instance.ShowTip("��һغ�", Color.green, delegate ()
        {
            UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(4);
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardPos();
           
            Debug.Log("����");
        });
    }
    public override void OnUpData()
    {
       
    }
    // Start is called before the first frame update

}
