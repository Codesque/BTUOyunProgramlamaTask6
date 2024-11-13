using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{

    [SerializeField] private float ThresholdY = 7f;


    // Start is called before the first frame update
    void Start()
    {
    }


    private void Update()
    {
        // Hareketin 1birim/saniye hizinda blend olmus bir sekilde gitmesi icin alttaki satiri ekleyip rutini sildim.
        transform.position += Vector3.up * Time.deltaTime;

        // lazerin konum vektorunun y degeri "ThresholdY" esik degerinden buyukse laser objesini yok et.
        if(transform.position.y > ThresholdY) Destroy(gameObject);
    }




}
