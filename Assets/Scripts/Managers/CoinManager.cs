using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{

    public Text coinText;
    public int coinCount;

    public static CoinManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        coinCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = coinCount + "";
    }
    public void updateCoin() 
    {
        coinCount = coinCount + 1;
    }
}
