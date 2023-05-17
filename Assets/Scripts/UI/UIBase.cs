using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    
    public UIEventTrigger Register(string name)
    {
        Transform tf = transform.Find(name);
        return UIEventTrigger.Get(tf.gameObject);
    }


    // Start is called before the first frame update
  ///<summary>
  ///界面基类
  ///</summary>>
    public virtual void Close()
    {
        UIManager.Instance.CloseUI(gameObject.name);
    }


    public virtual void Show()
    {
        gameObject.SetActive(true);
    }
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
   

    // Update is called once per frame
  
}
