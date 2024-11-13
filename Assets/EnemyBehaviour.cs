using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 velocity = Vector2.down;

    // OnTriggerEnter , bu objenin sahip oldugu trigger colliderlardan biri eger baska bir colliderla temas ederse ,
    // ilk olarak ve collider basina bir defa olacak sekilde bu fonksiyon calisir. 
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Tagler sayesinde de bu ayrisim yapilabilir ancak
        // GetComponent<T>() methodunu gostermek adina bu sekilde yazdim.
        
        // Other (temas eden collider)'daki BeanController komponentini cek
        BeanController  player = other.GetComponent<BeanController>();

        // Other (temas eden collider)'daki LaserBehaviour komponentini cek
        LaserBehaviour laser = other.GetComponent<LaserBehaviour>();

        // Eger temas eden colliderda BeanController komponenti varsa bu komponentteki TakeDamage() methodunu calistir
        if (player != null) player.TakeDamage(10);


        // Eger temas eden colliderda LaserBehaviour komponenti varsa ...
            // Once laser objesini yok et 
            // sonra bu scripte sahip olan objeyi (yani kendimizi) yok et.
        if (laser != null) {
            Destroy(laser.gameObject);
            Destroy(this.gameObject);

        }

    }


  


    // Start is called before the first frame update
    void Start()
    {

        // Bu  komponente(EnemyBehaviour) bagli objenin Rigidbody komponentini al ve rb nesne degiskenine ata. 
        rb = GetComponent<Rigidbody2D>();

        // Eger GetComponent<Rigidbody>() null donduyse error ver.
            // !(null) == !(false) == true
        if (!rb) throw new System.Exception("There is not any rigidbody in " + transform.gameObject.name + "  enemyPrefab");
        
        
        // Fizik bazli harekete ihtiyacimiz olmadigi icin Rigidbody kinematik olmali.
        // Unityde olusturulan rigidbody(3d) varsayilan olarak Dinamik Rigidbody olusturur.
        // Bunu degistirmek icin isKinematic nesne degiskenini false'tan true'ya degistirmeliyiz.
        rb.isKinematic = true;

        // Yer cekimi nesneye etki etmemelidir.
        // rb.useGravity = false; 3d de gecerli 
        rb.gravityScale = 0;

        // Oyun basladiginda rastgele bir pozisyonda basla.
        rb.position = Vector3.right * UnityEngine.Random.Range(-7f, 7f) + Vector3.up * 7f;


        // Bu objedeki collider komponentine eris. Eger komponent yoksa null don.
        Collider coll = GetComponent<Collider>(); 
        // Eger collider komponenti bulunduysa bu collider'i trigger collider yap
        if(coll != null) coll.isTrigger = true;

    }



    // Fixed Update fiziksel hesaplamalarda kullanilir.
    private void FixedUpdate()
    {
        // Kinematik hareket algoritmasi cismin pozisyonunu hiz vektoru kullanarak degistirir. 
        // Kinematik rigidbodylerin pozisyonuna direkt atama yapilmaktan kacinilmalidir. 
        // Sebebi , yapilan direkt atamalar objenin isinlanarak gidiyormus gibi gozukmesine neden olur. (jitering)
        // Ayrica diger scriptlerin cismin hareketine etki etmesi zorlasir.
        rb.position += velocity * Time.deltaTime;

        // Rigidbodynin pozisyonu -7den kucukse yatay ekseni rastgele olacak sekilde ekranin ust kismina ata.
            // Vector3.right * ...Random.Range(-7f,7f) : x degeri -7 , 7 arasinda rastgele sayi olacak sekilde =>Vector3(x,0,0);
            // Vector3.up * 7f = (0,7f,0); 
            // (x,0,0) + (0f,7f,0f) = (x,7f,0f);
        if (rb.position.y < -7f) rb.position = Vector3.right * UnityEngine.Random.Range(-7f, 7f) + Vector3.up * 7f;
        
        
    }
}
