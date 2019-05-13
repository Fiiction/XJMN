using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurBrushGenerator : MonoBehaviour {

    HashSet<GameObject> BBSet, tmp;
    Stack<GameObject> des;
    GameObject BB;
    Rigidbody2D body;
    float angle = 0F;
    int cnt = 0;
    public int NumOfBlur = 24;
    public float Radius = 1.0F;
    public Color BlurCoverColor = Color.blue;
    Vector3 totPos = Vector3.zero, curPos;

    void Generate()
    {
        float r = Random.Range(0.1F, Radius);
        float a = Random.Range(0F, 6.2832F);
        Vector3 pos = curPos + new Vector3(Mathf.Cos(a), Mathf.Sin(a)) * r;
        if(cnt>0)
            pos += (curPos - (totPos / cnt));
        GameObject obj = GameObject.Instantiate(BB, pos, Quaternion.identity);
        obj.transform.rotation = Quaternion.Euler(0,0,angle + Random.Range(-30F, 30F));
        BBSet.Add(obj);
        cnt++;
        obj.GetComponent<SpriteRenderer>().material.SetColor("_CC", BlurCoverColor);
        totPos += obj.transform.position;
    }

    void Remove(GameObject obj)
    {
        cnt--;
        totPos -= obj.transform.position;
        BBSet.Remove(obj);
        obj.GetComponent<BlurBrush>().fadeOut = true;
    }
    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
        BB = Resources.Load<GameObject>("Prefabs/Others/BlurBrush");
        BBSet = new HashSet<GameObject>();
        tmp = new HashSet<GameObject>();
        des = new Stack<GameObject>();
        for (int i = 1; i <= NumOfBlur; i++)
        {
            Generate();
        }
    }
	
	// Update is called once per frame
	void Update () {
        tmp = BBSet;
        des.Clear();
        curPos = transform.position + (Vector3)body.velocity * 0.2F;
        foreach (GameObject i in tmp)
        {
            
            if ((curPos - i.transform.position).magnitude >= Radius * 1.25F)
            {
                des.Push(i);
                //Generate();
            }
        }

        while (des.Count > 0)
        {
            var i = des.Pop();
            Remove(i);
            Generate();
        }

        angle += Time.deltaTime * 45F;
    }
}
