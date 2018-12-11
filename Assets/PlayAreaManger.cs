using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaManger : MonoBehaviour {

	public Transform JumpObsticle;
    public Transform SideObsticle;
    public Vector3 JumpSpawn;
    public Vector3 RightSpawn;
    public Vector3 LeftSpawn;

    public bool JumpLastObsticle;

    private void Awake()
    {
        JumpLastObsticle = false; 
    }

    public void CreateObsticle()
    {
        if (JumpLastObsticle)
        {
            CreateSlideObsticel();
            JumpLastObsticle = false;
        }
        else
        {
            CreateJumpObsticle();
           // JumpLastObsticle = true;
        }
    }

    void CreateJumpObsticle()
    {
        Instantiate(JumpObsticle, JumpSpawn, Quaternion.identity);
    }

    void CreateSlideObsticel()
    {
        Instantiate(SideObsticle, RightSpawn, Quaternion.identity);
    }
}
