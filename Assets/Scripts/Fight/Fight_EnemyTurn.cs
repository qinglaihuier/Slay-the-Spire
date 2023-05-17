using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_EnemyTurn : FightUnit
{
    public override void Init()
    {
        UIManager.Instance.GetUI<FightUI>("FightUI").RemoveAllCards();

        UIManager.Instance.GetUI<FightUI>("FightUI").CancelTurnButtonOnClickEvent();

        UIManager.Instance.ShowTip("µ–»Àªÿ∫œ", Color.red,delegate() {

            FightManager.Instance.StartCoroutine(EnemyManager.Instance.DoAllEnemyAction());

        });


        
        
    }
    public override void OnUpData()
    {
        
    }
    // Start is called before the first frame update

}
