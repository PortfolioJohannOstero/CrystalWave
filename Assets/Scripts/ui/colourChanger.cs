using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class colourChanger : MonoBehaviour {

    public Color[] colours;
    public float colourChangeSpeed;
    public float fudgeFactor = 0.2f;

    private int index;
    private Text text;

    private void Start()
    {
        index = 0;

        text = GetComponent<Text>();
        text.color = colours[index];

        index++;
    }
    	
	// Update is called once per frame
	void Update ()
    {
        Color c = text.color;
        Color c2 = colours[index];

        Vector3 cv = new Vector3(c.r, c.g, c.b);
        Vector3 cv2 = new Vector3(c2.r, c2.g, c2.b);

        cv = Vector3.Lerp(cv, cv2, Mathf.SmoothStep(0.0f, 1.0f, colourChangeSpeed * Time.deltaTime));
        text.color = new Color(cv.x, cv.y, cv.z);

        if ((cv - cv2).magnitude < fudgeFactor)
        {
            index++;
            if (index > colours.Length - 1)
                index = 0;
        }

	}
}
