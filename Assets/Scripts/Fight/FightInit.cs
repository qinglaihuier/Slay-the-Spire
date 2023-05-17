using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FightInit : FightUnit
{
    public override void Init()
    {
        FightManager.Instance.Init();

        AudioManager.Instance.PlayBgm("battle");
        //显示战斗界面

        EnemyManager.Instance.LoadRes("10003");
        
        UIManager.Instance.ShowUI<FightUI>("FightUI");

        FightCardManager.Instance.Init();

        FightManager.Instance.ChangeType(FightType.Player);
    
    }
    public override void OnUpData()
    {
        base.OnUpData();
    }
    // Start is called before the first frame update

}
