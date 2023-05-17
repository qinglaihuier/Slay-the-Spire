using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DefendCard : CardItem
{
    // Start is called before the first frame update
    public override void OnEndDrag(PointerEventData eventData)
    {
       
        if (TryUse())
        {
            int val = int.Parse(data["Arg0"]);

            AudioManager.Instance.PlayEffect("Effect/healspell");

            FightManager.Instance.DefendCount += val;
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();

            Vector3 pos = Camera.main.transform.position;
            pos.y = pos.y - 0.5f;
            PlayEffect(pos);
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
