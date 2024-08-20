using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConectionTest : MonoBehaviour
{
    public class Conection
    {
        public Rune Energicer;
        public Rune Resiver;
        public int conection;
    }
    public List<Conection> conections=new List<Conection>();
    public List<Rune> runes=new List<Rune>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            runes.Add(transform.GetChild(i).GetChild(0).GetComponent<Rune>());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addConection(Rune enegicer,Rune resiver, int num)
    {
        Conection conection = new Conection();
        conection.Energicer = enegicer;
        conection.Resiver = resiver;
        conection.conection = num;
        conections.Add(conection);
    }
    public void checkConection()
    {
        for (int i = 0; i < conections.Count; i++)
        {
            int coparador = conections[i].conection + 3;
            if (conections[i].conection >= 3)//esta pergunta es para saber en que lugar de la lista estamos y ves contra que valor tenemos que comparar ya que 0 se compara con 3 que seria 0=i y 3=i+3 pero si la relacion es 3 que se compara con 0 3=i y 0 =i+3 pero esto da 6 asque si i>3 hay que restar en es de sumar
            {
                coparador = conections[i].conection - 3;
            }
            if (conections[i].Energicer.list[conections[i].conection] != 1 || conections[i].Resiver.list[coparador] != 1|| !conections[i].Energicer.Energice)
            {
                conections[i].Resiver.Energice = false;
                conections.RemoveAt(i);
                Debug.Log(conections.Count);
                i -= 1;
            }
        }
        for (int i = 0; i < conections.Count; i++)
        {
            conections[i].Resiver.Energice = true;
        }
    }
    public void RotateAll()
    {
        foreach(Rune rune in runes)
        {
            if (rune.Rotable)
            {
                for (int i = 0; i < Random.Range(0, 5); i++)
                {
                    rune.RotateLeft();
                }
            }
        }
    }
    
}
