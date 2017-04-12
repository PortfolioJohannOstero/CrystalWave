using UnityEngine;
using System.Collections;


public class CrystalSpawner : MonoBehaviour {

    public Crystal[] crystalPrefabs;
    public Color[] crystalColours;
    public float spawnRate;
    public float spawnChance;

    [Header("Placement")]
    public Transform distanceFromCentre;

    private Bounds bounds;
    private float mLastTime;


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

    private void Update()
    {
        if (manager.gameState == GameState.GAME_OVER || manager.gameState == GameState.START_GAME)
            return;

        if(Time.time - mLastTime > spawnRate)
        {
            mLastTime = Time.time;

            float percent = Random.Range(0.0f, 100.0f);
            if(percent <= spawnChance)
                Spawn();
        }
    }

    void Spawn()
    {
        GameObject crystal = Instantiate(GetRandomCrystal().gameObject) as GameObject;
        Crystal crystalComb = crystal.GetComponent<Crystal>();
        SpriteRenderer crystalRenderer = crystal.GetComponent<SpriteRenderer>();

        int colourIndex = Random.Range(0, crystalColours.Length - 1);
        crystalRenderer.material.color = crystalColours[colourIndex];

        // attempt to flip
        SetFlip(crystal);

        // Crystal orientation
        int topOrBottom = SetOrientation(crystal);
        
        // Crystal init placement
        Vector3 placementPos = distanceFromCentre.position;
        placementPos.y *= topOrBottom;

        crystal.transform.position = placementPos;
        crystal.transform.Translate(0.0f, crystalComb.height * topOrBottom, 0.0f, Space.World);
    }

    Crystal GetRandomCrystal()
    {
        return crystalPrefabs[Random.Range(0, crystalPrefabs.Length - 1)];
    }

    void SetFlip(GameObject obj)
    {
        float percent = Random.Range(0.0f, 100.0f);
        if(percent < 50.0f)
        {
            Vector3 scale = obj.transform.localScale;
            scale.x = -scale.x;
            obj.transform.localScale = scale;
        }
    }

    int SetOrientation(GameObject obj)
    {
        float orientation = Random.Range(0.0f, 100.0f);

        if (orientation < 50.0f)
        {
            obj.transform.Rotate(Vector3.forward, 180.0f);
            return -1;
        }

        return 1;
    }
}
