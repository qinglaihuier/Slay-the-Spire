using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
public class CardItem :MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
{
   public Dictionary<string, string> data = new Dictionary<string, string>();
    // Start is called before the first frame update
    int index;

    Vector2 startPos;
    public void Init(Dictionary<string,string> data)
    {
        this.data = data;
        index = transform.GetSiblingIndex();
        transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgIcon"]);
        transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Icon"]);
        transform.Find("bg/msgTxt").GetComponent<Text>().text = string.Format(data["Des"], data["Arg0"]);
        transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
        transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];
     
        transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instacne.GetCardTypeById(data["Type"])["Name"];

        transform.Find("bg").GetComponent<Image>().material = Instantiate(Resources.Load<Material>("Mats/outline"));
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        startPos = GetComponent<RectTransform>().anchoredPosition;
        AudioManager.Instance.PlayEffect("Cards/draw");
        
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = new Vector2();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
           transform.parent.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out pos);
        GetComponent<RectTransform>().anchoredPosition = pos;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<RectTransform>().anchoredPosition = startPos;
        transform.SetSiblingIndex(index);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.5f, 0.25f);
        transform.SetAsLastSibling();
        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.yellow);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 10);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1, 0.1f);
        transform.SetSiblingIndex(index);
        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.black);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 1);
    }

    private void Start()
    {
       
        
    }
    public virtual bool TryUse()
    {
        int cost = int.Parse(data["Expend"]);
        if(cost > FightManager.Instance.CurPowerCount)
        {
            AudioManager.Instance.PlayEffect("Effect/lose");

            UIManager.Instance.ShowTip("费用不足", Color.red);

           

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
    public void PlayEffect(Vector3 pos)
    {
        GameObject effectObj = Instantiate(Resources.Load(data["Effects"])) as GameObject;
        effectObj.transform.position = pos;
        Destroy(effectObj, 2f);
    }
}
