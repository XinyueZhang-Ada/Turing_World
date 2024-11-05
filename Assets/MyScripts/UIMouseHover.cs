using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //鼠标是否悬停
    private bool isShow;
    //设置GUI样式
    private GUIStyle style;
    //展示的信息
    public string text;

    // Start is called before the first frame update
    void Start()
    {
        isShow = false;
        //默认的矩形框样式
        style = new GUIStyle("box");
        style.fontSize = 12;
        style.normal.textColor = Color.black;
        //文字排列样式：垂直居中水平靠左
        style.alignment = TextAnchor.MiddleLeft;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //实现接口
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse hover.");
        isShow = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse left.");
        isShow = false;
    }

    //在此方法中判断
    private void OnGUI()
    {
        if (isShow)
        {
            //让text满足窗口尺寸
            var vt = style.CalcSize(new GUIContent(text));
            GUI.backgroundColor = new Color32(255, 255, 255, 100);
            //Label表示出现的位置，其中Rect对象中为box的位置及大小
            GUI.Label(new Rect(Input.mousePosition.x+10, Screen.height - Input.mousePosition.y, vt.x, vt.y), text, style);
        }
    }
}
