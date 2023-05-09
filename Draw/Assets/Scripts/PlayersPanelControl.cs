using UnityEngine;
using UnityEngine.UI;

public class PlayersPanelControl : MonoBehaviour
{
    public GameObject CurrentScin;

    [SerializeField] GameObject[] PlayersButton;

    [SerializeField] private Sprite selectButtun;
    [SerializeField] private Sprite unSelectButtun;

    public int[] RewardedScins;

    private void Start()
    {
        CheckPlayerPanel();
    }

    public void CheckPlayerPanel()
    {
        CurrentScin = PlayersButton[PlayerPrefs.GetInt("key_IndexHead", 0)];
        int kirill = PlayerPrefs.GetInt("UnlockLevel");

        for (int i = 0; i < PlayersButton.Length; i++)
        {
            RewardedScins[i] = PlayerPrefs.GetInt("RewardedScinsKey" + i.ToString(), 0);

            int lvlToUnlock = PlayersButton[i].GetComponent<PlayerItem>().levelToOpen;

            if (lvlToUnlock <= kirill || RewardedScins[i]==1)
            {
                PlayersButton[i].GetComponent<PlayerItem>().RefreshHead(true);
            }
            else
            {
                PlayersButton[i].GetComponent<PlayerItem>().RefreshHead(false);
            }

            if (i == PlayerPrefs.GetInt("key_IndexHead", 0))
            {
                PlayersButton[i].GetComponent<Image>().sprite = selectButtun;
            }
            else
            {
                PlayersButton[i].GetComponent<Image>().sprite = unSelectButtun;
            }
        }
    }



    public GameObject AdErrorPanel;

    public void CloseErrorPanel()
    {
        AdErrorPanel.SetActive(false);
    }    

}
