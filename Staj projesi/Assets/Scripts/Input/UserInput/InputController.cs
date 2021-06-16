using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

// TODO: Release ederken kaldırılacak
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
using Input = CustomInput.Input;
#endif

public class InputController : MonoBehaviour
{
    private PlayerController _playerController;
    //private Managers _managers;
    private Vector2 _initialTouchPosition;
    private int _initialFingerId;
    private Vector2 _endTouchPosition;
    private bool _isCircle;

    #region Swipe and Tap
    private float _deltaPosXCalculationThreshold = 25f;
    private bool _isXDeltaPosition;
    private float _xDeltaPositionValue;

    private float _deltaPosYCalculationThreshold = 25f;
    private bool _isYDeltaPosition;
    private float _yDeltaPositionValue;

    private float _deltaPositionCalculationThreshold = 25f;
    private float _deltaPositionMagnitude;

    public float _angleTreshold;
    private Vector2 _touchVector;
    private float _touchAngle;
    #endregion

    #region Circle
    private float _dotProduct;
    private float _cosTetha;
    private float _currentAngle;
    private float _totalAngle;

    public List<Vector2> _touchDataset;
    public Vector2 _currentTempVector;
    public Vector2 _prevTempVector;

    private float _circleAcceptedAngleTreshold = 30f;
    #endregion

    private float _touchElapsedTime;
    private float _touchSwipeTimeTreshold = 0.25f;

    private GlobalVariables.TouchTypes _touchType;

    private void Awake()
    {
    }

    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _touchDataset = new List<Vector2>();
    }

    void Update()
    {
        this.GetUserInput();
    }

    private void GetUserInput()
    {
        if (Input.touchCount > 0)
        {
            //if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) || EventSystem.current.IsPointerOverGameObject(-1))
            //    return;

            if (Input.touches[0].phase.Equals(TouchPhase.Began))
            {
                _touchElapsedTime = 0f;
                this.ResetTouchValues();

                _initialTouchPosition = Input.touches[0].position;
                _endTouchPosition = Input.touches[0].position;
                _initialFingerId = Input.touches[0].fingerId;

                _touchDataset.Add(Input.touches[0].position);
            }
            else
            {
                _endTouchPosition = Input.touches[0].position;
                _touchElapsedTime += Time.deltaTime;
            }

            _xDeltaPositionValue = Input.touches[0].position.x - _initialTouchPosition.x;
            _yDeltaPositionValue = Input.touches[0].position.y - _initialTouchPosition.y;
            _deltaPositionMagnitude = Vector2.Distance(_initialTouchPosition, _endTouchPosition);

            if (_deltaPositionMagnitude >= _deltaPositionCalculationThreshold) //input type is swipe or circle
            {
                if (Input.touches[0].phase.Equals(TouchPhase.Moved) || Input.touches[0].phase.Equals(TouchPhase.Stationary))
                {
                    _touchDataset.Add(Input.touches[0].position);

                    _isCircle = this.CheckIsCircle();

                    if (!_isCircle)
                    {
                        if (Mathf.Abs(_initialTouchPosition.x - Input.touches[0].position.x) >= _deltaPosXCalculationThreshold)
                        {
                            _isXDeltaPosition = true;
                        }

                        if (Mathf.Abs(_initialTouchPosition.y - Input.touches[0].position.y) >= _deltaPosYCalculationThreshold)
                        {
                            _isYDeltaPosition = true;
                        }

                        if (_isXDeltaPosition && _isYDeltaPosition)
                        {
                            _touchVector = Input.touches[0].position - _initialTouchPosition;

                            if (_xDeltaPositionValue > 0 && _yDeltaPositionValue > 0)
                            {
                                _touchAngle = Vector2.Angle(_touchVector, Vector2.right);
                                _touchType = GlobalVariables.TouchTypes.MOVE_UP_RIGHT;

                                if (_touchAngle < _angleTreshold)
                                    _touchType = GlobalVariables.TouchTypes.MOVE_RIGHT;
                                else if (_touchAngle > (90f - _angleTreshold))
                                    _touchType = GlobalVariables.TouchTypes.MOVE_UP;

                            }
                            else if (_xDeltaPositionValue > 0 && _yDeltaPositionValue < 0)
                            {
                                _touchAngle = Vector2.Angle(_touchVector, Vector2.right);
                                _touchType = GlobalVariables.TouchTypes.MOVE_DOWN_RIGHT;

                                if (_touchAngle < _angleTreshold)
                                    _touchType = GlobalVariables.TouchTypes.MOVE_RIGHT;
                                else if (_touchAngle > (90f - _angleTreshold))
                                    _touchType = GlobalVariables.TouchTypes.MOVE_DOWN;

                            }
                            else if (_xDeltaPositionValue < 0 && _yDeltaPositionValue > 0)
                            {
                                _touchAngle = Vector2.Angle(_touchVector, Vector2.left);
                                _touchType = GlobalVariables.TouchTypes.MOVE_UP_LEFT;

                                if (_touchAngle < _angleTreshold)
                                    _touchType = GlobalVariables.TouchTypes.MOVE_LEFT;
                                else if (_touchAngle > (90f - _angleTreshold))
                                    _touchType = GlobalVariables.TouchTypes.MOVE_UP;
                            }
                            else if (_xDeltaPositionValue < 0 && _yDeltaPositionValue < 0)
                            {
                                _touchAngle = Vector2.Angle(_touchVector, Vector2.left);
                                _touchType = GlobalVariables.TouchTypes.MOVE_DOWN_LEFT;

                                if (_touchAngle < _angleTreshold)
                                    _touchType = GlobalVariables.TouchTypes.MOVE_LEFT;
                                else if (_touchAngle > (90f - _angleTreshold))
                                    _touchType = GlobalVariables.TouchTypes.MOVE_DOWN;
                            }
                        }
                        else if (_isXDeltaPosition)
                        {
                            if (_xDeltaPositionValue > 0)
                                _touchType = GlobalVariables.TouchTypes.MOVE_UP_RIGHT;
                            else
                                _touchType = GlobalVariables.TouchTypes.MOVE_LEFT;
                        }
                        else if (_isYDeltaPosition)
                        {
                            if (_yDeltaPositionValue > 0)
                                _touchType = GlobalVariables.TouchTypes.MOVE_UP;
                            else if (_yDeltaPositionValue < 0)
                                _touchType = GlobalVariables.TouchTypes.MOVE_DOWN;
                        }

                        if (!_touchType.Equals(GlobalVariables.TouchTypes.TAP))
                        {
                            //_playerMovement.PlayerInput(_touchType);
                            //_playerController.PlayerInput(_touchType, _deltaPositionMagnitude);
                        }
                    }
                    else
                    {
                        _playerController.PlayerInput(GlobalVariables.TouchTypes.CIRCLE, _deltaPositionMagnitude);
                    }
                }
            }

            if (Input.touches[0].phase.Equals(TouchPhase.Ended))
            {
                _touchDataset.Add(Input.touches[0].position);

                _isCircle = this.CheckIsCircle();

                _endTouchPosition = Input.touches[0].position;
                _deltaPositionMagnitude = Vector2.Distance(_initialTouchPosition, _endTouchPosition);

                _xDeltaPositionValue = _endTouchPosition.x - _initialTouchPosition.x;
                _yDeltaPositionValue = _endTouchPosition.y - _initialTouchPosition.y;

                if (_deltaPositionMagnitude >= _deltaPositionCalculationThreshold && !_isCircle) //input type is swipe or circle
                {
                    if (_touchElapsedTime < _touchSwipeTimeTreshold)
                    {
                        if (Mathf.Abs(_initialTouchPosition.x - _endTouchPosition.x) >= _deltaPosXCalculationThreshold)
                        {
                            _isXDeltaPosition = true;
                        }

                        if (Mathf.Abs(_initialTouchPosition.y - _endTouchPosition.y) >= _deltaPosYCalculationThreshold)
                        {
                            _isYDeltaPosition = true;
                        }

                        if (_isXDeltaPosition && _isYDeltaPosition)
                        {
                            _touchVector = Input.touches[0].position - _initialTouchPosition;


                            if (_xDeltaPositionValue > 0 && _yDeltaPositionValue > 0)
                            {
                                _touchType = GlobalVariables.TouchTypes.SWIPE_UP_RIGHT;

                                _touchAngle = Vector2.Angle(_touchVector, Vector2.right);

                                if (_touchAngle < _angleTreshold)
                                    _touchType = GlobalVariables.TouchTypes.SWIPE_RIGHT;
                                else if (_touchAngle > (90f - _angleTreshold))
                                    _touchType = GlobalVariables.TouchTypes.SWIPE_UP;

                            }
                            else if (_xDeltaPositionValue > 0 && _yDeltaPositionValue < 0)
                            {
                                _touchType = GlobalVariables.TouchTypes.SWIPE_DOWN_RIGHT;

                                _touchAngle = Vector2.Angle(_touchVector, Vector2.right);

                                if (_touchAngle < _angleTreshold)
                                    _touchType = GlobalVariables.TouchTypes.SWIPE_RIGHT;
                                else if (_touchAngle > (90f - _angleTreshold))
                                    _touchType = GlobalVariables.TouchTypes.SWIPE_DOWN;
                            }
                            else if (_xDeltaPositionValue < 0 && _yDeltaPositionValue > 0)
                            {
                                _touchType = GlobalVariables.TouchTypes.SWIPE_UP_LEFT;

                                _touchAngle = Vector2.Angle(_touchVector, Vector2.left);

                                if (_touchAngle < _angleTreshold)
                                    _touchType = GlobalVariables.TouchTypes.SWIPE_LEFT;
                                else if (_touchAngle > (90f - _angleTreshold))
                                    _touchType = GlobalVariables.TouchTypes.SWIPE_UP;


                            }
                            else if (_xDeltaPositionValue < 0 && _yDeltaPositionValue < 0)
                            {
                                _touchType = GlobalVariables.TouchTypes.SWIPE_DOWN_LEFT;

                                _touchAngle = Vector2.Angle(_touchVector, Vector2.left);

                                if (_touchAngle < _angleTreshold)
                                    _touchType = GlobalVariables.TouchTypes.SWIPE_LEFT;
                                else if (_touchAngle > (90f - _angleTreshold))
                                    _touchType = GlobalVariables.TouchTypes.SWIPE_DOWN;
                            }
                        }
                        else if (_isXDeltaPosition)
                        {
                            if (_xDeltaPositionValue > 0)
                                _touchType = GlobalVariables.TouchTypes.SWIPE_RIGHT;
                            else
                                _touchType = GlobalVariables.TouchTypes.SWIPE_LEFT;
                        }
                        else if (_isYDeltaPosition)
                        {
                            if (_yDeltaPositionValue > 0)
                                _touchType = GlobalVariables.TouchTypes.SWIPE_UP;
                            else if (_yDeltaPositionValue < 0)
                                _touchType = GlobalVariables.TouchTypes.SWIPE_DOWN;
                        }
                    }
                    else
                        _touchType = GlobalVariables.TouchTypes.ENDED;

                }
                else if (_isCircle)
                {
                    _touchType = GlobalVariables.TouchTypes.CIRCLE;
                }
                else // Tap
                {
                    _touchType = GlobalVariables.TouchTypes.TAP;
                }

                _playerController.PlayerInput(_touchType, _deltaPositionMagnitude);
            }
        }
    }

    private void ResetTouchValues()
    {
        _isXDeltaPosition = false;
        _xDeltaPositionValue = 0f;

        _isYDeltaPosition = false;
        _yDeltaPositionValue = 0f;

        _initialTouchPosition = Vector2.zero;
        _endTouchPosition = Vector2.zero;
        _initialFingerId = -1; // Aslinda -1 kullanmamak lazim cunku -1 mouse click demek

        _dotProduct = 0f;
        _cosTetha = 0f;
        _currentAngle = 0f;
        _totalAngle = 0f;
        _currentTempVector = Vector2.zero;
        _prevTempVector = Vector2.zero;

        _touchDataset = new List<Vector2>();
    }

    private bool CheckIsCircle()
    {
        _totalAngle = 0f;
        _prevTempVector = Vector2.zero;

        Vector2 centerOfCircle = Vector2.zero;

        foreach (Vector2 item in _touchDataset)
        {
            centerOfCircle += item;
        }

        centerOfCircle = centerOfCircle / _touchDataset.Count;

        foreach (Vector2 item in _touchDataset)
        {
            _currentTempVector = centerOfCircle - item;
            if (_prevTempVector.Equals(Vector2.zero))
            {
                _prevTempVector = _currentTempVector;
            }

            _dotProduct = Vector2.Dot(_currentTempVector, _prevTempVector);
            _cosTetha = _dotProduct / (_currentTempVector.magnitude * _prevTempVector.magnitude);
            _currentAngle = Mathf.Acos(_cosTetha) * Mathf.Rad2Deg;

            if (Vector3.Cross(_prevTempVector, _currentTempVector).z > 0) //counter clockwise
                _currentAngle = _currentAngle * -1f;

            if (!_currentAngle.Equals(0f) && !float.IsNaN(_currentAngle))
                _totalAngle += _currentAngle;

            _prevTempVector = _currentTempVector;
        }

        if (Mathf.Abs(_totalAngle) <= 360f + _circleAcceptedAngleTreshold && Mathf.Abs(_totalAngle) >= 360f - _circleAcceptedAngleTreshold)
            return true;
        else
            return false;
    }
}
