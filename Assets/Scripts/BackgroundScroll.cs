using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {

    public float distanceAway;

    private float offset;

    Renderer render;
    private void Start()
    {
        offset = 0.0f;
        render = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (manager.gameState == GameState.GAME_OVER)
            return;

        offset += manager.travelSpeed / distanceAway * Time.deltaTime;
        render.material.SetTextureOffset("_MainTex", new Vector2(offset, 0.0f));
	}
}
