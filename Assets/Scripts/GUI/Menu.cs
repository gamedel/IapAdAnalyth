using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Menu : MonoBehaviour
{

    IapAdAnalyth iapAdAnalyth;


    // Start is called before the first frame update
    void Start()
    {
        iapAdAnalyth = FindAnyObjectByType<IapAdAnalyth>();
    }

    public void GoToScene(string scenePath)
    {
        GlobalObj.GoToScene(scenePath);
    }

    public void BuyConsumable()
    {
        iapAdAnalyth?.BuyConsumable();
    }

    public void BuyNonConsumable()
    {
            iapAdAnalyth?.BuyNonConsumable();
    }


    public void ShowRewardVideo()
    {
            iapAdAnalyth?.ShowRewardVideo();
    }

}
