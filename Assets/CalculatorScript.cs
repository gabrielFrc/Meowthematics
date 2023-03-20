using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CalculatorScript : MonoBehaviour
{
    //OLD CODE, NEW ONE IS THE THIRD ONE.

    // TextMeshProUGUI display_text;
    // public string atual_cont;

    // public bool ended = false;

    // public int result = 0;

    // public string first_num = "";
    // public string second_num = "";
    // public string current_signal = "";
    // public int signal_cont = 0;
    // public bool can_signal = false;
    // public int signal_quantity = 0;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     display_text = GameObject.Find("/Calculator/Canvas/DisplayText").GetComponent<TextMeshProUGUI>();
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     atual_cont = display_text.text;

    //     if (Input.anyKeyDown)
    //     {
    //         if (ended)
    //         {
    //             //display_text.text = "";
    //             result = 0;
    //             first_num = "";
    //             second_num = "";
    //             ended = false;
    //         }

    //         int number;
    //         if (int.TryParse(Input.inputString, out number))
    //         {
    //             can_signal = true;
    //             ChangeDisplay(number);
    //         }
    //         else if ((Input.inputString == "+" || Input.inputString == "-" ||
    //             Input.inputString == "*" || Input.inputString == "/") && can_signal) 
    //         {
    //             can_signal = false;
    //             display_text.text += Input.inputString;
    //         }
    //     }

    //     if (Input.GetKeyDown(KeyCode.Return))
    //     {
    //         Calculate();
    //     }
    // }
    // // N = NUMBER
    // void ChangeDisplay(int n)
    // {
    //     display_text.text += n;
    // }

    // void Calculate()
    // {
    //     Debug.Log("Repeated");
    //     int number;
    //     //atual_cont = display_text.text;
    //     //Debug.Log(result+"result");
    //     foreach (char c in display_text.text)
    //     {
    //         //Debug.Log(first_num);
    //         //Debug.Log(second_num);
    //         if (int.TryParse(c.ToString(), out number))
    //         {
    //             //Debug.Log(current_number);
    //             if (signal_cont == 0)
    //             {
    //                 //Debug.Log(first_num);
    //                 first_num += number;//c.ToString();
                    
    //             }
    //             else if (signal_cont == 1)
    //                 second_num += number;
                
    //         }
    //         else
    //         {
    //             //Debug.Log(first_num);
    //                 //Debug.Log(first_num);
    //                 //atual_cont = display_text.text;

    //             switch (c.ToString())
    //             {
    //                 case "+":
    //                     if(signal_cont == 0)
    //                         current_signal = "+";

    //                     if (signal_cont == 1)
    //                     {
    //                         display_text.text = display_text.text.Remove(display_text.text.IndexOf("+"), 1);
    //                         Debug.Log("Pri num = " + first_num + " || Second num = " + second_num);
    //                         result = int.Parse(first_num) + int.Parse(second_num);
    //                         Debug.Log(result);
    //                         first_num = result.ToString();
    //                         display_text.text = display_text.text.Remove(display_text.text.IndexOf(first_num),
    //                             first_num.Length);
    //                         display_text.text = display_text.text.Remove(display_text.text.IndexOf(second_num),
    //                             second_num.Length);
    //                     }

    //                     break;
    //                 case "-":
    //                     if (signal_cont == 0)
    //                         current_signal = "-";
    //                     break;
    //             }
    //             if (signal_cont == 1)
    //             {
    //                 /*display_text.text = display_text.text.Remove(display_text.text.IndexOf(first_num), first_num.Length);
    //                 display_text.text = display_text.text.Remove(display_text.text.IndexOf(second_num), second_num.Length);*/
                    
    //                 break;
    //             }
    //             signal_cont = 1;
    //         }
    //     }
            
    //     /*if (current_signal == "+")
    //     {
    //         result = int.Parse(first_num) + int.Parse(second_num);
    //     }
    //     else if (current_signal == "-")
    //     {
    //         result = int.Parse(first_num) - int.Parse(second_num);
    //     }*/
    //     current_signal = "";
    //     if (display_text.text.Contains("+") || display_text.text.Contains("-"))
    //     {
    //         //display_text.text = display_text.text.Insert(0,first_num);
    //         //first_num = result.ToString();
    //         second_num = "";
    //         Calculate();
    //         return;
    //     }
    //     display_text.text = result.ToString();
    //     Debug.Log("Finished calculation");
    //     //ended = true;
    // }
}

//if (signal_cont == 0)
            /*{
                if (int.TryParse(c.ToString(), out number))
                {
                    //Debug.Log(current_number);
                    first_num += number;//c.ToString();
                }
                else
                {
                    Debug.Log(first_num);
                    display_text.text = display_text.text.Replace(first_num, "_");
                    Debug.Log(first_num);
                    //atual_cont = display_text.text;
                    switch (c.ToString())
                    {
                        case "+":
                            //result += number;
                            current_signal = "+";
                            signal_cont++;
                            signal_quantity++;
                            display_text.text = display_text.text.Remove(display_text.text.IndexOf("+"));
                            //atual_cont = display_text.text;
                            //c.
                            break;
                        case "-":
                            //result -= number;
                            current_signal = "-";
                            signal_cont++;
                            signal_quantity++;
                            break;
                    }
                }

            }
            else if (signal_cont == 1)
            {
                if (int.TryParse(c.ToString(), out number))
                {
                    //Debug.Log(current_number);
                    second_num += number;//c.ToString();
                }
                else if (current_signal == "")
                {
                    switch (c.ToString())
                    {
                        case "+":
                            //result += number;
                            current_signal = "+";
                            signal_cont = 1;
                            //signal_quantity++;
                            break;
                        case "-":
                            //result -= number;
                            current_signal = "-";
                            signal_cont = 1;
                            //signal_quantity++;
                            break;
                    }
                }

            }*/

