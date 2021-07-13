using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float Speed = 1900;                     //キャラクターのスピード
    private float Friction = 0.983f;                         //摩擦の数値

    private Vector2 startPos;                       //マウスを押したときのポジション
    private Vector2 BouncelastPos;                  //他オブジェクトに当たった時の座標を取得
    public int FlowFlag;                            //行動パターンの数値を格納する変数
    private enum ACTIONNUM {WAIT, READY, MOVE};     //行動パターンのNum
    private bool control;                           //現在キャラクターを操作出来るかどうか

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        FlowFlag = (int)ACTIONNUM.WAIT;             //最初は動かない
    }

    public void FixedUpdate()
    {
        this.BouncelastPos = this.rb.velocity;      //反射のベクトルをvelocityに代入
        Rubbing();                                  //摩擦を起こす
    }

    void Update()
    {

        if (control)
        {
            if (FlowFlag == (int)ACTIONNUM.READY)       //クリックされたら
            {
                ReadyAnimation();                       //横の揺れの動きの処理

            }
            //if(FlowFlag == (int)ACTIONNUM.MOVE)
            //{
            //    MoveAnimation();                        //ショットの動きの処理
            //}
            PushButton();                               //クリックされた座標を求める
            OffButton();                                //クリックして指を離した座標を求める
        }
        if(FlowFlag == (int)ACTIONNUM.WAIT)
        {
            WaitAnimation();                            
        }
        //Debug.Log(rb.velocity.magnitude);
    }

    //常に摩擦を起こす関数
    private void Rubbing()
    {
        this.rb.velocity *= Friction;
    }

    //クリックしてショットを放つための座標を取得
    private void PushButton()
    {
        if (Input.GetMouseButtonDown(0))                //マウスクリックしたら
        {
            FlowFlag = (int)ACTIONNUM.READY;            //引っ張るターンにする
            startPos = Input.mousePosition;
            Debug.Log(gameObject.transform.position);
        }
    }

    //そしてクリックした座標と指を離した座標を引いてショットの方角を決める
    public void OffButton()
    {
        if (Input.GetMouseButtonUp(0))                  //クリックを離したら
        {
            FlowFlag = (int)ACTIONNUM.MOVE;             //攻撃ターンにする
            Vector2 endPos = Input.mousePosition;       //現在のマウス位置のピクセル座標
            Vector2 startDirection = -1 * (endPos - startPos).normalized;
            rb.AddForce(startDirection * Speed/*,ForceMode2D.Impulse*/);
            Debug.Log("離されたよ");
        }
    }

    //何もしていない時の縦の動き
    private void WaitAnimation()
    {
        float sin = Mathf.Sin(Time.time * 6) * 0.08f;
        this.transform.position = new Vector2(gameObject.transform.position.x, sin - 1.5f);
    }
    //何もしていない時の横の動き
    private void ReadyAnimation()
    {
        float sin = Mathf.Sin(Time.time * 80) * 0.03f;
        this.transform.position = new Vector2(sin + gameObject.transform.position.x, gameObject.transform.position.y);
    }

    //ChangeChara.csで呼ばれる関数
    public void ChangeControl(bool controlFlag)         //現在操作しているキャラを変更する
    {
        control = controlFlag;                          //Changeマネージャーから操作可能のキャラ番号を受け取る
    }


    public void OnCollisionEnter2D(Collision2D other)
    {
        //★★★追加した反射をさせるための処理★★★
        Vector2 refrectVec = Vector2.Reflect(this.BouncelastPos, other.contacts[0].normal);
        this.rb.velocity = refrectVec;
    }

}
