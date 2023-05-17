using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class AttackCardItem :CardItem,IPointerDownHandler
{
    Enemy hitenemy;
    public void OnPointerDown(PointerEventData eventData)
    {
        /*AudioManager.Instance.PlayEffect("Cards/draw");

        Cursor.visible = false;
        StopAllCoroutines();
        StartCoroutine(nameof(OnMouseDownRight), eventData);*/
        // throw new System.NotImplementedException();
        AudioManager.Instance.PlayEffect("Cards/draw");
        Cursor.visible = false;
        StopAllCoroutines();
        UIManager.Instance.ShowUI<LineUI>("LineUI");
        StartCoroutine(nameof(OnMouseDownRight), eventData);

      
        Vector2 startPos;

         RectTransformUtility.ScreenPointToLocalPointInRectangle(
                         transform.parent.GetComponent<RectTransform>(),
                         eventData.position,
                         eventData.pressEventCamera,
                         out startPos);

        UIManager.Instance.GetUI<LineUI>("LineUI").SetStartPos(startPos);
      
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
      
        
    }
    public override void OnDrag(PointerEventData eventData)
    {
       
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
       
    }

    // Start is called before the first frame update
    
   
   IEnumerator OnMouseDownRight(PointerEventData pData)
    {
        Vector2 pos = new Vector2();
        while (true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                break;
            }
            else if(RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        transform.parent.GetComponent<RectTransform>(),
                        pData.position,
                        pData.pressEventCamera,
                        out pos
            ))
            {
              
                UIManager.Instance.GetUI<LineUI>("LineUI").SetEndPos(pos);
                UIManager.Instance.GetUI<LineUI>("LineUI").CalcuteMidBezierPoint();
                CheckEnemy();
            }
            yield return null;
        }
        if (hitenemy != null)
        {
            hitenemy.OnUnSelect();
            hitenemy = null;
        }
        Cursor.visible = true;
        UIManager.Instance.CloseUI("LineUI");
    }
    
    void CheckEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit,1000, LayerMask.GetMask("Enemy")))
        {
            
            hitenemy = hit.transform.GetComponent<Enemy>();
            hitenemy.OnSelect();
            if (Input.GetMouseButtonDown(0))
            {
                StopAllCoroutines();
                Cursor.visible = true;
                if (TryUse())
                {
                    AudioManager.Instance.PlayEffect("Effect/Sword");
                    int val = int.Parse(data["Arg0"]);
                    hitenemy.Hit(val);
                    PlayEffect(hitenemy.transform.position);

                    UIManager.Instance.CloseUI("LineUI");
                    hitenemy.OnUnSelect();
                    hitenemy = null;
                }
              
            }
        }
        else
        {
            if (hitenemy != null)
            {
                hitenemy.OnUnSelect();
                hitenemy = null;
            }
        }
    }
    public override bool TryUse()
    {
        int cost = int.Parse(data["Expend"]);
        if (cost > FightManager.Instance.CurPowerCount)
        {
            AudioManager.Instance.PlayEffect("Effect/lose");

            UIManager.Instance.ShowTip("费用不足", Color.red);

            UIManager.Instance.CloseUI("LineUI");

            if (hitenemy)
            {
                hitenemy.OnUnSelect();
                hitenemy = null;
            }

            return false;
        }
        else
        {
            FightManager.Instance.CurPowerCount -= cost;
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();

            //删除使用的卡牌
            UIManager.Instance.GetUI<FightUI>("FightUI").RemoveCard(this);


            return true;
        }
    }
}