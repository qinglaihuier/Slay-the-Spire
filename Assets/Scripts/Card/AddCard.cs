using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddCard :CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse())
        {
            int num = int.Parse(data["Arg0"]);
            if (FightCardManager.Instance.HasCard())
            {
                UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(num);
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardPos();

                Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
                PlayEffect(pos);
            }
            else
            {
                base.OnEndDrag(eventData);
            }
        }
        else{
            base.OnEndDrag(eventData);
        }





       
    }



}
