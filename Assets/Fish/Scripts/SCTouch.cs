using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFish
{

    public class SCTouch : MonoBehaviour
    {
        GOCannon mCannon;

        void Start()
        {
            mCannon = GameObject.Find("Cannon").GetComponent<GOCannon>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))//鼠标或触摸事件  
            {
                if (!IsInvoking("DoFire"))
                {
                    InvokeRepeating("DoFire", 0.00f, 0.2f);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                CancelInvoke("DoFire");
            }
        }

        void DoFire()
        {
            mCannon.FirePre();
        }
    }
}
