using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrecingLightsMinigame : MonoBehaviour
{
    public Image shield;
    public List<Button> buttons = new List<Button>();
    public GameObject buttonHolder;
    public TextMeshProUGUI pointCounter;
    public int points=0;
    public Animator animator;
    void Start()
    {
        for (int i = 0; i < buttonHolder.transform.childCount; i++) {
            buttons.Add(buttonHolder.transform.GetChild(i).GetComponent<Button>());
        }
    }
    private void OnEnable()
    {
        
        StartCoroutine("CountdownForLights");

    }
    private void OnDisable()
    {
        StopCoroutine("CountdownForLights");
        foreach (var button in buttons)
        {
            button.interactable = false;
        }
    }
    void Update()
    {
        pointCounter.text = points.ToString();
        if (points >=5)
        {
            shield.enabled = true;
            points = 0;
            animator.SetTrigger("defensa");
        }
    }

    public IEnumerator CountdownForLights()
    {
        yield return new WaitForEndOfFrame();
        int randomButton = Random.Range(0, buttons.Count);
        buttons[randomButton].interactable = true;
        yield return new WaitForSeconds(.7f);
        buttons[randomButton].interactable = false;
        yield return new WaitForSeconds(.7f);
        StartCoroutine(CountdownForLights());
    }
    public void AddPoints(int value)
    {
        points += value;
    }
    public void DeductPoints(int value)
    {
        points += value;
    }
}
