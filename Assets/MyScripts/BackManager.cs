using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackManager : MonoBehaviour
{
    //Book
    public GameObject book;
    //返回按钮
    //public GameObject back;
    //关闭按钮
    public Button close;

    //对话继续按钮
    public Button nextButton;
    //翻书箭头
    public GameObject flip;

    //指南
    public GameObject guidebook;
    //关闭指南按钮
    public Button close_guide;

    //提问者的动画信息
    //public Animator questioner_ani;


    // Start is called before the first frame update
    void Start()
    {
        //back.gameObject.SetActive(false);
        close.gameObject.SetActive(false);
        book.gameObject.SetActive(false);
        guidebook.gameObject.SetActive(false);
        close_guide.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HideBook()
    {
        book.gameObject.SetActive(false);
        close.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
        flip.gameObject.SetActive(false);
        //back.gameObject.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//编辑状态下退出
        #else
        Application.Quit();//打包编译后退出
        #endif
    }

    public void ShowGuide()
    {
        guidebook.gameObject.SetActive(true);
        close_guide.gameObject.SetActive(true);
    }

    public void HideGuide()
    {
        guidebook.gameObject.SetActive(false);
        close_guide.gameObject.SetActive(false);
    }
}