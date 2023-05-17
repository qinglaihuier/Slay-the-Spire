using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum FightType
{
    None,
    Init,
    Player,
    Enemy,
    Win,
    Loss
}
/// <summary>
/// 战斗管理器
/// </summary>
public class FightManager : MonoBehaviour
{
    // Start is called before the first frame update
    [System.NonSerialized] public static FightManager Instance;
    [System.NonSerialized] public FightUnit fightUnit;

    [System.NonSerialized] public int MaxHP;
    [System.NonSerialized] public int CurHp;

    [System.NonSerialized] public int MaxPowerCount;//最大能量（卡牌使用消耗）
    [System.NonSerialized] public int CurPowerCount;
    [System.NonSerialized] public int DefendCount;//防御值
    public void Init()
    {
        MaxHP = 10;
        CurHp = 10;
        MaxPowerCount = 3;
        CurPowerCount = 3;
        DefendCount = 10;
    }


    private void Awake()
    {
        Instance = this;
    }
    public void ChangeType(FightType type)
    {
        switch (type)
        {
            case FightType.None:
                fightUnit = new FightUnit();
                break;
            case FightType.Init:
                fightUnit = new FightInit();
                break;
            case FightType.Player:
                fightUnit = new Fight_PlayerTurn();
                break;
            case FightType.Enemy:
                fightUnit = new Fight_EnemyTurn();
                break;
            case FightType.Win:
                fightUnit = new Fight_Win();
                break;
            case FightType.Loss:
                fightUnit = new Fight_Loss();
                break;
        }
        fightUnit.Init();
    }
    private void Update()
    {
        if(fightUnit!=null)
          fightUnit.OnUpData();
    }
    public void BeHit(int damage)
    {
        if (DefendCount>=damage)
        {
            DefendCount -= damage;
            //播放音效 特效等  更改UI

        }
        else
        {
            damage -= DefendCount;
            DefendCount = 0;
            CurHp -= damage;
            if (CurHp <= 0)
            {
                CurHp = 0;
                //游戏结束
                FightManager.Instance.ChangeType(FightType.Loss);
            }

            Camera.main.DOShakePosition(0.1f, 0.2f, 5, 45);
            
        }
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHp();
    }
    
}
