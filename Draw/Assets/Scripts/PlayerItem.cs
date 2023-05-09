using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    public Image bgImage;

    public Sprite selectSpr, unSelectSpr;

    public GameObject lockObj;
    public GameObject adObj;

    public int levelToOpen;
    private bool isOpen;

    public int IndexHead;

    public AdManager am;

    public GameObject AdErrorPanel;

    public void SelectHead()
    {
        if (isOpen)
        {
            transform.parent.gameObject.GetComponent<PlayersPanelControl>().CurrentScin.GetComponent<Image>().sprite = unSelectSpr;
            PlayerPrefs.SetInt("key_IndexHead", IndexHead);
            GetComponent<Image>().sprite = selectSpr;
            transform.parent.gameObject.GetComponent<PlayersPanelControl>().CheckPlayerPanel();
            Debug.Log(PlayerPrefs.GetInt("key_IndexHead"));
        }
        else
        {
            if(am.ShowVideoAd())
            {
                RefreshHead(true);
                PlayerPrefs.SetInt("RewardedScinsKey" + IndexHead.ToString(), 1);
            }
            else
            {
                AdErrorPanel.SetActive(true);
            }
        }
    }

    public void RefreshHead(bool isDone)
    {
        if (!isDone)
        {
            lockObj.SetActive(true);
            adObj.SetActive(true);
            isOpen = false;
        }
        else
        {
            lockObj.SetActive(false);
            adObj.SetActive(false);
            isOpen = true;
        }
    }
}