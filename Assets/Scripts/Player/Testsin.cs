using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Testsin : MonoBehaviour
{
    Text text;
    public GameObject TextBox;
    // Start is called before the first frame update
    void Start()
    {
        text = TextBox.GetComponent<Text>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        float sin = Mathf.Sin(Time.time);
        text.text = "sin値は" + sin;
    }
    void Update()
    {
        
    }
}
