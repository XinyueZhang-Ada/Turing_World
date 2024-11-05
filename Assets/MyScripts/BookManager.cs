using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BookManager : MonoBehaviour
{

    //书籍logo
    public SpriteRenderer myBook;
    //Book
    public GameObject book;
    //关闭按钮
    public GameObject close;
    //开始按钮
    public GameObject start;
    //welcome
    public TMP_Text welcome;
    //read介绍
    //public TMP_Text read;
    //bubble
    public SpriteRenderer bubble;
    //翻书箭头
    public GameObject flip;
    //提问者的动画信息
    public Animator questioner_ani;
    //指南
    public GameObject guidebook;
    //关闭指南按钮
    public Button close_guide;

    // Start is called before the first frame update
    void Start()
    {
        start.gameObject.SetActive(false);
        myBook.gameObject.SetActive(true);
        book.gameObject.SetActive(false);
        close.gameObject.SetActive(false);
        flip.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowBook()
    {
        //srart.gameObject.SetActive(true);
        myBook.gameObject.SetActive(false);
        book.gameObject.SetActive(true);
        close.gameObject.SetActive(true);
        //read.gameObject.SetActive(false);
        bubble.gameObject.SetActive(false);
        welcome.gameObject.SetActive(false);
        flip.gameObject.SetActive(true);
        questioner_ani.SetBool("guide", true);
    }

    public void HideBook()
    {
        book.gameObject.SetActive(false);
        close.gameObject.SetActive(false);
        start.gameObject.SetActive(true);
        flip.gameObject.SetActive(false);
        questioner_ani.SetBool("guide", false);
    }


    public void ShowGuide()
    {
        guidebook.gameObject.SetActive(true);
        close_guide.gameObject.SetActive(true);
        questioner_ani.SetBool("guide", true);
    }

    public void HideGuide()
    {
        guidebook.gameObject.SetActive(false);
        close_guide.gameObject.SetActive(false);
        questioner_ani.SetBool("guide", false);
    }
}
