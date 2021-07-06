using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeChara : MonoBehaviour
{
    //現在操作しているキャラ
    private int nowChara;
    //操作可能なキャラ
    [SerializeField]
    private List<GameObject> CharaList;
    // Start is called before the first frame update
    void Start()
    {
        CharaList[0].GetComponent<PlayerMove>().ChangeControl(true);
    }

    // Update is called once per frame
    void Update()
    {
        //デバック用のEnterキー
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    ChangeCharacter(nowChara);
        //}
        if(CharaList[nowChara].GetComponent<PlayerMove>().FlowFlag == 2)
        {
            ChangeCharacter(nowChara);
        }
    }
    //操作キャラを変更する関数
    void ChangeCharacter(int tempNowChara)
    {
        //現在操作しているキャラを動かなくする
        CharaList[tempNowChara].GetComponent<PlayerMove>().ChangeControl(false);
        //次のキャラクターの番号にする
        int nextChara = tempNowChara + 1;
        if(nextChara >= CharaList.Count)
        {
            nextChara = 0;
        }
        //次のキャラクターを動かせるようにする
        CharaList[nextChara].GetComponent<PlayerMove>().ChangeControl(true);
        //現在のキャラクター番号を保持する
        nowChara = nextChara;
    }
}
