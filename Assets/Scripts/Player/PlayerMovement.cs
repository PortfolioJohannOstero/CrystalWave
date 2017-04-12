using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    [Header("Movement")]
    public float speed = 10.0f;
    public float speedIncrease = 0.002f;
    public float distance = 50.0f;

    [Header("Bounciness")]
    public float bounceDistance;
    public float bounceAwaySpeed;
    public float bounceBackSpeed;
    public float bounceAwayFudgeFactor;
    public float bounceBackFudgeFactor;

    [Header("Points")]
    public int specialPointAmount = 3;

    // movement
    private float timestep;
    private Vector3 startPos;
    private bool moving;
    private float curve;

    // points and player control
    private Player player;
    private bool pointAdded;

    // player state
    delegate void playerState();
    playerState currentState;

    // bounce
    private Vector3 origPos;
    private Vector3 targetPos;
    private bool isBouncing;
    private float prevCurve;
    playerState bounceState;

    delegate float WaveMethod();
    WaveMethod CurveGetter;

    private void Start()
    {
        player = GetComponent<Player>();
        pointAdded = false;
        moving = true;
        startPos = transform.position;
        currentState = IdleState;

        timestep = 0;

        // bounce
        bounceState = NoBounce;
        isBouncing = false;

        CurveGetter = GetSineWave;
    }

    // states
    void IdleState()
    {
        UpdateCurv();
        if (ControlInput.Tap())
        {
            player.gameManager.StartGame();
            currentState = RunningState;
            player.uiHandler.startGameCollection.SetActive(false);
        }
    }

    void RunningState()
    {
        if (ControlInput.Tap())
        {
            moving = !moving;

            if(!isBouncing && !moving)
            {
                bounceState = BounceOn;
                isBouncing = true;
            }
        }
        if (moving)
        {
            prevCurve = curve;
            curve = UpdateCurv();

            TryAddPoints(curve);
            PopulateBounce(curve);
        }

        speed += speedIncrease * Time.deltaTime;
        bounceState();   
    }
	void Update ()
    {
        currentState();
	}

    void NoBounce() { }

    void PopulateBounce(float curve)
    {
        float dir = (curve - prevCurve > 0.0f) ? 1.0f : -1.0f;

        origPos = transform.position;

        targetPos = origPos;
        targetPos.y += bounceDistance * dir;
    }

    void BounceOn()
    {
        Vector3 pos = transform.position;
        float step = Mathf.SmoothStep(0.0f, (targetPos - pos).magnitude, bounceAwaySpeed * Time.deltaTime);

        pos = Vector3.Lerp(pos, targetPos, step);
        transform.position = pos;

        if ((pos - targetPos).magnitude < bounceAwayFudgeFactor)
        {
            bounceState = BounceOff;
        }
    }

    void BounceOff()
    {
        Vector3 pos = transform.position;
        float step = Mathf.SmoothStep(0.0f, (origPos - pos).magnitude, bounceAwaySpeed * Time.deltaTime);
        pos = Vector3.Lerp(pos, origPos, step);
        transform.position = pos;

        if ((pos - origPos).magnitude < bounceBackFudgeFactor)
        {
            bounceState = NoBounce;
            isBouncing = false;
        }
    }

    float UpdateCurv()
    {
        timestep += Time.deltaTime;

        float curve = CurveGetter();
        float step = Mathf.SmoothStep(0.0f, distance, speed * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, startPos + Vector3.up * curve * distance, step);

        return curve;
    }

    // Waves
    float GetSineWave()
    {
        return Mathf.Sin(speed * timestep);
    }

    float GetCosineWave()
    {
        return Mathf.Cos(speed * timestep);
    }

    void TryAddPoints(float curve)
    {
        if (manager.gameState == GameState.START_GAME)
            return;

        if (Mathf.Abs(curve) > 0.999f)
        {
            if (!pointAdded)
            {
                player.AddPoint(specialPointAmount);
                pointAdded = true;

                player.audioSource.PlayOneShot(player.topBottomSound, 1.0f);
            }
        }
       else
            pointAdded = false;
    }
}
