using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏入口脚本
public class GameApp : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        GameConfigManager.Instacne.Init();

        RoleManager.Instance.Init();

        AudioManager.Instance.Init();
        UIManager.Instance.ShowUI<LoginUI>("LoginUI");
        AudioManager.Instance.PlayBgm("bgm1");
       
    }
}
