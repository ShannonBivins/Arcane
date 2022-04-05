using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Characters : MonoBehaviour
{
    public Character plain;
    public Character pyromancer;

    [Serializable] public class Character
    {
        public List<GameObject> spells;
    }
}