using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private Transform parent;





    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(SpawnRoutine));
    }



    
    public void SpawnEnemy() {


        // Dusmani 7f yukarida ve -7f ile 7f arasinda bir yere spawnlamak icin pozisyon lokal degiskeni yarattik.
        Vector3 position = Vector3.right * UnityEngine.Random.Range(-7f, 7f) + Vector3.up * 7f;

        // Instantiate(GameObject prefab , Vector3 position , Quaternion rotation) fonksiyonuyla oyun sahnesine prefabler yuklenebilir
        // Instantiate(enemyPrefab , position , Quaternion.identity);

        // Spesifik bir objenin child'i olarak objeyi yaratmak icin asagidaki fonksiyon kullanilmalidir.
        Instantiate(enemyPrefab, position, Quaternion.identity, parent);

    
    }



    // Bu fonksiyon cagirildiginda tum spawnlanma islemleri durur.
    public void OnGameOver() { StopAllCoroutines(); }

    

    // Bu rutin her 5 saniyede bir dusman objesi spawnlar. 
    // Calisma zamani "yield" ifadesine geldiginde ondan sonra yazilan sey birden fazla frame'de devam eder.
    // yield new WaitForNewSeconds(5f) , 5 saniye gecene kadar birden fazla frame'de calisir
    IEnumerator SpawnRoutine() {

        while (true)
        {
            // 5 saniye bekle
            yield return new WaitForSeconds(5f);

            // Dusmani spawnla
            SpawnEnemy();
        }
    
    
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
