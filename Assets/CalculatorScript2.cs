using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CalculatorScript2 : MonoBehaviour
{
    // OLD SCRIPT, NEW SCRIPT IS THE THIRD ONE

    // TextMeshProUGUI display_text;
    // public string atual_cont;

    // public bool ended = false;

    // public float result = 0;

    // public string current_signal = "";
    // public int signal_cont = 0;
    // public bool can_signal = false;
    // private bool can_dot = false;

    // public List<GroupOfList> group = new List<GroupOfList>();
    // List<object> cont_list = new List<object>();

    // List<int> itens_to_delete = new List<int>();
    // List<int> itens_range = new List<int>();
    // List<float> itens_result = new List<float>();

    // float group_f;

    // int paren_end = 0;

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
    //             cont_list.Clear();
    //             display_text.text = "";
    //             result = 0;
    //             ended = false;
    //         }

    //         int count = 0;
    //         int count2 = 0;
    //         foreach (char c in display_text.text)
    //         {
    //             if (c.ToString() != "(") { count++; }
    //         }
    //         foreach (char c in display_text.text)
    //         {
    //             if (c.ToString() != ")") { count2++; }
    //         }

    //         float number;
    //         if (float.TryParse(Input.inputString, out number))
    //         {
    //             can_signal = true;
    //             can_dot = true;
    //             ChangeDisplay(number);
    //         }
    //         else if (Input.inputString == "," && can_dot)
    //         {
    //             display_text.text += Input.inputString;
    //             can_dot = false;
    //         }
    //         else if (Input.inputString == "(" || (Input.inputString == ")" &&
    //             display_text.text.Contains("(") && count < count2))
    //         {
    //             if (Input.inputString == "(") count++;
    //             if (Input.inputString == ")") count2++;

    //             display_text.text += Input.inputString;
    //             can_dot = false;
    //         }
    //         else if (Input.inputString == "-" && can_signal)
    //         {
    //             display_text.text += Input.inputString;
    //             can_signal = false;
    //         }
    //         else if ((Input.inputString == "+" ||
    //             Input.inputString == "*" || Input.inputString == "/") && can_signal &&
    //             display_text.text.Length != 0)
    //         {
    //             can_signal = false;
    //             display_text.text += Input.inputString;
    //         }
    //     }

    //     if (Input.GetKeyDown(KeyCode.Return))
    //     {
    //         int number;
    //         if (int.TryParse(display_text.text[display_text.text.Length-1].ToString(), out number) ==
    //             false && display_text.text[display_text.text.Length - 1].ToString() != ")") 
    //         {
    //             display_text.text = display_text.text.Remove(display_text.text.Length-1);
    //         }
    //         Calculate();
    //     }
    // }
    // // N = NUMBER
    // void ChangeDisplay(float n)
    // {
    //     display_text.text += n;
    // }

    // void Calculate()
    // {
    //     int cont = -1;
    //     float number;
    //     string n = "";
    //     int start = 0;
    //     int end = 0;
        

    //     foreach (char c in display_text.text)
    //     {
    //         cont++;
    //         /*if (c.ToString() != "(" && c.ToString() != ")") 
    //         { 
    //             cont++;
    //         }
    //         else
    //         {*/
    //             if(c.ToString() == "(")
    //             {
    //                 start = cont;
    //             }
    //             else if(c.ToString() == ")")
    //             {
    //                 end = cont;
    //                 //if(paren_end > 0)
    //                 //{
    //                     //start -= 2;
    //                 //}
    //                 paren_end++;
    //             }
    //         //}
            
    //         if (float.TryParse(c.ToString(), out number))
    //         {
    //             n += number;
    //         }
    //         else if (c.ToString() == ")")
    //         {
    //             display_text.text = display_text.text.Remove(end, 1);
    //             display_text.text = display_text.text.Remove(start, 1);
    //             cont -= 2;
                
    //             int range = 0;

    //             List<object> local_list = new List<object>();

    //             for (int i = start; i < end-1; i++)
    //             {
    //                 range++;
    //                 local_list.Add(display_text.text[i]);

    //                 //Debug.Log(display_text.text[i] + "ADDING TO THE LIST!");
    //             }
    //             if (range > 1)
    //             {
    //                 group.Add(new GroupOfList());

    //                 group[group.Count - 1].group_l.Add(local_list);
    //                 group[group.Count - 1].start_index = start;
    //                 group[group.Count - 1].range = range - 1;
    //             }
    //             SearchForPrimary();

    //             /*display_text.text = "";

    //             foreach(object obj in cont_list)
    //             {
    //                 display_text.text += obj.ToString();
    //             }*/
    //             //Calculate();
    //             //Debug.Log("The count of parethesis: " + group.Count);
    //         }
    //         else if (c.ToString() == ",")
    //         {
    //             n += c.ToString();
    //         }
    //         else if (c.ToString() == "-")
    //         {
    //             if(n != "")
    //                 cont_list.Add(float.Parse(n));
    //             n = "";
    //             cont_list.Add(c.ToString());
    //         }
    //         else if (c.ToString() == "+" || 
    //             c.ToString() == "*" || c.ToString() == "/")
    //         {
    //             cont_list.Add(float.Parse(n));
    //             n = "";
    //             cont_list.Add(c.ToString());
    //         }
    //     }
    //     if (n != "")
    //         cont_list.Add(n);
    //     n = "";

    //     string daCont = "";
    //     foreach (object obj in cont_list)
    //     {
    //         daCont += obj.ToString() + " || ";
    //     }
    //     Debug.Log(daCont);

    //     if (cont_list[0].ToString() == "-")
    //     {
    //         cont_list[0] = -float.Parse(cont_list[1].ToString());
    //         cont_list.RemoveAt(1);
    //     }

    //     //SearchForPrimary();

    //     display_text.text = result.ToString();
        
    //     ended = true;
    // }

    // float SearchForPrimary()
    // {
    //     List<object> local_list = new List<object>();
    //     Debug.Log("REPETINDO");

    //     if (group.Count > 0)
    //     {
    //         foreach (List<object> ob in group[0].group_l) //WORKING CODE
    //         //foreach ()
    //         {
    //             foreach (object o in ob)
    //             {
    //                 local_list.Add(o.ToString());
    //                 //Debug.Log(o.ToString() + " Element.");
    //             }
    //         }
    //         if (local_list.Count > 1)
    //         {
    //             /*string a = "";
    //             foreach (object o in cont_list)
    //             {
    //                 a += o.ToString();
    //             }*/
    //             int ind = group[0].start_index;
    //             int qnt = group[0].range;//local_list.Count;
                
    //             SearchForSecondary(local_list);

    //             foreach (object o in local_list)
    //             {
    //                 //local_list.Add(o.ToString());
    //                 Debug.Log(o.ToString() + " Element. AFTER THE FUNCTION CALLED");
    //             }

    //             float res = group_f;

    //             //cont_list[ind] = group_f;

    //             string a = "";
    //             foreach(object obj in cont_list)
    //             {
    //                 a += obj.ToString();
    //             }
    //             Debug.Log(a);

    //             //cont_list.RemoveRange(ind, qnt-1);

    //             return group_f;
    //         }
    //         else if (cont_list.Count > 1)
    //         {
    //             //SearchForSecondary(cont_list);
    //             return 0f;
    //         }
    //         return 0f;
    //     }
    //     else
    //     {
    //         return 0f;
    //         //SearchForSecondary(cont_list);
    //     }
    // }

    // void SearchForSecondary(List<object> cont)
    // {
    //     string daCont = "";
    //     foreach (object obj in cont)
    //     {
    //         daCont += obj.ToString() + " || ";
    //     }
    //     //Debug.Log(daCont + "\n this is from SECONDARY function.");

    //     if (cont.Contains("*"))
    //     {
    //         if ((cont.Contains("/") && cont.IndexOf("/") > cont.IndexOf("*")) ||
    //             !cont.Contains("/"))
    //         {
    //             int middle_index = cont.IndexOf("*");
    //             result = float.Parse(cont[middle_index - 1].ToString()) *
    //                 float.Parse(cont[middle_index + 1].ToString());

    //             //cont_list
    //             cont.RemoveAt(middle_index - 1);
    //             cont[cont.IndexOf("*") + 1] = result;
    //             cont.RemoveAt(cont.IndexOf("*"));

    //             string resultin = "";
    //             foreach (object obj in cont)
    //             {
    //                 resultin += obj.ToString();
    //             }

    //             SearchForSecondary(cont);
    //         }
    //     }
    //     if (cont.Contains("/"))
    //     {
    //         if ((cont.Contains("*") && cont.IndexOf("/") < cont.IndexOf("*")) ||
    //             !cont.Contains("*"))
    //         {
    //             int middle_index = cont.IndexOf("/");
    //             result = float.Parse(cont[middle_index - 1].ToString()) /
    //                 float.Parse(cont[middle_index + 1].ToString());

    //             cont.RemoveAt(middle_index - 1);
    //             cont[cont.IndexOf("/") + 1] = result;
    //             cont.RemoveAt(cont.IndexOf("/"));

    //             SearchForSecondary(cont);
    //         }
    //     }
    //     SearchForThird(cont);
    // }
    
    // void SearchForThird(List<object> cont)
    // {
    //     string daCont = "";
    //     foreach (object obj in cont)
    //     {
    //         daCont += obj.ToString();
    //     }
    //     //Debug.Log(daCont + "\n this is from THIRD function.");

    //     if (cont.Contains("+"))
    //     {
    //         //Debug.Log("SOMANDO");
    //         int middle_index = cont.IndexOf("+");
    //         result = float.Parse(cont[middle_index - 1].ToString()) +
    //             float.Parse(cont[middle_index + 1].ToString());

    //         cont.RemoveAt(middle_index - 1);
    //         cont[cont.IndexOf("+") + 1] = result;
    //         cont.RemoveAt(cont.IndexOf("+"));

    //         SearchForThird(cont);
    //     }
    //     else if (cont_list.Contains("-"))
    //     {
    //         int middle_index = cont_list.IndexOf("-");

    //         //if (middle_index != 0)
    //         {
    //             result = float.Parse(cont_list[middle_index - 1].ToString()) -
    //                 float.Parse(cont_list[middle_index + 1].ToString());

    //             cont_list.RemoveAt(middle_index - 1);
    //             cont_list[cont_list.IndexOf("-") + 1] = result;
    //             cont_list.RemoveAt(cont_list.IndexOf("-"));
    //         }
    //         /*else
    //         {
    //             int n = int.Parse(cont_list[middle_index + 1].ToString());
    //             cont_list[middle_index] = -n;
    //             cont_list.RemoveAt(middle_index + 1);
    //         }*/

    //         //SearchForThird();
    //     }
    //     //Debug.Log(cont[0] + " FINAL RESULT FROM CONT[0]");
    //     group_f = float.Parse(result.ToString());//cont[0].ToString());
    // }

    // public class GroupOfList
    // {
    //     public List<object> group_l = new List<object>();
    //     public int start_index;
    //     public int range;
    //     //public int result;
    // }
}