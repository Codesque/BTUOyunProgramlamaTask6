using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanController : MonoBehaviour
{

    // Olusturdugumuz lazer prefabini Project tabinden Inspector tabindeki "Laser Prefab" yazisinin yanina surukleyin
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private int Speed = 10;
    [SerializeField] private float Cooldown = 2f;

    [Header("Threshold")]
    public float thresholdX = 10f; 
    public float thresholdY = 10f;



    float lastShotTime;


    SpawnerManager spawnerManager;
    // Start is called before the first frame update
    void Start()
    {
        // Oyunun baslangicinda bekleme suresi kadar beklememesi icin -Cooldown 'a esitledim.
        lastShotTime = -Cooldown;

        // GameObject.FindObjectOfType<T>() hiyerarsi tabindeki T tipindeki ilk objeyi dondurur.
        spawnerManager = GameObject.FindObjectOfType<SpawnerManager>();

    }


    // Saglik puani nesne degiskeni
    public float Health { get; private set; } = 100f;

    // damageAmount : Hasar Miktari
    public void TakeDamage(float damageAmount) {

        // Saglik puani zaten 0'in altindaysa hasar alma. 
        if (Health <= 0) return;
        


        // Saglik puanini damageAmount kadar azalt.
        Health -= damageAmount;
        //  Eger yeni saglik puani 0dan kucukse spawnerManagerdaki spawn coroutinelerini durdurur.Aksi halde alinan hasari yazdir.
        if (Health <= 0) spawnerManager.OnGameOver(); else Debug.Log("Player took " + damageAmount + "amount of damage."); 

    }



    public void Movement()
    {
        // transform.position.x , transform.position.y , transfrom.position.z  r-value degerler oldugu icin direkt atama yapilamaz.
        // Bu yuzden x , y , z bilesenlerine direkt atama yapmak istiyorsak transform.position nesne degiskenini bir lokal degiskene
        // atamamiz gerekli
        Vector3 pos = transform.position;

        // "Horizontal" == yatay eksen girdisini  && "Vertical" == dikey eksen girdisini temsil eder. 
        // Edit -> Project settings -> Input Manager dan bu eksenleri hangi tuslarin etkiledigini kontrol edebilirsiniz.
        Vector3 input = Vector3.right * Input.GetAxis("Horizontal") + Vector3.up * Input.GetAxis("Vertical");

        // Machine independent yapmak icin yer degistirmeyi (1/fps) ile carptim
        pos += input * Speed * Time.deltaTime;

        // Threshold X , Y esik degerlerini geciyorsa esik degerin 1 gerisine at. 
        pos.x = (pos.x >= thresholdX) ? 1 - thresholdX : (pos.x <= -thresholdX) ? thresholdX -1 : pos.x;
        pos.y = (pos.y >= thresholdY) ? 1 - thresholdY : (pos.y <= -thresholdY) ? thresholdY - 1 : pos.y;

        
        transform.position = pos;


    }




    public void ShootLaser()
    {
        // Space tusuna basildiginda laser prefabini bu objenin bulundugu pozisyonun 1 birim yukarisinda olustur
        // Quaternion.identity => laserPrefabinin orijinal rotasyon degerlerini bozmadan yuklenmesini sagliyor burada.

        // Time.time oyunun baslangicindan itibaren gecen sureye karsilik gelir. 
        // Seri atislar bekleme suresi icerisindeyken lastShotTime + Cooldown > Time.time. 
        // Bekleme suresi bittiginde lastShotTime + Cooldown <= Time.time
        if(Input.GetKeyDown(KeyCode.Space) && lastShotTime + Cooldown < Time.time)
        {
            Instantiate(laserPrefab , transform.position + Vector3.up , Quaternion.identity);
            lastShotTime = Time.time;
        }
    }




    // Update is called once per frame
    void Update()
    {
        Movement();
        ShootLaser();
    }
}
