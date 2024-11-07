using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class HexManager : MonoBehaviour
{
    public List<HexManager> Adj = new List<HexManager>();
    public Rune rune;
    public ConectionTest Conection;
    // Start is called before the first frame update


    void Start()
    {
        Conection=this.gameObject.GetComponentInParent<ConectionTest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rune != null && rune.Energice) 
        {
            int coparador;
            for (int i = 0; i < rune.list.Count; i++)
            {
                coparador = i + 3;
                if(i>=3)//esta pergunta es para saber en que lugar de la lista estamos y ves contra que valor tenemos que comparar ya que 0 se compara con 3 que seria 0=i y 3=i+3 pero si la relacion es 3 que se compara con 0 3=i y 0 =i+3 pero esto da 6 asque si i>3 hay que restar en es de sumar
                {
                    coparador = i - 3;
                }
                if (rune.list[i] == 1 && Adj[i].rune.list[coparador] ==1)
                {
                    for(int j = 0;j < Conection.conections.Count;j++)
                    {
                        if ((Conection.conections[j].Energicer == rune && Conection.conections[j].Resiver == Adj[i].rune)||
                            (Conection.conections[j].Energicer == Adj[i].rune && Conection.conections[j].Resiver == rune))
                        { j= Conection.conections.Count;
                            break; }
                        if (j== Conection.conections.Count-1 &&
                            !(Conection.conections[j].Energicer == rune && Conection.conections[j].Resiver == Adj[i].rune) &&
                            !(Conection.conections[j].Energicer == Adj[i].rune && Conection.conections[j].Resiver == rune))
                        {
                            Conection.addConection(rune, Adj[i].rune, i);
                            Adj[i].rune.Energice = true;
                            Debug.Log(rune + " " + Adj[i].rune + " " + i);
                        }
                        
                    }
                    if (Conection.conections.Count == 0)
                    {
                        Conection.addConection(rune, Adj[i].rune, i);
                        Adj[i].rune.Energice = true;
                    }
                    
                }
            }
        }
    }
    public void Conect(int conection)
    {

    }
    public void InicializeHex(HexManager[] adjacent)
    {
        for (int i = 0; i < adjacent.Length; i++)
            Adj.Add(adjacent[i]);
    }
}
