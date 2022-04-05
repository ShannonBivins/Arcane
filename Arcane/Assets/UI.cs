using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class UI : MonoBehaviour
{
    public GameObject upgradePanel;
    public Button[] upgradeBtns;
    public GameObject player;
    public GameObject deathScreen;
    public Button restartBtn;
    public PlayerInput playerInput;
    public TMP_Text killCount;
    public int kills = 0;

    void Start()
    {
        foreach(Button btn in upgradeBtns)
        {
            btn.onClick.AddListener(delegate{FinishUpgrade(EventSystem.current.currentSelectedGameObject);});
        }

        restartBtn.onClick.AddListener(RestartScene);
        killCount.text = "Kills: " + kills.ToString();
    }

    public void StartUpgrade()
    {
        List<LvUpgrade> upgrades = new List<LvUpgrade>();

        foreach(LvUpgrade upgrade in this.GetComponent<UpgradeSystem>().upgrades)
        {
            upgrades.Add(upgrade);
        }

        List<LvUpgrade> choices = new List<LvUpgrade>();

        int i = 0;
        for(i = 0; i < 3; i++)
        {
            int j = Random.Range(0, upgrades.Count);
            choices.Add(upgrades[j]);
            upgrades.RemoveAt(j);
        }

        i = 0;
        foreach(LvUpgrade choice in choices)
        {
            upgradeBtns[i].GetComponentInChildren<Text>().text = choice.name;
            i++;
        }

        upgradePanel.SetActive(true);
        Time.timeScale = 0f;
        playerInput.SwitchCurrentActionMap("Menus");
    }

    public void FinishUpgrade(GameObject btn)
    {
        this.gameObject.GetComponent<UpgradeSystem>().Upgrade(btn);
        upgradePanel.SetActive(false);
        playerInput.SwitchCurrentActionMap("Gameplay");
        Time.timeScale = 1f;
    }
 
    public void OnDeath()
    {
        deathScreen.SetActive(true);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
