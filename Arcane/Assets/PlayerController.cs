using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public TMP_Text lvText;
    public int playerLevel = 1;
    public float exp = 0f;
    float lvExp = 100f;
    public GameObject gameManager;
    public float health = 100f;

    public enum Character{ Default, Pyromancer };
    public Character character;

    public PlayerInput playerInput;

    public GameObject pauseTxt;
    public bool paused = false;


    void Start()
    {
        lvText.text = "Level: " + playerLevel.ToString();
    }

    public void UpdateExp(float expGain)
    {
        exp += expGain;

        if(exp >= lvExp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        playerLevel++;
        lvExp *= 2;
        lvText.text = "Level: " + playerLevel.ToString();
        gameManager.GetComponent<UI>().StartUpgrade();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == 8)
        {
            health -= other.gameObject.GetComponent<EnemyStats>().damage;
        }
        else if(other.gameObject.layer == 9)
        {
            health -= other.gameObject.GetComponent<EnemyProjectile>().damage;
        }

        if(health <= 0)
        {
            Destroy(gameObject);
            gameManager.GetComponent<UI>().OnDeath();
        }
    }

    void OnPause()
    {
        if(gameManager.GetComponent<UI>().upgradePanel.active)
        {
            return;
        }

        if(!paused)
        {
            Time.timeScale = 0f;
            pauseTxt.SetActive(true);
            playerInput.SwitchCurrentActionMap("Menus");
            paused = true;
        }
        else
        {
            paused = false;
            pauseTxt.SetActive(false);
            playerInput.SwitchCurrentActionMap("Gameplay");
            Time.timeScale = 1f;
        }
    }
}
