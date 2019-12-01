using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CrawlerTrap : Trap
{

    //CONSTRUCTOR:
    public CrawlerTrap(GameObject Prefab, GameObject Start, GameObject End) : base()
    {

        CrawlerPrefab = Prefab;
        SetStartObject(Start);
        SetEndObject(End);
        type = TrapType.CRAWLER;
    }
    //PUBLIC:
    public void SetStart(Vector3 temp)
    {
        StartPoint = temp;
    }
    public void SetEnd(Vector3 temp)
    {
        EndPoint = temp;
    }
    public void SetStartObject(GameObject temp)
    {
        StartObject = temp;
        StartPoint = StartObject.transform.position;
        StartRotation = StartObject.transform.eulerAngles;
    }

    public void SetEndObject(GameObject temp)
    {
        EndObject = temp;
        EndPoint = EndObject.transform.position;
    }
    public Vector3 GetStart()
    {
        return StartPoint;
    }
    public Vector3 GetEnd()
    {
        return EndPoint;
    }
    public override void Initiate()
    {
        CreateCrawler = IsLerping ? false : true;
    }

    public void Initiate(float lerpTime)
    {
        if (!IsLerping)
        {
            CreateCrawler = true;
            if (lerpTime > 0)
            {
                LerpTime = lerpTime;
            }
        }
    }
    public void Update()
    {

        if (CreateCrawler)
        {
            IsLerping = true;
            CurrentCrawler = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Chupa"),
                StartPoint, Quaternion.Euler(StartRotation)); //MonoBehaviour.Instantiate(CrawlerPrefab);
            CurrentCrawler.transform.position = StartPoint;
            CurrentCrawler.transform.eulerAngles = StartRotation;
            CreateCrawler = false;
        }

        if (IsLerping)
        {
            bool reset = false;
            LerpParam += (1 * Time.deltaTime) / LerpTime;
            if (LerpParam >= 1)
            {
                LerpParam = 1;
                IsLerping = false;
                reset = true;
            }
            CurrentCrawler.transform.position = Vector3.Lerp(StartPoint, EndPoint, LerpParam);

            LerpParam = reset ? 0 : LerpParam;
        }
    }



    //PRIVATE:
    private GameObject StartObject;
    private GameObject EndObject;


    private Vector3 StartPoint;
    private Vector3 EndPoint;

    private Vector3 StartRotation;
    private GameObject CrawlerPrefab;

    private GameObject CurrentCrawler;

    private bool CreateCrawler = false;
    private bool IsLerping = false;
    private float LerpParam = 0;
    private float LerpTime = 1;
}
