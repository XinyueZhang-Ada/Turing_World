using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Linq;


public class DialogManager : MonoBehaviour
{

    //对话文本文件，csv文件
    public TextAsset dialogDataFile;
    //左侧角色图像
    public Transform personLeft;
    //右侧角色图像
    public Transform personRight;
    //提问者文本
    public TMP_Text quesText;
    //提问者背景
    public SpriteRenderer quesBubble;
    //回答者文本
    public TMP_Text respText;
    //回答者背景
    public SpriteRenderer respBubble;

    //对话内容文本
    //public TMP_Text dialogText;
    ////对话内容背景
    //public GameObject panelText;

    //角色图片列表
    //public List<Sprite> trans = new List<Sprite>();
    //角色名字对应图片的字典
    // Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();

    //当前对话索引值
    public int dialogIndex;

    //对话文本，按行分割
    public string[] dialogRows;

    //对话继续按钮
    public Button nextButton;

    //选项按钮预置体
    public GameObject optionButton;

    //选项按钮父节点，用于自动排列
    public Transform buttonGroup;

    //人物
    // public List<Person> people = new List<Person>();

    //礼物
    public List<Gift> gifts = new List<Gift>();
    Dictionary<string, Gift> giftDic = new Dictionary<string, Gift>();

    //门票
    public List<Ticket> tickets = new List<Ticket>();
    Dictionary<string, Ticket> ticketDic = new Dictionary<string, Ticket>();

    //游戏结束图片
    public SpriteRenderer gameOver;

    //分数文本
    public TMP_Text scoreText;
    //分数背景
    public GameObject panelScore;

    //提问者的动画信息
    public Animator questioner_ani;
    //回答者的动画信息
    public Animator respondent_ani;

    //记录显示完书籍后到多少题
    //public int flag = 0;

    //记录原先的场景
    //public static string originalSceneName;

    //public static DialogManager Instance { get; set; }//单例

    //Book
    public GameObject book;
    //返回按钮
    //public Button back;
    //关闭按钮
    public GameObject close;
    //翻书箭头
    public GameObject flip;
    //退出文本
    public TMP_Text overText;

    //排行榜
    public GameObject leaderBoard;
    //排行榜-你的排名
    public TMP_Text yourPosNum;
    public TMP_Text yourPosScore;
    //门票奖励
    public GameObject ticket;
    //拥有的门票-2
    // public GameObject myTicket2;
    // public GameObject myTicket5;
    // public GameObject myTicket8;

    //礼物
    // public GameObject giftLeft;
    // public GameObject giftMiddle;
    // public GameObject giftRight;

    //通知文本
    // public TMP_Text noti2Text;
    // public TMP_Text noti5Text;
    // public TMP_Text noti8Text;

    //分数显示
    public TMP_Text scoreNow;

    //答对个数
    public int trueNum = 0;
    //所得积分
    public int totalScore = 0;
    //题目总数
    int quesNum = 8;
    //礼物个数
    public int giftNum = 0;
    //问题编号
    // int questionIndex = 0;
    //连续答对数目的计数器
    public int continuousTrueNum = 0;
    //每次增加的积分
    public int plusScore = 0;

    //点击选项的音效
    public AudioSource clickOption;
    //答对的音效
    public AudioSource win;
    //答错的音效
    public AudioSource fail;

    //连续答对的音效
    public AudioSource double_kill;
    public AudioSource triple_kill;
    public AudioSource legendary;

    //展示礼物音效
    public AudioSource giftShow;
    //展示书的音效
    public AudioSource bookShow;
    //游戏结束音效
    public AudioSource gameoverSound;

    //粒子特效
    public GameObject doubleTrue;
    public GameObject trippleTrue;
    public GameObject multiTrue;
    // public GameObject loopGift;

    private void Awake()
    {
        //imageDic["questioner"] = trans[0];
        //imageDic["respondent"] = trans[1];
        //imageDic["gameover"] = trans[2];
        giftDic["gift-left"] = gifts[0];
        giftDic["gift-middle"] = gifts[1];
        giftDic["gift-right"] = gifts[2];
        ticketDic["ticket-2"] = tickets[0];
        ticketDic["ticket-3"] = tickets[1];
        ticketDic["ticket-multi"] = tickets[2];

    }

    // Start is called before the first frame update
    void Start()
    {
        //if (Instance != null)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(this);
        //}
        ClearScreen();
        ReadText(dialogDataFile);
        ShowDialogRow();

    }

    // Update is called once per frame
    void Update()
    {

    }

    //清屏，只剩下背景
    public void ClearScreen()
    {
        //back.gameObject.SetActive(false);
        close.gameObject.SetActive(false);
        book.gameObject.SetActive(false);
        panelScore.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        //quesPanel.gameObject.SetActive(false);
        //respPanel.gameObject.SetActive(false);
        //panelText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        //quesText.gameObject.SetActive(false);
        //respText.gameObject.SetActive(false);
        //dialogText.gameObject.SetActive(false);
        personLeft.gameObject.SetActive(false);
        personRight.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
        flip.gameObject.SetActive(false);
        overText.gameObject.SetActive(false);
        leaderBoard.gameObject.SetActive(false);
        ticket.gameObject.SetActive(false);
        // myTicket2.gameObject.SetActive(false);
        // myTicket5.gameObject.SetActive(false);
        // myTicket8.gameObject.SetActive(false);
        // giftLeft.gameObject.SetActive(false);
        // giftMiddle.gameObject.SetActive(false);
        // giftRight.gameObject.SetActive(false);
        // noti2Text.gameObject.SetActive(false);
        // noti5Text.gameObject.SetActive(false);
        // noti8Text.gameObject.SetActive(false);
        foreach (var value in giftDic.Values)
        {
            value.giftObj.gameObject.SetActive(false);
        }
        foreach (var value in ticketDic.Values)
        {
            value.ticketObj.gameObject.SetActive(false);
            // value.notification.gameObject.SetActive(false);
        }
        // foreach (string key in ticketDic.Keys)
        // {
        //     ticketDic[key].ticketObj.gameObject.SetActive(false);
        // }
        doubleTrue.gameObject.SetActive(false);
        trippleTrue.gameObject.SetActive(false);
        multiTrue.gameObject.SetActive(false);
        // loopGift.gameObject.SetActive(false);
    }

    //还原
    public void recoverScreen()
    {
        //panelText.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
        //dialogText.gameObject.SetActive(true);
        personLeft.gameObject.SetActive(true);
        personRight.gameObject.SetActive(true);
    }


    /// <summary>
    /// 更新文本信息
    /// </summary>
    /// <param name="_name">角色名字</param>
    /// <param name="_position">位置</param>
    /// <param name="_text">对话内容</param>
    public void UpdateInfo(string _name, string _position, string _text)
    {
        if (_position == "left")
        {
            //personLeft.gameObject.SetActive(true);
            respBubble.gameObject.SetActive(false);
            respText.gameObject.SetActive(false);
            quesBubble.gameObject.SetActive(true);
            quesText.gameObject.SetActive(true);
            quesText.text = _text;
            //personLeft.sprite = imageDic[_name];
            //personRight.gameObject.SetActive(false);
        }
        else if (_position == "right")
        {
            //personRight.gameObject.SetActive(true);
            respBubble.gameObject.SetActive(true);
            respText.gameObject.SetActive(true);
            quesBubble.gameObject.SetActive(false);
            quesText.gameObject.SetActive(false);
            respText.text = _text;
            //personRight.sprite = imageDic[_name];
            //personLeft.gameObject.SetActive(false);
        }
        //dialogText.text = _text;
    }

    //跳过引号中的逗号,进行逗号分隔(字段内容中的逗号不参与分隔)
    public string[] CSVstrToArry(string splitStr)
    {
        var newstr = string.Empty;
        List<string> sList = new List<string>();

        bool isSplice = false;
        string[] array = splitStr.Split(new char[] { ',' });
        foreach (var str in array)
        {
            if (!string.IsNullOrEmpty(str) && str.IndexOf('"') > -1)
            {
                var firstchar = str.Substring(0, 1);
                var lastchar = string.Empty;
                if (str.Length > 0)
                {
                    lastchar = str.Substring(str.Length - 1, 1);
                }
                if (firstchar.Equals("\"") && !lastchar.Equals("\""))
                {
                    isSplice = true;
                }
                if (lastchar.Equals("\""))
                {
                    if (!isSplice)
                        newstr += str;
                    else
                        newstr = newstr + "," + str;

                    isSplice = false;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(newstr))
                    newstr += str;
            }

            if (isSplice)
            {
                //添加因拆分时丢失的逗号
                if (string.IsNullOrEmpty(newstr))
                    newstr += str;
                else
                    newstr = newstr + "," + str;
            }
            else
            {
                sList.Add(newstr.Replace("\"", "").Trim());//去除字符中的双引号和首尾空格
                newstr = string.Empty;
            }
        }
        return sList.ToArray();
    }

    public void ReadText(TextAsset _textAsset)
    {
        dialogRows = _textAsset.text.Split('\n');
        Debug.Log("Success!!");
    }

    //展示文件内的对话内容文本
    public void ShowDialogRow()
    {
        recoverScreen();
        for (int i = 0; i < dialogRows.Length; i++)
        {
            questioner_ani.SetBool("book", false);
            respondent_ani.SetBool("book", false);
            string[] cells = CSVstrToArry(dialogRows[i]);
            if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)
            {
                UpdateInfo(cells[2], cells[3], cells[4]);

                dialogIndex = int.Parse(cells[5]);
                nextButton.gameObject.SetActive(true);
                break;
            }
            else if (cells[0] == "&" && int.Parse(cells[1]) == dialogIndex)
            {
                nextButton.gameObject.SetActive(false);
                GenerateOption(i);
            }
            else if (cells[0] == "BOOK" && int.Parse(cells[1]) == dialogIndex)
            {
                UpdateInfo(cells[2], cells[3], cells[4]);
                dialogIndex = int.Parse(cells[5]);
                nextButton.gameObject.SetActive(true);
                questioner_ani.SetBool("book", true);
                respondent_ani.SetBool("book", true);
                bookShow.Play();
                //flag = i;
                //originalSceneName = SceneManager.GetActiveScene().name;
                //DontDestroyOnLoad(this);
                //SceneManager.LoadScene(2);
                Debug.Log("Book!");
                //panelText.gameObject.SetActive(false);
                quesBubble.gameObject.SetActive(false);
                quesText.gameObject.SetActive(false);

                nextButton.gameObject.SetActive(false);
                close.gameObject.SetActive(true);
                //back.gameObject.SetActive(true);
                book.gameObject.SetActive(true);
                flip.gameObject.SetActive(true);

                break;
            }
            else if (cells[0] == "GIFT" && int.Parse(cells[1]) == dialogIndex)
            {
                UpdateInfo(cells[2], cells[3], cells[4]);
                dialogIndex = int.Parse(cells[5]);

                // loopGift.gameObject.SetActive(true);

                if (giftNum > 0)//有门票可以获得礼物
                {
                    questioner_ani.SetTrigger("show-gift");
                    respondent_ani.SetTrigger("surprised");
                    giftShow.Play();
                    nextButton.gameObject.SetActive(false);
                    if (giftNum == 1)
                    {
                        giftDic["gift-left"].giftObj.gameObject.SetActive(true);
                    }
                    else if (giftNum == 2)
                    {
                        giftDic["gift-left"].giftObj.gameObject.SetActive(true);
                        giftDic["gift-middle"].giftObj.gameObject.SetActive(true);
                    }
                    else if (giftNum == 3)
                    {
                        giftDic["gift-left"].giftObj.gameObject.SetActive(true);
                        giftDic["gift-middle"].giftObj.gameObject.SetActive(true);
                        giftDic["gift-right"].giftObj.gameObject.SetActive(true);
                    }
                }

                break;
            }
            else if (cells[0] == "END" && int.Parse(cells[1]) == dialogIndex)
            {
                Debug.Log("Over!!!");
                ClearScreen();
                //gameOver.sprite = imageDic["gameover"];
                gameOver.gameObject.SetActive(true);
                panelScore.gameObject.SetActive(true);
                scoreText.gameObject.SetActive(true);
                quesBubble.gameObject.SetActive(false);
                quesText.gameObject.SetActive(false);
                respBubble.gameObject.SetActive(false);
                respText.gameObject.SetActive(false);
                scoreNow.gameObject.SetActive(false);
                overText.gameObject.SetActive(true);
                gameoverSound.Play();
                // int finalScore = 0;
                // foreach (var person in people)
                // {
                //     if (person.name == "respondent")
                //     {
                //         finalScore = person.score;
                //     }
                // }
                scoreText.text = "Number of correct answers: " + trueNum.ToString();
                leaderBoard.gameObject.SetActive(true);
                if (trueNum < 2)
                {
                    yourPosNum.text = "No. 50";
                }
                else if (trueNum >= 2 && trueNum < 5)
                {
                    yourPosNum.text = "No. 45";
                }
                else if (trueNum == 5)
                {
                    yourPosNum.text = "No. 40";
                }
                else if (trueNum > 5 && trueNum < quesNum)
                {
                    yourPosNum.text = "No. 35";
                }
                else if (trueNum == quesNum)
                {
                    yourPosNum.text = "No. 30";
                }
                // trueNum = 0;//拿到门票置零，门票消失
            }
        }
    }


    public void OnClickNext()
    {
        // noti2Text.gameObject.SetActive(false);
        // noti5Text.gameObject.SetActive(false);
        // noti8Text.gameObject.SetActive(false);
        doubleTrue.gameObject.SetActive(false);
        trippleTrue.gameObject.SetActive(false);
        multiTrue.gameObject.SetActive(false);
        // loopGift.gameObject.SetActive(false);
        foreach (string key in ticketDic.Keys)
        {
            ticketDic[key].notification.text = "";
        }
        leaderBoard.gameObject.SetActive(false);
        ticket.gameObject.SetActive(false);
        ShowDialogRow();
        if (giftNum == 1)
        {
            ticketDic["ticket-2"].ticketObj.gameObject.SetActive(true);
        }
        else if (giftNum == 2)
        {
            ticketDic["ticket-3"].ticketObj.gameObject.SetActive(true);
        }
        else if (giftNum == 3)
        {
            ticketDic["ticket-multi"].ticketObj.gameObject.SetActive(true);
        }
    }

    //生成选项按钮
    public void GenerateOption(int _index)
    {
        string[] cells = CSVstrToArry(dialogRows[_index]);
        if (cells[0] == "&")
        {
            GameObject button = Instantiate(optionButton, buttonGroup);
            //绑定按钮事件
            button.GetComponentInChildren<TMP_Text>().text = cells[4];
            button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener
                (
                    delegate//添加委托
                    {
                        OnOptionClick(int.Parse(cells[5]));
                        clickOption.Play();
                        if (cells[6] != "")
                        {
                            string[] effect = cells[6].Split('@');
                            cells[7] = Regex.Replace(cells[7], @"[\r\n]", "");//去掉末尾的换行符
                            OptionEffect(effect[0], int.Parse(effect[1]), cells[7]);//计算得分
                        }
                    });
            GenerateOption(_index + 1);
        }
    }

    public void OnOptionClick(int _id)
    {
        dialogIndex = _id;
        ShowDialogRow();
        for (int i = 0; i < buttonGroup.childCount; i++)
        {
            Destroy(buttonGroup.GetChild(i).gameObject);
        }
    }

    //选项点击时带来的效果：计算得分
    public void OptionEffect(string _effect, int _param, string _target)
    {
        // questionIndex += 1;//每道题需要计算一次得分
        // Debug.Log(questionIndex);

        // if (_effect == "score+")
        // {
        //     foreach (var person in people)
        //     {
        //         if (person.name == _target)
        //         {
        //             person.score += _param;
        //         }
        //     }
        // }
        if (_param > 0)//答对
        {
            trueNum += 1;
            continuousTrueNum += 1;//连续答对计数器开始计数

            if (continuousTrueNum == 1)//答对一道题，一次积分+10
            {
                plusScore = 10;
                totalScore += plusScore;
            }
            else if (continuousTrueNum == 2)//连续答对2题，一次积分+20
            {
                plusScore = 20;
                totalScore += plusScore;
                doubleTrue.gameObject.SetActive(true);

                // if (person.score == 2)
                // {
                double_kill.Play();
                ticketDic["ticket-2"].notification.text = "Double kill! Come on!!!";
                // ticketDic["ticket-2"].notification.gameObject.SetActive(true);
                leaderBoard.gameObject.SetActive(true);
                yourPosNum.text = "No. 50";
                yourPosScore.text = totalScore.ToString();
                // ticket.gameObject.SetActive(true);
                // giftNum += 1;
                // }
            }
            else if (continuousTrueNum == 3)//连续答对3题，一次积分+30，并赠送一张门票
            {
                plusScore = 30;
                totalScore += plusScore;
                trippleTrue.gameObject.SetActive(true);

                // if (person.score == 3)
                // {
                triple_kill.Play();
                ticketDic["ticket-3"].notification.text = "Triple kill!\nHere is your gift ticket.";
                // ticketDic["ticket-3"].notification.gameObject.SetActive(true);
                leaderBoard.gameObject.SetActive(true);
                yourPosNum.text = "No. 45 (↑5)";
                yourPosScore.text = totalScore.ToString();
                ticket.gameObject.SetActive(true);
                giftNum += 1;
                // }
            }
            else if (continuousTrueNum > 3)//连续答对大于3题，一次积分+50
            {
                plusScore = 50;
                totalScore += plusScore;
                multiTrue.gameObject.SetActive(true);

                if (trueNum == quesNum)//若全部答对，送第三张门票
                {
                    legendary.Play();
                    ticketDic["ticket-multi"].notification.text = "Legendary!\nHere is your gift ticket.";
                    // ticketDic["ticket-multi"].notification.gameObject.SetActive(true);
                    leaderBoard.gameObject.SetActive(true);
                    yourPosNum.text = "No. 30 (↑15)";
                    yourPosScore.text = totalScore.ToString();
                    ticket.gameObject.SetActive(true);
                    giftNum += 1;
                }
            }

            // Debug.Log(giftNum);

            //多行注释option+shift+A
            /*  if (person.score == 2)
             {
                 ticketDic["ticket-2"].notification.gameObject.SetActive(true);
                 leaderBoard.gameObject.SetActive(true);
                 yourPosNum.text = "No. 20           "+person.totalScore;
                 ticket.gameObject.SetActive(true);
                 giftNum += 1;
             }
             if (person.score == 5)
             {
                 ticketDic["ticket-3"].notification.gameObject.SetActive(true);
                 leaderBoard.gameObject.SetActive(true);
                 yourPosNum.text = "No. 15 (↑5)           "+person.totalScore;
                 ticket.gameObject.SetActive(true);
                 giftNum += 1;
             }
             if (person.score == quesNum)
             {
                 ticketDic["ticket-multi"].notification.gameObject.SetActive(true);
                 leaderBoard.gameObject.SetActive(true);
                 yourPosNum.text = "No. 6 (↑9)           "+person.totalScore;
                 ticket.gameObject.SetActive(true);
                 giftNum += 1;
             } */

            questioner_ani.SetTrigger("true");
            respondent_ani.SetTrigger("true");
            win.Play();
        }
        else if (_param == 0)//答错
        {
            plusScore = 0;
            continuousTrueNum = 0;//连续答对的计数器置零
            questioner_ani.SetTrigger("false");
            respondent_ani.SetTrigger("false");
            fail.Play();
        }
        scoreNow.text = "Number of correct answers: " + trueNum.ToString()
                        + " Your current score is: " + totalScore.ToString() + " (+" + plusScore + ")";

    }

    //点击礼物效果
    public void OnClickGift(string giftName)
    {
        continuousTrueNum = 0;
        respondent_ani.SetTrigger("pickup");
        giftDic[giftName].giftObj.gameObject.SetActive(false);
        giftNum -= 1;//礼物个数减少一个
        if (giftNum == 0)
        {
            nextButton.gameObject.SetActive(true);
        }
        ticketDic.ElementAt(giftNum).Value.ticketObj.gameObject.SetActive(false);
    }

}
