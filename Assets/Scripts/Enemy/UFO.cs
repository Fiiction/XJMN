using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour {

    public float  angularVelocity,phaseDegree;
    float cenX = 0F, cenY = 13.5F, radiusX = 4.5F, radiusY = 2F;
    public float waitTime = 2F, aimTime = 1.5F, beamTime = 1.5F,firstWait = 6F;
    bool isAiming = false,arrivedTargetPos;
    float aimAlpha;
    public GameObject Aim, Beam;
    public Rigidbody2D body;

    float phase;
    Vector2 targetPos;
    // Use this for initialization

    private void Awake()
    {
        phase = Mathf.Deg2Rad * phaseDegree;
        targetPos.x = cenX + radiusX * Mathf.Cos(phase);
        targetPos.y = cenY + radiusY * Mathf.Sin(phase);

        transform.position = targetPos;
    }

    void Start () {
        Beam.SetActive(false);
        body = GetComponent<Rigidbody2D>();
        //StartCoroutine(BeamCoroutine());
	}

    void StartWait()
    {
        Beam.SetActive(false);
    }
    void StartAim()
    {
        isAiming = true;
    }
    void StartBeam()
    {
        isAiming = false;
        aimAlpha = 0;
        Beam.SetActive(true);
    }

    IEnumerator BeamCoroutine()
    {
        yield return new WaitForSeconds(firstWait);
        while(true)
        {
            StartAim();
            yield return new WaitForSeconds(aimTime);
            StartBeam();
            yield return new WaitForSeconds(beamTime);
            StartWait();
            yield return new WaitForSeconds(waitTime);

        }
        
    }

    void FixedUpdate()
    {
        body.AddTorque(-0.2F * body.rotation);
    }

	// Update is called once per frame
	void Update () {

        if (!arrivedTargetPos)
        {
            cenY -= 0.8F * Time.deltaTime;
            if (cenY <= 6.5F)
            {
                StartCoroutine(BeamCoroutine());
                arrivedTargetPos = true;
            }
        }


        if (isAiming)
        {
            aimAlpha += Time.deltaTime / aimTime;
        }
        Aim.GetComponent<SpriteRenderer>().color = new Color(1F, 1F, 1F, aimAlpha);

        phaseDegree += angularVelocity * Time.deltaTime;
        phase = Mathf.Deg2Rad * phaseDegree;

        targetPos.x = cenX + radiusX * Mathf.Cos(phase);
        targetPos.y = cenY + radiusY * Mathf.Sin(phase);

        GetComponent<EnemyController>().targetPos = targetPos;
	}
}
