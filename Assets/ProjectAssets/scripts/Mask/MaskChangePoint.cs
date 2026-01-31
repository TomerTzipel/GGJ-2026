using System.Collections;
using UnityEngine;

public class MaskChangePoint : MonoBehaviour
{
    //Event for OnTriggerPlayerChange
    [SerializeField] private MaskTypeEventChannel MaskChangeEC;
    [SerializeField] private SpriteRenderer _PointSpriteRenderer;
    [SerializeField] private MaskType _MyType;
    [SerializeField] private float _MaskChangeTimer = 2f;
    [SerializeField] private float _MaskChangeTimerReset = 25f;

    private Vector3 _hiddenPosition = new Vector3(0f, -10f, 0f);
    private WaitForSeconds _changeMaskActionTime;
    private Coroutine _maskChangeCoroutine;
    private bool _isPlayerInRange = false;
    private float _currentTimer = 0f;
    private float _currentTimerReset = 0f;

    private void Start()
    {
        //_PointSpriteRenderer.sprite = MaskSettings.GetShrineSpriteByType(_MyType);
        _changeMaskActionTime = new WaitForSeconds(_MaskChangeTimerReset);
    }
    private void Update()
    {
        if (GetCurrentTimerReset() <= 0 && _isPlayerInRange)//If player is in range and reset timer is not active
        {
            if (_currentTimer <= 0f && _maskChangeCoroutine == null) { ActivateMaskChange(); }//Time to change mask

            _currentTimer -= Time.deltaTime;//Countdown to change mask
        }

        if (GetCurrentTimerReset() > 0) { _currentTimerReset -= Time.deltaTime; }//Reset point timer after mask change
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayerInRange = true;
            ResetMaskChangeTimer();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayerInRange = false;

            //Reset timer when player leaves and point wasan't used
            if (GetCurrentTimerReset() <= 0f) { ResetMaskChangeTimer(); }
        }
    }

    private void ActivateMaskChange() { _maskChangeCoroutine = StartCoroutine(ActivateMaskPoint()); }

    private float GetCurrentTimerReset() { return _currentTimerReset; }
    private void ResetMaskChangeTimer() { _currentTimer = _MaskChangeTimer; }

    private IEnumerator ActivateMaskPoint()
    {
        //MaskChangeEvent.OnMaskChange?.Invoke(_MyType);
        MaskChangeEC.RaiseEvent(_MyType);
        _PointSpriteRenderer.transform.localPosition += _hiddenPosition;
        _currentTimerReset = _MaskChangeTimerReset;

        yield return _changeMaskActionTime;

        _PointSpriteRenderer.transform.localPosition = Vector3.zero;//Needs interpolation?
        _maskChangeCoroutine = null;
        ResetMaskChangeTimer();
    }

}