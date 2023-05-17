using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class FightUI :UIBase
{
    private Text cardCountTxt;
    private Text noCardCountTxt;//ÆúÅÆ¶Ñ
    private Text powerTxt;
    private Text hpTxt;
    private Image hpImg;
    private Text fyTxt; //·ÀÓùÊýÖµ
    

    List<CardItem> cardList;
    public bool isCreateCard;
    
    private void Awake()
    {
        cardList = new List<CardItem>();
        cardCountTxt = transform.Find("hasCard/icon/Text").GetComponent<Text>();
        noCardCountTxt = transform.Find("noCard/icon/Text").GetComponent<Text>();
        powerTxt = transform.Find("mana/Text").GetComponent<Text>();
        hpTxt = transform.Find("hp/moneyTxt").GetComponent<Text>();
        hpImg = transform.Find("hp/fill").GetComponent<Image>();
        fyTxt = transform.Find("hp/fangyu/Text").GetComponent<Text>();
       
    }
    private void Start()
    {
        UpdateHp();
        UpdatePower();
        UpdateDefense();
        UpdateCardCount();
        UpdateUsedCardCount();
    }
    public void UpdateHp()
    {
        hpTxt.text = FightManager.Instance.CurHp + "/" + FightManager.Instance.MaxHP;
        hpImg.fillAmount = (float)FightManager.Instance.CurHp / (float)FightManager.Instance.MaxHP;

    }
    void turnButtonOnClick()
    {
        if(FightManager.Instance.fightUnit is Fight_PlayerTurn&&!isCreateCard)
        {

           
            FightManager.Instance.ChangeType(FightType.Enemy);
          
        }
    }
    public void CancelTurnButtonOnClickEvent()
    {
        transform.Find("turnBtn").GetComponent<Button>().onClick.RemoveAllListeners();
    }
  
    public void UpdatePower()
    {
        powerTxt.text = FightManager.Instance.CurPowerCount + "/" + FightManager.Instance.MaxPowerCount;

    }
    public void UpdateDefense()
    {
        fyTxt.text = FightManager.Instance.DefendCount.ToString();

    }
    public void UpdateCardCount()
    {
        cardCountTxt.text = FightCardManager.Instance.cardList.Count.ToString();

    }
    public void UpdateUsedCardCount()
    {
        noCardCountTxt.text = FightCardManager.Instance.usedCardList.Count.ToString();
    }
    public void CreateCardItem(int count)
    {
        
        for( int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(Resources.Load("UI/CardItem"),transform) as GameObject;
          
            string cardId = FightCardManager.Instance.DrawCard();
            Dictionary<string, string> data = GameConfigManager.Instacne.GetCardById(cardId);
            CardItem item = obj.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
            item.Init(data);
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000, -700);
            cardList.Add(item);
        }
        transform.Find("turnBtn").GetComponent<Button>().onClick.AddListener(turnButtonOnClick);
       
        UpdateUsedCardCount();
    }
    public void UpdateCardPos()
    {
        if (cardList.Count == 0) return;
        float offset = 800 / cardList.Count;
        Vector2 startPos = new Vector2(-cardList.Count / 2 * offset + offset * 0.5f, -700);
        for(int i = 0; i < cardList.Count; i++)
        {
            cardList[i].GetComponent<RectTransform>().DOAnchorPos(startPos, 0.5f);
            startPos.x += offset;
        }
    }
    // Start is called before the first frame update
    public void RemoveCard(CardItem item)
    {
        AudioManager.Instance.PlayEffect("Cards/cardShove");
        item.enabled = false;

        FightCardManager.Instance.usedCardList.Add(item.data["Id"]);

        UpdateUsedCardCount();

        noCardCountTxt.text = FightCardManager.Instance.usedCardList.Count.ToString();

        cardList.Remove(item);

        UpdateCardPos();
        
        item.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1000, -700), 0.25f);

        item.transform.DOScale(0, 0.25f);
        Destroy(item.gameObject, 1);

    }
    public void RemoveAllCards()
    {
        int cardNum = cardList.Count;
        for(int i = cardNum-1;i>= 0; i--){
            RemoveCard(cardList[i]);
        }
    }
}
