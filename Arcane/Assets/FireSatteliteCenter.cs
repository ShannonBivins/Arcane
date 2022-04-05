using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSatteliteCenter : MonoBehaviour, ICast
{
    // Start is called before the first frame update
    Quaternion rotation;
    public GameObject satellite;
    public SpellStats stats;

    void Awake()
    {
        rotation = transform.rotation;
    }

    void Start()
    {
        
    }

    public void Cast(Transform transform)
    {
        gameObject.SetActive(true);
    }

    public void LevelUp()
    {
        stats.level++;
        GameObject newSat = Instantiate(satellite, transform);
        newSat.transform.RotateAround(transform.position, Vector3.forward, 180f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = rotation;
    }
}