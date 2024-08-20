using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rune : MonoBehaviour
{
    // Start is called before the first frame update
    public bool goalRune;
    public Slider goalSlider;
    public List<int> list = new List<int>();
    public bool Energice;
    public bool Rotable;
    public HexManager hexManager;
    public ConectionTest conectionTest;
    public SpriteRenderer spriteRenderer;
    public bool atacking=false;
    public Animator animator;
    public runeChanger changer;
    void Start()
    {
        hexManager = this.gameObject.GetComponentInParent<HexManager>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Energice)
        {
            spriteRenderer.color = Color.yellow;
            
        }
        else
        {
            spriteRenderer.color = Color.white;
            if(goalRune)
            {
                spriteRenderer.color = new Vector4(.5f,.1f,.2f,1);
            }
        }
            

        if (Energice&&goalRune&&goalSlider!=null&&!atacking)
        {
            StartCoroutine(atack());

        }
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    RotateRight();
        //}
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    RotateLeft();
        //}
    }
    private void OnMouseDown()
    {
        RotateRight();
    }
    public void RotateRight()
    {
        if (Rotable)
        {
            gameObject.transform.Rotate(new Vector3(0, 0, -60));
            list.Insert(0, list[5]);
            list.RemoveAt(6);
            if (Energice)
            {
                conectionTest.checkConection();
            }
        }
        
    }
    public void RotateLeft()
    {
        if (Rotable)
        {
            gameObject.transform.Rotate(new Vector3(0, 0, 60));
            list.Add(list[0]);
            list.RemoveAt(0);
            if (Energice)
            {
                conectionTest.checkConection();
            }
        }
    }
    IEnumerator atack()
    {
        atacking=true;
        animator.SetTrigger("ataque");
        yield return new WaitForSeconds(.5f);
        goalSlider.value -= .25f;
        conectionTest.RotateAll();
        atacking=false;
        changer.ChangeRune();
    }
}
