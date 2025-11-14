using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    InputSystem_Actions input;
    Vector2 moveInput;

    [SerializeField] private Text CO2ScoreUI; // Legacy UI の Text コンポーネント
    [SerializeField] private Text ConcreteScoreUI;

    public static int CO2Score;
    public static int ConcreteScore;

    // 現在重なっているものを保持する
    List<Collider> overlapping = new List<Collider>();

    void Awake()
    {
        input = new InputSystem_Actions();
        input.Player.Click.performed += _ => OnClick();
    }

    void OnEnable()
    {
        input.Player.Enable();
    }

    void OnDisable()
    {
        input.Player.Disable();
    }

    void Update()
    {
        moveInput = input.Player.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(move * moveSpeed * Time.deltaTime);
    }

    // --- 重なり始めたら登録 ---
    void OnTriggerEnter(Collider other)
    {
        if (!overlapping.Contains(other))
            overlapping.Add(other);
    }

    // --- 離れたら解除 ---
    void OnTriggerExit(Collider other)
    {
        overlapping.Remove(other);
    }

    // --- クリックされたときに重なっているものを利用 ---
    void OnClick()
    {
        foreach (var obj in overlapping)
        {
            if (obj.CompareTag("CO2"))
            {
                Debug.Log("CO2 に重なってる: " + obj.name);
            }
            else if (obj.CompareTag("Concrete"))
            {
                Debug.Log("Concrete に重なってる: " + obj.name);
            }
        }
    }

    void GetCO2()
    {
        CO2Score++;
        //CO2ScoreUI.text =;
    }
}
