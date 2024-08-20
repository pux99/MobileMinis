using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class paintable : MonoBehaviour
{
    public Slider slider;
    public List<GameObject> images;
    public GameObject brush;
    public float brushSize = 1f;
    private List<GameObject> painting = new List<GameObject>();
    public RenderTexture render;
    public Texture2D texture2D;
    public int points;
    public int selectedImage;
    public Animator animator;
    public Image backgrownd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            
            var mouseposition =Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit= Physics2D.Raycast(mouseposition, -Vector2.up); ;
            if(hit.collider!=null)
            {
                var go= Instantiate(brush, mouseposition+new Vector3(0,0,10), Quaternion.identity,transform);
                go.transform.localScale = Vector3.one * brushSize;
                painting.Add(go);
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(cosave());
        }
        switch (selectedImage)
        {
            case 0:
                images[0].SetActive(true);
                images[1].SetActive(false);
                images[2].SetActive(false);
                break;
            case 1:
                images[0].SetActive(false);
                images[1].SetActive(true);
                images[2].SetActive(false);
                break; 
            case 2:
                images[0].SetActive(false);
                images[1].SetActive(false);
                images[2].SetActive(true);
                break;
        }

        
    }
    public int compate()
    {
        int similarityPoints=0;
        for (int i = 0;i<texture2D.width; i++)
        {
            for (int j = 0;j < texture2D.height; j++)
            {
                if (texture2D.GetPixel(i, j) != Color.black && images[selectedImage].GetComponent<SpriteRenderer>().sprite.texture.GetPixel(i,j)==Color.white)
                {
                    similarityPoints++;
                }
            }
        }
        Debug.Log(similarityPoints);
        return similarityPoints;
    }
    private IEnumerator cosave()
    {

        yield return new WaitForEndOfFrame();
        RenderTexture.active = render;
        texture2D = new Texture2D(render.width, render.height);
        texture2D.ReadPixels(new Rect(0, 0, render.width, render.height), 0, 0);
        texture2D.Apply();
        Debug.Log(texture2D.GetPixel(0,0));
        if (compate() > 600)
        {
            slider.value += .1f;
            selectedImage=Random.Range(0,3);
            animator.SetTrigger("cura");
        }
        for (int i = 0; i < painting.Count; i++)
        {
            Destroy(painting[i]);
        }
        painting.Clear();
    }
}
