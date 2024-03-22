using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrowPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void shootArrow(){
        //THIS is the code for getting the bow to spawn the arrow
        Instantiate(arrowPrefab, transform.position, Quaternion.identity);
    }
}
