using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Buttons : MonoBehaviour
{
    [SerializeField]private string symbol_num;

    static private TextMeshProUGUI text_display;
    private CalculatorScript3 calculator;

    private Animator anim;

    private AudioSource clickingAudio;

    private void Start()
    {
        text_display = GameObject.Find("Box/Canvas/DisplayText").GetComponent<TextMeshProUGUI>();

        calculator = GameObject.Find("Box").GetComponent<CalculatorScript3>();

        GetComponent<Button>().onClick.AddListener(ButtonListener);

        clickingAudio = transform.parent.parent.parent.GetComponent<AudioSource>();

        anim = transform.GetComponent<Animator>();
    }

    private void Update()
    {

    }
    private void ButtonListener() //Put the button valor in the calculator.
    {
        clickingAudio.Play();
        
        anim.Play("ButtonAnim");

        if (symbol_num == "=")
        {
            calculator.GetResult();
        }
        else if(symbol_num == "C")
        {
            calculator.cont_list.Clear();
            text_display.text = "";
            calculator.result = 0;
        }
        else
        { 
            if (calculator.ended)
            {
                calculator.cont_list.Clear();
                text_display.text = "";
                calculator.result = 0;
                calculator.ended = false;
            }

            int openParenthesis = 0;
            int closeParenthesis = 0;
            foreach (char c in text_display.text)
            {
                if (c.ToString() != "(") { openParenthesis++; }
            }
            foreach (char c in text_display.text)
            {
                if (c.ToString() != ")") { closeParenthesis++; }
            }

            float number;
            if (float.TryParse(symbol_num, out number))
            {
                calculator.can_signal = true;
                calculator.can_negative = true;
                calculator.can_dot = true;
                text_display.text += number;
            }
            else if (symbol_num == "," && calculator.can_dot)
            {
                text_display.text += symbol_num;
                calculator.can_dot = false;
            }
            else if (symbol_num == "(" || (symbol_num == ")" &&
                text_display.text.Contains("(") && openParenthesis < closeParenthesis))
            {
                if (symbol_num == "(")
                {
                    if (calculator.make_negative)
                    {
                        text_display.text += "-";
                        calculator.make_negative = false;
                    }
                    calculator.can_negative = true;
                    calculator.can_signal = false;
                    openParenthesis++;
                }
                if (symbol_num == ")")
                {
                    closeParenthesis++;
                }

                text_display.text += symbol_num;
                calculator.can_dot = false;
            }
            else if (symbol_num == "-" && calculator.can_negative)
            {
                text_display.text += symbol_num;
                calculator.can_negative = false;
                calculator.can_signal = false;
            }
            else if ((symbol_num == "+" ||
                symbol_num == "*" || symbol_num == "/") && calculator.can_signal &&
                text_display.text.Length != 0)
            {
                calculator.can_signal = false;
                calculator.can_negative = false;
                text_display.text += symbol_num;
            }
        }
    }
}
