using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UpgradeSystem : MonoBehaviour
{
    public GameObject player;

    [HideInInspector] public List<LvUpgrade> upgrades = new List<LvUpgrade>()
    {
        new LvUpgrade("+Attack", "stat"),
        new LvUpgrade("+Speed", "stat"),
        new LvUpgrade("+Health", "stat"),
    };

    void Awake()
    {
        List<GameObject> spells = new List<GameObject>();

        switch(player.GetComponent<PlayerController>().character)
        {
            case PlayerController.Character.Default:
            {
                spells = gameObject.GetComponent<Characters>().plain.spells;
                break;
            }
            case PlayerController.Character.Pyromancer:
            {
                spells = gameObject.GetComponent<Characters>().pyromancer.spells;
                break;
            }
        }

        foreach(GameObject spell in spells)
        {
            if(spell == spells[0])
            {
                if(spell.GetComponent<SpellStats>().level < spell.GetComponent<SpellStats>().maxLevel)
                {
                    upgrades.Add(new LvUpgrade("+" + spell.name, "enhance"));
                }
            }
            else
            {
                upgrades.Add(new LvUpgrade(spell.name, "unlock"));
            }
        }
    }

    public void Upgrade(GameObject btn)
    {
        LvUpgrade upgrade = upgrades.Where(obj => obj.name == btn.GetComponentInChildren<Text>().text).SingleOrDefault();

        if(upgrade.type == "stat")
        {
            if(btn.GetComponentInChildren<Text>().text == "+Speed")
            {
                player.GetComponent<PlayerMovement>().speedMultiplier *= 1.1f;
            }

            else if(btn.GetComponentInChildren<Text>().text == "+Attack")
            {
                player.GetComponent<PlayerMovement>().attackMultiplier *= 1.1f;
            }

            else if(btn.GetComponentInChildren<Text>().text == "+Health")
            {
                player.GetComponent<PlayerController>().health *= 1.1f;
            }
        }

        else if(upgrade.type == "unlock")
        {
            GameObject spell = player.GetComponent<SpellSystem>().allSpells.Where(obj => obj.name == upgrade.name).SingleOrDefault();
            player.GetComponent<SpellSystem>().UnlockSpell(spell);
            upgrades.Remove(upgrade);

            if(spell.GetComponent<SpellStats>().maxLevel > 1)
            {
                upgrades.Add(new LvUpgrade("+" +  spell.name, "enhance"));
            }
        }

        else if(upgrade.type == "enhance")
        {
            GameObject spell = player.GetComponent<SpellSystem>().unlockedSpells.Where(obj => obj.name == upgrade.name.Substring(1) + "(Clone)").SingleOrDefault();
            spell.GetComponent<ICast>().LevelUp();
            
            if(spell.GetComponent<SpellStats>().level == spell.GetComponent<SpellStats>().maxLevel)
            {
                upgrades.Remove(upgrade);
            }
        }
    }
}

public class LvUpgrade
{
    public string name;
    public string type;

    public LvUpgrade(string upgradeName, string upgradeType)
    {
        name = upgradeName;
        type = upgradeType;
    }
}