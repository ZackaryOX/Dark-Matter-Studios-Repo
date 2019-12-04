﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CrawlerTrap : Trap
{

    //CONSTRUCTOR:
    public CrawlerTrap()
    {
        type = TrapType.CRAWLER;
    }
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

    public void Initiate(float lerpTime, FMOD.Studio.EventInstance damage)
    {
        if (!IsLerping)
        {
            CreateCrawler = true;
            

            MyDamageSound = damage;
            if (lerpTime > 0)
            {
                LerpTime = lerpTime;
            }
        }
    }
    public void Update(PhotonView trapnodeview)
    {
        if (Ghost.AllGhosts.Count > 0)
        {
            PhotonView temp = Ghost.AllGhosts[0].GetObject().GetComponent<PhotonView>();
            if (CreateCrawler)
            {
                IsLerping = true;
                CurrentCrawler = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Chupa"),
                    StartPoint, Quaternion.Euler(StartRotation)); //MonoBehaviour.Instantiate(CrawlerPrefab);
                CurrentCrawler.transform.position = StartPoint;
                CurrentCrawler.transform.eulerAngles = StartRotation;
                CreateCrawler = false;

                FMODUnity.RuntimeManager.AttachInstanceToGameObject(MyDamageSound, CurrentCrawler.GetComponent<Transform>(), CurrentCrawler.GetComponent<Rigidbody>());
                trapnodeview.RPC("PlayAudio", RpcTarget.AllBuffered);
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
                    PhotonNetwork.Destroy(CurrentCrawler);
                    trapnodeview.RPC("StopAudio", RpcTarget.AllBuffered);
                }
                CurrentCrawler.transform.position = Vector3.Lerp(StartPoint, EndPoint, LerpParam);

                LerpParam = reset ? 0 : LerpParam;



                //TESTING
                if (temp.IsMine)
                {
                    Player Target = Player.AllPlayers[0];
                    Transform TargetsHeadtrans = Target.GetHead().transform;
                    Transform Targetstrans = Target.GetObject().transform;
                    Transform Casterstrans = CurrentCrawler.transform;
                    float Divider = 2;
                    int layerMask = 1 << 11;
                    layerMask = ~layerMask;
                    RaycastHit hit;
                    Vector3 Direction = Targetstrans.position - Casterstrans.position;
                    float Distance = Vector3.Distance(Casterstrans.position, Targetstrans.position);
                    if (!Physics.Raycast(Targetstrans.position, Direction, out hit,
                       Distance, layerMask))
                    {
                        float thisFramesDamage = 0;

                        if (Vector3.Angle(TargetsHeadtrans.forward, Casterstrans.position - TargetsHeadtrans.position) <= 60)
                        {


                            thisFramesDamage += LookDamage * Time.deltaTime;

                        }

                        if (Distance <= AOERadius)
                        {

                            thisFramesDamage += AOEDamage * Time.deltaTime;

                            if (Target.GetWalkSpeed() != Target.GetDefaultSpeed() / Divider)
                            {
                                temp.RPC("SetTargetSpeed", RpcTarget.AllBuffered, Target.GetDefaultSpeed() / Divider);
                            }

                        }
                        else if (Target.GetWalkSpeed() != Target.GetDefaultSpeed())
                        {
                            temp.RPC("SetTargetSpeed", RpcTarget.AllBuffered, Target.GetDefaultSpeed());
                        }


                        if (thisFramesDamage > 0)
                        {
                            float CurrentSanityToSet = 0;
                            float SanityToTest = Target.GetSanity() - thisFramesDamage;
                            if (SanityToTest > 0.0f)
                            {
                                CurrentSanityToSet = SanityToTest;
                            }
                            temp.RPC("SetTargetSanity", RpcTarget.AllBuffered, CurrentSanityToSet);
                        }
                    }
                }

            }
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
    private float LerpTime = 5f;
    private float LookDamage = 12.5f;
    private float AOERadius = 20f;
    private float AOEDamage = 12.5f;
    private FMOD.Studio.EventInstance MyDamageSound;



}
