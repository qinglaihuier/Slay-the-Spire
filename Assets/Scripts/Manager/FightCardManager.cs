using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCardManager 
{
    public static FightCardManager Instance = new FightCardManager();
    // Start is called before the first frame update
    public List<string> cardList;
    public List<string> usedCardList;
    
    
   public void Init()
    {
        cardList = new List<string>();
        usedCardList = new List<string>();

        List<string> tempList = new List<string>();

        tempList.AddRange(RoleManager.Instance.cardList);
        while (tempList.Count > 0)
        {
            int index = Random.Range(0, tempList.Count);
            
            cardList.Add(tempList[index]);
            tempList.RemoveAt(index);
        }
        Debug.Log(cardList.Count);
    }
    public bool HasCard()
    {
        return cardList.Count > 0;
    }
    public string DrawCard()
    {

        if (cardList.Count == 0) {
            cardList.AddRange(usedCardList);
            usedCardList.Clear();
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateUsedCardCount();
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
        }
        string cardId = cardList[cardList.Count - 1];
        cardList.RemoveAt(cardList.Count - 1);
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
        return cardId;
    }
    // Update is called once per frame
    
}
