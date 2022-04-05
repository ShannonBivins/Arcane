using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpellSystem : MonoBehaviour
{
    public GameObject gameManager;
    [HideInInspector] public List<GameObject> allSpells;
     public List<GameObject> unlockedSpells;
    [HideInInspector] public List<GameObject> assignedSpells;

    void Awake()
    {
        switch(gameObject.GetComponent<PlayerController>().character)
        {
            case PlayerController.Character.Default:
            {
                allSpells = gameManager.GetComponent<Characters>().plain.spells;
                break;
            }
            case PlayerController.Character.Pyromancer:
            {
                allSpells = gameManager.GetComponent<Characters>().pyromancer.spells;
                break;
            }
        }
    }

    void Start()
    {
        UnlockSpell(allSpells[0]);
    }

    void OnSpell1()
    {
        if(assignedSpells.ElementAtOrDefault(0))
        {
            if(!assignedSpells[0].GetComponent<SpellStats>().cooldown)
            {
                CastSpell(assignedSpells[0]);
            }
        }
    }

    void OnSpell2()
    {
        if(assignedSpells.ElementAtOrDefault(1))
        {
            CastSpell(assignedSpells[1]);
        }
    }

    void OnSpell3()
    {
        if(assignedSpells.ElementAtOrDefault(2))
        {
            CastSpell(assignedSpells[2]);
        }
    }

    void OnSpell4()
    {
        if(assignedSpells.ElementAtOrDefault(3))
        {
            CastSpell(assignedSpells[3]);
        }
    }

    void OnSpell5()
    {
        if(assignedSpells.ElementAtOrDefault(4))
        {
            CastSpell(assignedSpells[4]);
        }
    }

    public void UnlockSpell(GameObject spell)
    {
        spell = Instantiate(spell, transform);
        spell.SetActive(false);
        spell.transform.parent = this.gameObject.transform;
        unlockedSpells.Add(spell);

        if(!spell.GetComponent<SpellStats>().passive && assignedSpells.Count < 10)
        {
            AssignSpell(spell, assignedSpells.Count);
        }
        else if(spell.GetComponent<SpellStats>().passive)
        {
            spell.GetComponent<ICast>().Cast(transform);
        }
    }

    public void AssignSpell(GameObject spell, int i)
    {
        assignedSpells.Insert(i, spell);
    }

    void CastSpell(GameObject spell)
    {
        if(!spell.GetComponent<SpellStats>().cooldown)
        {
            var inter = spell.GetComponent<ICast>();
            inter.Cast(transform);
            StartCoroutine(Cooldown(spell));
        }
    }

    IEnumerator Cooldown(GameObject spell)
    {
        spell.GetComponent<SpellStats>().cooldown = true;
        yield return new WaitForSeconds(spell.GetComponent<SpellStats>().coolTime);
        spell.GetComponent<SpellStats>().cooldown = false;
    }
}