using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runeChanger : MonoBehaviour
{
    public List<GameObject> list = new List<GameObject>();
    public int runeMap;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0;i< transform.childCount;i++)
        {
            if (transform.GetChild(i).GetComponent<ConectionTest>() != null )
                list.Add(transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeRune()
    {
        list[runeMap].SetActive(false);
        runeMap = Random.Range(0, list.Count);
        list[runeMap].SetActive(true);
    }
}
