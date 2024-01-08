using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;

public class GlobalObj : MonoBehaviour
{
    public static int Coins = 0;
    public static bool IsVip = false;
    public static MyGUI GUIInstance=null;

    public static void GoToScene(string scenePath)
    {
        Addressables.LoadSceneAsync(scenePath);
    }
    public static void GUIRefresh()
    {
        if (GUIInstance != null)
        {
            if (GUIInstance.CoinsObject != null) GUIInstance.CoinsObject.GetComponent<TMPro.TMP_Text>().text = $"Coins: {Coins}";
            if (GUIInstance.VipObject != null)
            {
                GUIInstance.VipObject.GetComponent<TMPro.TMP_Text>().text = IsVip?"<color=green>VIP</color>": "<color=red>Not VIP</color>";
            }
        }
    }



    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        GUIInstance=FindAnyObjectByType<MyGUI>();
        GUIRefresh();
    }





    bool first = false;
    void Awake()
    {
        DontDestroyOnLoad(this);
        if (FindObjectsOfType(GetType()).Length == 1) first = true;
        if (!first) Destroy(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
}
