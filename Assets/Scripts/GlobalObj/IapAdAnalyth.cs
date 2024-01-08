using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;
using Mycom.Tracker.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class IapAdAnalyth : MonoBehaviour, IDetailedStoreListener, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener//, IRewardedVideoAdListener
{
    public bool IsIapTest = true;
    public bool IsUnityAdsTest = true;
    public string ConsumableProduct = "coins";
    public string NonConsumableProduct = "vip";
    public string UnityAdsGameId = "5520694";
    public string UnityAdsPlacement = "Rewarded_Android";
    public string MyTrackerKey = "14679322622160768519";

    IStoreController m_StoreController;

    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        MyTracker.Init(MyTrackerKey);
#endif
        SceneManager.sceneLoaded += OnSceneLoaded;
        Advertisement.Initialize(UnityAdsGameId, IsUnityAdsTest, this);
       
        InitializePurchasing();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        MyTrackerSendEvent("MyEvent", "LoadScene", scene.name);
    }


        public void MyTrackerSendEvent(string trackEvent,string string1,string string2)
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
        Dictionary<string, string> parameters = new Dictionary<string, string>() { { string1,string2 } };
        MyTracker.TrackEvent(trackEvent, parameters);
         #endif
    }


    void AddCoins()
    {
        GlobalObj.Coins++;
        GlobalObj.GUIRefresh();
    }

    void AddVip()
    {
        GlobalObj.IsVip = true;
        GlobalObj.GUIRefresh();
    }




    #region Iap


    public void BuyConsumable()
    {
        m_StoreController.InitiatePurchase(ConsumableProduct);
    }

    public void BuyNonConsumable()
    {
        m_StoreController.InitiatePurchase(NonConsumableProduct);
    }


    void InitializePurchasing()
    {
        if (IsIapTest) StandardPurchasingModule.Instance().useFakeStoreAlways = true;
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //Add products that will be purchasable and indicate its type.
        builder.AddProduct(ConsumableProduct, ProductType.Consumable);
        builder.AddProduct(NonConsumableProduct, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
    }


    void IStoreListener.OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("In-App Purchasing successfully initialized");
        m_StoreController = controller;
    }

    void IStoreListener.OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    void IStoreListener.OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }

    void IDetailedStoreListener.OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        throw new System.NotImplementedException();
    }

    void IStoreListener.OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs args)
    {
        var product = args.purchasedProduct;

        
        //Add the purchased product to the players inventory
        if (product.definition.id == ConsumableProduct)
        {
            AddCoins();
            MyTrackerSendEvent("MyEvent", "IAP_Successful", product.definition.id);
        }
        
        if (product.definition.id == NonConsumableProduct)
        {
            AddVip();
            MyTrackerSendEvent("MyEvent", "IAP_Successful", product.definition.id);
        }

        Debug.Log($"Purchase Complete - Product: {product.definition.id}");


        return PurchaseProcessingResult.Complete;
    }


    #endregion



    #region UnityAds

    public void ShowRewardVideo()
    {
        Advertisement.Show(UnityAdsPlacement,this);
    }

    void IUnityAdsInitializationListener.OnInitializationComplete()
    {
        Advertisement.Load(UnityAdsPlacement, this);
    }

    void IUnityAdsInitializationListener.OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
    }

    void IUnityAdsLoadListener.OnUnityAdsAdLoaded(string placementId)
    {
    }

    void IUnityAdsLoadListener.OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
    }

    void IUnityAdsShowListener.OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
    }

    void IUnityAdsShowListener.OnUnityAdsShowStart(string placementId)
    {
        Advertisement.Load(UnityAdsPlacement, this);
    }

    void IUnityAdsShowListener.OnUnityAdsShowClick(string placementId)
    {
    }

    void IUnityAdsShowListener.OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == UnityAdsPlacement)
        {
            AddCoins();
        }
    }
    
    #endregion

}
