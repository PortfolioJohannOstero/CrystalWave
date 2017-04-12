using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Crystal : MonoBehaviour {

    public float colourChangeSpeed = 2.0f;
    public float brightness = 10.0f;

    Bounds spriteBound;
    SpriteRenderer renderer;

    Vector3[] targetColours;
    int targetIndex;

    public float height
    {
        get { return spriteBound.max.y; }
    }

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        spriteBound = renderer.bounds;
    }

    private void Start()
    {
        targetColours = new Vector3[2];

        Color bc = renderer.material.color;
        targetColours[0] = new Vector3(bc.r, bc.g, bc.b);

        targetColours[1] = targetColours[0];
        targetColours[1].x += brightness;
        targetColours[1].y += brightness;
        targetColours[1].z += brightness;

        targetIndex = 0;
    }

    // Update is called once per frame
    void Update ()
    {
        float stateConversion = (float)manager.gameState;
        transform.Translate(stateConversion  * (-manager.travelSpeed * Time.deltaTime), 0.0f, 0.0f, Space.World);

        UpdateColour();
    }

    void UpdateColour()
    {
        float deltaSpeed = colourChangeSpeed * Time.deltaTime;

        Color c = renderer.material.color;
        Vector3 currentColour = new Vector3(c.r, c.g, c.b);

        if ((currentColour - targetColours[targetIndex]).magnitude < 0.2f)
        {
            targetIndex++;
            if (targetIndex > targetColours.Length-1)
                targetIndex = 0;
        }

        currentColour = Vector3.Lerp(currentColour, targetColours[targetIndex], deltaSpeed);

        renderer.material.color = new Color(currentColour.x, currentColour.y, currentColour.z, 1.0f);
    }
}
