// Mouse ile Touch simüle eder
// Bu class sahnede olmalı ve Update methodu Touch'ı okuyacak classın methodundan önce çalışmalı
// (Edit >> Project Settings >> Script Execution Order)
/* Touch okuyacak class'a aşağıdaki 5 satır eklenerek kullanılır
 * ----------------------------------------------
// TODO: Release ederken kaldırılacak
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER 
using CustomInput;
using Input = CustomInput.Input;
#endif
* ----------------------------------------------
*/


using UnityEngine;
using System.Collections;


public class CustomInputManager : MonoBehaviour
{
#if (UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER)
    private bool m_MouseTouchEnded = false; // Mouse'da ended durumunun yakalanabilmesi için "touchCount = 0" komutunu sonraki framede işletmek için.
    private float m_MouseTouchTime; // Tap Count kontrolü yapmak için
    private float m_TapCountMaxInterval = 0.5f;

    private float m_FakeInputMoveT = 0f;
    private Transform DebugObj, DebugObj2;
#endif

    // Use this for initialization
    void Start()
    {
        CustomInput.Input.touches = new CustomInput.Touch[1];
        CustomInput.Input.touches[0] = new CustomInput.Touch();
        CustomInput.Input.touches[0].fingerId = 0;
        CustomInput.Input.touchCount = 0;
        CustomInput.Input.touches[0].tapCount = 1;
    }

    // Update is called once per frame
    void Update()
    {

#if !(UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER)
		
		CustomInput.Input.acceleration = Input.acceleration;
		CustomInput.Input.gyro = Input.gyro;
		CustomInput.Input.deviceOrientation = Input.deviceOrientation;
		CustomInput.Input.touchCount = Input.touchCount;
		
		if (Input.touchCount > 0)
		{
			CustomInput.Input.touches = new CustomInput.Touch[Input.touchCount];
			
			for (int i = 0; i < Input.touchCount; i++)
			{
				CustomInput.Input.touches[i] = new CustomInput.Touch();
				CustomInput.Input.touches[i].fingerId = Input.GetTouch(i).fingerId;
				CustomInput.Input.touches[i].position = Input.GetTouch(i).position;
				CustomInput.Input.touches[i].deltaPosition = Input.GetTouch(i).deltaPosition;
				CustomInput.Input.touches[i].deltaTime = Input.GetTouch(i).deltaTime;
				CustomInput.Input.touches[i].tapCount = Input.GetTouch(i).tapCount;
				CustomInput.Input.touches[i].phase = Input.GetTouch(i).phase;
			}
		}	
		
#else
        if (Input.GetButtonDown("Fire1"))
        {
            CustomInput.Input.touches[0].position = Input.mousePosition;
            CustomInput.Input.touches[0].phase = TouchPhase.Began;
            CustomInput.Input.touches[0].deltaPosition = Vector2.zero; // Yeni eklendi
            CustomInput.Input.touchCount = 1;
            m_MouseTouchEnded = false;

            if (Time.time - m_MouseTouchTime < m_TapCountMaxInterval)
                CustomInput.Input.touches[0].tapCount++;
            else
                CustomInput.Input.touches[0].tapCount = 1;

            m_MouseTouchTime = Time.time;
        }
        else if (Input.GetButton("Fire1"))
        {
            if (CustomInput.Input.touches[0].position == (Vector2)Input.mousePosition)
            {
                CustomInput.Input.touches[0].phase = TouchPhase.Stationary;
            }
            else
            {
                CustomInput.Input.touches[0].deltaPosition = CustomInput.Input.touches[0].position - (Vector2)Input.mousePosition; //Yeni eklendi
                CustomInput.Input.touches[0].position = Input.mousePosition;
                CustomInput.Input.touches[0].phase = TouchPhase.Moved;
            }
            CustomInput.Input.touchCount = 1;
            m_MouseTouchEnded = false;
            CustomInput.Input.touches[0].tapCount = 1;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            CustomInput.Input.touches[0].phase = TouchPhase.Ended;
            m_MouseTouchEnded = true;
            return;
        }
        if (m_MouseTouchEnded)
            CustomInput.Input.touchCount = 0;

        // Fake second input
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (CustomInput.Input.touchCount == 1)
            {
                CustomInput.Input.touchCount = 2;
                CustomInput.Touch[] touches = new CustomInput.Touch[2];
                touches[0] = CustomInput.Input.touches[0];
                touches[1] = new CustomInput.Touch();
                touches[1].phase = TouchPhase.Began;
                touches[1].fingerId = 1;
                touches[1].position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f); // Fake second input screen position
                CustomInput.Input.touches = touches;

                DebugObj = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
                DebugObj.transform.localScale = Vector3.one * 5f;
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                Ray camRay = Camera.main.ScreenPointToRay(CustomInput.Input.touches[1].position);
                float distance;
                plane.Raycast(camRay, out distance);
                DebugObj.transform.position = camRay.GetPoint(distance);
            }
        }
        else if (Input.GetKey(KeyCode.F))
        {
            CustomInput.Input.touchCount = 2;
            CustomInput.Input.touches[1].phase = TouchPhase.Stationary;

            if (DebugObj == null)
            {
                DebugObj = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
                DebugObj.transform.localScale = Vector3.one * 5f;
            }
            else
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                Ray camRay = Camera.main.ScreenPointToRay(CustomInput.Input.touches[1].position);
                float distance;
                plane.Raycast(camRay, out distance);
                DebugObj.transform.position = camRay.GetPoint(distance);
            }

        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            CustomInput.Input.touchCount = 2;
            CustomInput.Input.touches[1].phase = TouchPhase.Ended;
        }
        else if (DebugObj != null)
            Destroy(DebugObj.gameObject);

        // Fake second input moving
        else if (Input.GetKeyDown(KeyCode.M))
        {
            if (CustomInput.Input.touchCount == 1)
            {
                CustomInput.Input.touchCount = 2;
                CustomInput.Touch[] touches = new CustomInput.Touch[2];
                touches[0] = CustomInput.Input.touches[0];
                touches[1] = new CustomInput.Touch();
                touches[1].phase = TouchPhase.Began;
                touches[1].fingerId = 1;
                touches[1].position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f); // Fake second input screen position
                CustomInput.Input.touches = touches;
                m_FakeInputMoveT = 0f;

                DebugObj2 = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
                DebugObj2.transform.position = touches[1].position;
                DebugObj2.transform.localScale = Vector3.one * 5f;
            }
        }
        else if (Input.GetKey(KeyCode.M))
        {
            CustomInput.Input.touchCount = 2;
            CustomInput.Input.touches[1].phase = TouchPhase.Moved;
            m_FakeInputMoveT += Time.deltaTime * 0.5f;
            Vector2 newPos = Vector2.Lerp(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f), new Vector2(Screen.width * 0.1f, Screen.height * 0.5f), m_FakeInputMoveT);
            CustomInput.Input.touches[1].deltaPosition = newPos - CustomInput.Input.touches[1].position;
            CustomInput.Input.touches[1].position = newPos;

            if (DebugObj2 != null)
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                Ray camRay = Camera.main.ScreenPointToRay(newPos);
                float distance;
                plane.Raycast(camRay, out distance);
                DebugObj2.transform.position = camRay.GetPoint(distance);
            }
        }
        else if (Input.GetKeyUp(KeyCode.M))
        {
            CustomInput.Input.touchCount = 2;
            CustomInput.Input.touches[1].phase = TouchPhase.Ended;
        }
        else if (DebugObj2 != null)
            Destroy(DebugObj2.gameObject);

#endif
    }
}
