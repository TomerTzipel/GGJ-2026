using UnityEngine;

public class MaskChangePoint : MonoBehaviour
{
    //Event for OnTriggerPlayerChange
    [SerializeField] private SpriteRenderer _PointSpriteRenderer;
    [SerializeField] private MaskType _MyType;
    [SerializeField] private float _MaskChangeTimer = 1.5f;
    [SerializeField] private float _MaskChangeTimerReset = 2f;

    private bool _isPlayerInRange = false;
    private float _currentTimer = 0f;
    private float _currentTimerReset = 0f;

    private void Start()
    {
        //_PointSpriteRenderer.color = MaskTypeColor.GetColorFromMaskType(_MyType);
    }
    private void Update()
    {
        if (_currentTimerReset <= 0 && _isPlayerInRange)
        {
            if (_currentTimer <= 0f)//Time to change mask
            {
                //Trigger Mask Change Event
                ResetMaskChangeTimer();
                _currentTimerReset = _MaskChangeTimerReset;
            }

            _currentTimer -= Time.deltaTime;//Countdown to change mask
        }

        if (_currentTimerReset > 0)//Reset point timer after mask change
        {
            _currentTimerReset -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayerInRange = true;
            ResetMaskChangeTimer();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayerInRange = false;

            if (_currentTimerReset <= 0f)//Reset timer when player leaves and point wasan't used
            {
                ResetMaskChangeTimer();
            }
        }
    }

    private void ResetMaskChangeTimer() { _currentTimer = _MaskChangeTimer; }

}