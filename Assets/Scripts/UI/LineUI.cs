using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LineUI :UIBase
{
    Vector2 startPos = new Vector2();
    Vector2 endPos = new Vector2();
    Vector2 midPos = new Vector2();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetStartPos(Vector2 pos)
    {
        transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = pos;
        startPos = pos;
    }
    public void SetEndPos(Vector2 pos)
    {
        transform.GetChild(transform.childCount - 1).GetComponent<RectTransform>().anchoredPosition = pos;
        endPos = pos;
    }
    public void CalcuteMidBezierPoint()
    {
        midPos.y = (startPos.y + endPos.y) * 0.5f;
        midPos.x = startPos.x;
        float t = (float)1 / (transform.childCount-1);
        Vector2 dir = endPos - startPos;
        dir.Normalize();
        float angle ;
    
        for(int i = transform.childCount - 2; i >= 0; i--)
        {
            
            transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = GetBezier(startPos, midPos, endPos, t * i);
            dir = transform.GetChild(i + 1).GetComponent<RectTransform>().anchoredPosition - transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition;
            dir.Normalize();
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            transform.GetChild(i).eulerAngles = new Vector3(0, 0, angle);
            if(i == transform.childCount - 2)
            {
                transform.GetChild(transform.childCount - 1).eulerAngles = new Vector3(0, 0, angle);
            }
        }
      
    }
    Vector2 GetBezier(Vector2 startPos, Vector2 midPos, Vector2 endPos, float t)
    {

       
        return (1 - t) * (1 - t) * startPos + 2*(1 - t) * t * midPos + t * t * endPos;
        
    }
}
