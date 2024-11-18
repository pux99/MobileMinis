using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Minigames.Drawing
{
    public class paintable : MonoBehaviour
    {
        [SerializeField] private  List<GameObject> images;
        [SerializeField] private  GameObject brush;
        [SerializeField] private  float brushSize = 1f;
        [SerializeField] private  List<GameObject> painting = new List<GameObject>();
        [SerializeField] private  RenderTexture render;
        [SerializeField] private  Texture2D texture2D;
        [SerializeField] private  int points;
        [SerializeField] private  int selectedImage;
        [SerializeField] private  Image backgrownd;
        public Action WinMinigame;
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
                    images[0].transform.parent.gameObject.SetActive(true);
                    images[1].transform.parent.gameObject.SetActive(false);
                    images[2].transform.parent.gameObject.SetActive(false);
                    images[3].transform.parent.gameObject.SetActive(false);
                    images[4].transform.parent.gameObject.SetActive(false);
                    break;
                case 1:
                    images[0].transform.parent.gameObject.SetActive(false);
                    images[1].transform.parent.gameObject.SetActive(true);
                    images[2].transform.parent.gameObject.SetActive(false);
                    images[3].transform.parent.gameObject.SetActive(false);
                    images[4].transform.parent.gameObject.SetActive(false);
                    break; 
                case 2:
                    images[0].transform.parent.gameObject.SetActive(false);
                    images[1].transform.parent.gameObject.SetActive(false);
                    images[2].transform.parent.gameObject.SetActive(true);
                    images[3].transform.parent.gameObject.SetActive(false);
                    images[4].transform.parent.gameObject.SetActive(false);
                    break;
                case 3:
                    images[0].transform.parent.gameObject.SetActive(false);
                    images[1].transform.parent.gameObject.SetActive(false);
                    images[2].transform.parent.gameObject.SetActive(false);
                    images[3].transform.parent.gameObject.SetActive(true);
                    images[4].transform.parent.gameObject.SetActive(false);
                    break;
                case 4:
                    images[0].transform.parent.gameObject.SetActive(false);
                    images[1].transform.parent.gameObject.SetActive(false);
                    images[2].transform.parent.gameObject.SetActive(false);
                    images[3].transform.parent.gameObject.SetActive(false);
                    images[4].transform.parent.gameObject.SetActive(true);
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
                //slider.value += .1f;
                WinMinigame?.Invoke();
                ChangeDrawing();
                //animator.SetTrigger("cura");
            }
            for (int i = 0; i < painting.Count; i++)
            {
                Destroy(painting[i]);
            }
            painting.Clear();
        }

        public void ChangeDrawing()
        {
            selectedImage=Random.Range(0,5);
        }
    }
}
