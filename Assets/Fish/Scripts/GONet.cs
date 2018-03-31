using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFish
{

    public class GONet : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            GetComponent<SpriteRenderer>().sortingOrder = 1000;
            Destroy(gameObject, 0.6f);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
