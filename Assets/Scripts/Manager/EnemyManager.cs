using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人管理器
/// </summary>
public class EnemyManager 
{
    // Start is called before the first frame update
    public static EnemyManager Instance = new EnemyManager();

    private List<Enemy> enemyList;
    
    
    
    /// <summary>
   /// 加载敌人资源
   /// </summary>
   /// <param name="id">关卡Id</param>
    public void LoadRes(string id)
    {
        enemyList = new List<Enemy>();

        Dictionary<string, string> levelData = GameConfigManager.Instacne.GetLevelById(id);

        string[] enemyIds = levelData["EnemyIds"].Split('=');

        string[] enemyPos = levelData["Pos"].Split('=');
        
        for(int i = 0; i < enemyIds.Length; i++)
        {
            string enemyId = enemyIds[i];

            string[] posArr = enemyPos[i].Split(',');

            float x = float.Parse(posArr[0]);
            float y = float.Parse(posArr[1]);
            float z = float.Parse(posArr[2]);

            Dictionary<string, string> enemyData = GameConfigManager.Instacne.GetEnemyById(enemyId);

            GameObject obj = Object.Instantiate(Resources.Load(enemyData["Model"])) as GameObject;

            Enemy enemy = obj.AddComponent<Enemy>();
            enemy.Init(enemyData);
            enemyList.Add(enemy);

            obj.transform.position = new Vector3(x, y, z);

        }
    }
    public void RemoveEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);
        if(enemyList.Count == 0)
        {
            FightManager.Instance.ChangeType(FightType.Win);
        }
        
    }
    public IEnumerator DoAllEnemyAction()
    {
        for(int i = 0; i < enemyList.Count; i++)
        {
            yield return FightManager.Instance.StartCoroutine(enemyList[i].DoAction());
        }
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].SetRandomAction();
        }
        FightManager.Instance.ChangeType(FightType.Player);
    }
}
