using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct AngelRef
{
    public GameObject obj;
    public int index;
    public float curDist, curRad;
    public Vector2 targetPos;
    public EnemyController ec;

    public void setTargetPos(Vector2 _pos)
    {
        targetPos = _pos;
        if (ec)
            ec.targetPos = targetPos;
    }
    public AngelRef(GameObject _obj, int _index)
    {
        obj = _obj;
        index = _index;
        curDist = curRad = 0;
        targetPos = _obj.transform.position;
        ec = obj.GetComponent<EnemyController>();
        if(ec)
            ec.targetPos = targetPos;
    }

}


public class AngelsManager : MonoBehaviour {

    public int cnt = 7;
    float spacingX = 0.9F, topY = 7F, radius = 0.7F, vel = 2F, shootTime = 4F, curDist = 0F,maxDist;
    public bool reversed = false;
//    bool rightward = false;
    int moveCnt = 0;
    List<AngelRef> angelList = new List<AngelRef>();
    GameObject angel,musicNote;
    public float spawningDist = 8F;
    public enum State { spawning, moving, shooting ,leaving};
    public State state = State.spawning;

	// Use this for initialization
	void Start ()
    {
        angel = Resources.Load<GameObject>("Prefabs/Enemy/Angel");
        musicNote = Resources.Load<GameObject>("Prefabs/Bullet/EnemyBullet/MusicNote");

        maxDist = radius * Mathf.PI + spacingX * (cnt);
        for (int i = 0; i <= cnt; i++)
        {
            float x = spawningDist + spacingX * (i - 0.5F * cnt);
            if (reversed)
                x = -x;
            GameObject obj = GameObject.Instantiate(angel, new Vector3(x, topY), Quaternion.identity);
            angelList.Add(new AngelRef(obj, i));
        }
	}
    void Shoot(int index)
    {
        GameObject o = null;
        foreach (AngelRef i in angelList)
        {
            if ((i.index) == index)
                o = i.obj;
        }

        if (o)
        {
            Vector3 pos = o.transform.position,targetPos = 1000*Vector3.down;
            if (GameObject.Find("Player"))
                targetPos = GameObject.Find("Player").transform.position;
            Vector3 vec = (targetPos - pos).normalized;
            GameObject.Instantiate(musicNote, pos + 0.5F * vec, Quaternion.identity);
        }
    }

    IEnumerator ShootCorotine()
    {
        state = State.shooting;
        float interval = shootTime / (cnt + 4);
        yield return new WaitForSeconds(interval * 2F);
        for (int i = 0; i < cnt; i++)
        {
            Shoot(i);
            yield return new WaitForSeconds(interval);
        }
        Shoot(cnt);
        yield return new WaitForSeconds(interval * 2F);
        state = State.moving;
    }

    Vector2 CalcPos(int index)
    {
        Vector2 ret = Vector2.zero;
        float dist = curDist - index * spacingX;
        bool rightward = (moveCnt % 2 == 0);
        if (dist < 0F)
        {
            ret.x = spacingX * cnt * 0.5F + dist;
            ret.y = topY - radius * 2F * moveCnt;
        }
        else if (dist < radius * Mathf.PI)
        {
            float a = dist / radius;
            ret.x = spacingX * cnt * 0.5F + radius * Mathf.Sin(a);
            ret.y = topY - radius * (2F * moveCnt+1F - Mathf.Cos(a));
        }
        else
        {
            ret.x = spacingX * cnt * 0.5F - (dist - radius * Mathf.PI);
            ret.y = topY - radius * 2F * (moveCnt+1F);
        }
        if (rightward)
            ret.x = -ret.x;
        if(reversed)
            ret.x = -ret.x;
        return ret;
    }

    void DestroyAngels()
    {
        foreach (AngelRef i in angelList)
            if(i.obj)
                Destroy(i.obj);
    }

    // Update is called once per frame
    void Update ()
    {
        switch (state)
        {
            case State.spawning:
                spawningDist -= vel * Time.deltaTime;
                foreach (AngelRef i in angelList)
                {
                    float x = spawningDist + spacingX * (i.index - 0.5F * cnt);
                    if (reversed)
                        x = -x;
                    i.setTargetPos(new Vector2(x, topY));
                }
                if (spawningDist <= 0F)
                {
                    StartCoroutine(ShootCorotine());
                }
                break;

            case State.shooting:
                break;

            case State.moving:
                curDist += vel * Time.deltaTime;
                if (curDist >= maxDist)
                {
                    moveCnt++;
                    if (moveCnt <= 3)
                        StartCoroutine(ShootCorotine());
                    else
                        state = State.leaving;
                    curDist = 0F;
                    break;
                }
                foreach (AngelRef i in angelList)
                {
                    i.setTargetPos(CalcPos(i.index));
                }
                break;
            case State.leaving:
                curDist += vel * Time.deltaTime;
                bool rightward = (moveCnt % 2 == 1);
                float y = topY - 2 * moveCnt * radius;
                foreach (AngelRef i in angelList)
                {
                    float x = curDist + spacingX * (0.5F * cnt - i.index);
                    if (reversed)
                        x = -x;
                    if (!rightward)
                        x = -x;
                    i.setTargetPos(new Vector2(x, y));
                }
                if (curDist >= 10F)
                {
                    DestroyAngels();
                    Destroy(gameObject);
                }
                break;

        }
		
	}
}
