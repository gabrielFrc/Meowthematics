using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalculatorScript3 : MonoBehaviour
{
    TextMeshProUGUI display_text;

    [HideInInspector] public string atual_cont;

    [HideInInspector] public bool ended = false;

    [HideInInspector] public float result = 0;

    [HideInInspector] public string current_signal = "";
    [HideInInspector] public int signal_cont = 0;

    [HideInInspector] public bool can_signal = false;
    [HideInInspector] public bool can_dot = false;

    [HideInInspector] public List<object> cont_list = new List<object>();

    [HideInInspector] public bool make_negative = false;
    [HideInInspector] public bool can_negative = true;

    bool can_enter = false;

    string b4_operation = "";

    private bool canOperate = false;

    private GameObject paper;

    private GameObject menu;
    private GameObject achviements;

    private void Awake()
    {
        display_text = GameObject.Find("/Box/Canvas/DisplayText").GetComponent<TextMeshProUGUI>();
        paper = GameObject.Find("Paper");
        menu = GameObject.Find("Menu");
        achviements = GameObject.Find("Achviements");
    }

    private void Update()
    {
        atual_cont = display_text.text;

        if (paper.activeSelf || menu.activeSelf || achviements.activeSelf)
        {
            canOperate = false;
        }
        else if (!paper.activeSelf && !menu.activeSelf && !achviements.activeSelf)
        {
            canOperate = true;
        }

        if(canOperate)
            DetectInput();
    }
    public void DetectInput()
    {
        if (Input.anyKeyDown)
        {
            if (ended || Input.inputString == "\b")
            {
                cont_list.Clear();
                display_text.text = "";
                result = 0;
                ended = false;
            }

            int openParenthesis = 0;
            int closeParenthesis = 0;
            foreach (char c in display_text.text)
            {
                if (c.ToString() != "(") { openParenthesis++; }
            }
            foreach (char c in display_text.text)
            {
                if (c.ToString() != ")") { closeParenthesis++; }
            }

            float number;
            if (float.TryParse(Input.inputString, out number))
            {
                can_signal = true;
                can_negative = true;
                can_dot = true;
                ChangeDisplay(number);
            }
            else if (Input.inputString == "," && can_dot)
            {
                display_text.text += Input.inputString;
                can_dot = false;
            }
            else if (Input.inputString == "(" || (Input.inputString == ")" &&
                display_text.text.Contains("(") && openParenthesis < closeParenthesis))
            {
                if (Input.inputString == "(")
                {
                    if (make_negative)
                    {
                        display_text.text += "-";
                        make_negative = false;
                    }
                    can_negative = true;
                    can_signal = false;
                    openParenthesis++;
                }
                if (Input.inputString == ")")
                {
                    closeParenthesis++;
                }

                display_text.text += Input.inputString;
                can_dot = false;
            }
            else if (Input.inputString == "-" && can_negative)
            {
                display_text.text += Input.inputString;
                can_negative = false;
                can_signal = false;
            }
            else if ((Input.inputString == "+" ||
                Input.inputString == "*" || Input.inputString == "/") && can_signal &&
                display_text.text.Length != 0)
            {
                can_signal = false;
                can_negative = false;
                display_text.text += Input.inputString;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            GetResult();
        }
    }
    public void GetResult()
    {
        b4_operation = display_text.text;

        int number;
        bool isAnumber = int.TryParse(display_text.text[display_text.text.Length - 1].ToString(),
            out number);

        if ((isAnumber == false && display_text.text[display_text.text.Length - 1].ToString() != ")") ||
            display_text.text[display_text.text.Length - 1].ToString() == "-")
        {
            display_text.text = display_text.text.Remove(display_text.text.Length - 1);
            Debug.Log("GETTING RESULT... " + display_text.text);
            GetResult();
        }
        else
        {
            Calculate(display_text.text);
        }
    }
    // N = NUMBER
    void ChangeDisplay(float n)
    {
        display_text.text += n;
    }

    public void Calculate(string da_text)
    {
        int cont = -1;
        float number;
        string n = "";

        foreach (char c in da_text)
        {
            cont++;
            if(c.ToString() == "(")
            {
                cont_list.Add(n);
                if (cont > 0)
                {
                    if (da_text[cont - 1].ToString() != "-" && da_text[cont - 1].ToString() != "+" &&
                        da_text[cont - 1].ToString() != "*" && da_text[cont - 1].ToString() != "/" &&
                        da_text[cont - 1].ToString() != "(")
                    {
                        cont_list.Add("*");
                    }
                }

                n = c.ToString();
                cont_list.Add(c.ToString());
                n = "";
            }
            else if(c.ToString() == ")")
            {
                cont_list.Add(n);
                cont_list.Add(c.ToString());

                if (cont < da_text.Length-1)
                {
                    if (da_text[cont + 1].ToString() != "-" && da_text[cont + 1].ToString() != "+" &&
                        da_text[cont + 1].ToString() != "*" && da_text[cont + 1].ToString() != "/" &&
                        da_text[cont + 1].ToString() != ")" && da_text[cont + 1].ToString() != "(")
                    {
                        cont_list.Add("*");
                    }
                }
                
                n = "";
            }
            
            if (float.TryParse(c.ToString(), out number))
            {
                n += number;
            }
            else if (c.ToString() == ",")
            {
                n += c.ToString();
            }
            else if (c.ToString() == "-")
            {
                if(n != "")
                    cont_list.Add(float.Parse(n));
                n = "";
                cont_list.Add(c.ToString());
            }
            else if (c.ToString() == "+" || 
                c.ToString() == "*" || c.ToString() == "/")
            {
                if(n.ToString() != "(" && n.ToString() != ")" && n.ToString() != "")
                    cont_list.Add(float.Parse(n));
                n = "";
                cont_list.Add(c.ToString());
            }
        }
        if (n != "")
            cont_list.Add(n);
        n = "";

        PosProcess(cont_list);
    }
    private void PosProcess(List<object> calc)
    {
        List<object> daCont = new List<object>();

        int count = 0;
        int start = 0;
        int end = 0;

        int open = 0;
        int close = 0;

        foreach (object obj in calc)
        {
            daCont.Add(obj);

            if(obj.ToString() == "(")
            {
                start = count;
                open++;
            }
            else if(obj.ToString() == ")")
            {
                close++;

                end = count;

                daCont.RemoveAt(end);
                daCont.RemoveAt(start);

                string preProcess = "";
                foreach (object obj2 in daCont)
                {
                    preProcess += obj2.ToString();
                }

                List<object> bf = new List<object>();

                for(int i = start; i<end-1; i++)
                {
                    if (daCont[i].ToString() == "-" && i == start)
                    {
                        bf.Add(0f);
                    }
                    bf.Add(daCont[i]);
                }
                SearchForSecondary(bf);

                float result_n;
                if (float.TryParse(bf[0].ToString(), out result_n))
                    cont_list[start] = bf[0];
                else
                {
                    cont_list[start] = bf[1];
                }

                string post = "";
                foreach (object obj2 in cont_list)
                {
                    post += obj2.ToString();
                }

                cont_list.RemoveRange(start + 1, end-start);

                RemoveEmpty();

                PosProcess(cont_list);
                return;
            }

            count++;
        }
        if (cont_list[0].ToString() == "-")
        {
            cont_list.Insert(0,0f);
        }

        SearchForSecondary(cont_list);

        string FINAL = "";
        foreach (object ob in cont_list)
        {
            if (ob.ToString() != "(" && ob.ToString() != ")")
                FINAL += ob.ToString();
        }

        display_text.text = b4_operation + " = " + FINAL.ToString();

        ended = true;

        Debug.Log(b4_operation + " = " + FINAL.ToString());
    }
    private void RemoveEmpty()
    {
        string postProcess = "";

        foreach (object obj2 in cont_list)
        {
            if (obj2.ToString() == "")
            {
                cont_list.RemoveAt(cont_list.IndexOf(obj2.ToString()));
                RemoveEmpty();
                break;
            }
            postProcess += obj2.ToString();
        }
        return;
    }
    private void SearchForSecondary(List<object> cont)
    {
        if (cont.Contains("*"))
        {
            if ((cont.Contains("/") && cont.IndexOf("/") > cont.IndexOf("*")) ||
                !cont.Contains("/"))
            {
                int middle_index = cont.IndexOf("*");
                result = float.Parse(cont[middle_index - 1].ToString()) *
                    float.Parse(cont[middle_index + 1].ToString());

                cont.RemoveAt(middle_index - 1);
                cont[cont.IndexOf("*") + 1] = result;
                cont.RemoveAt(cont.IndexOf("*"));

                string resultin = "";
                foreach (object obj in cont)
                {
                    resultin += obj.ToString();
                }
                SearchForSecondary(cont);
            }
        }
        if (cont.Contains("/"))
        {
            if ((cont.Contains("*") && cont.IndexOf("/") < cont.IndexOf("*")) ||
                !cont.Contains("*"))
            {
                int middle_index = cont.IndexOf("/");
                result = float.Parse(cont[middle_index - 1].ToString()) /
                    float.Parse(cont[middle_index + 1].ToString());

                cont.RemoveAt(middle_index - 1);
                cont[cont.IndexOf("/") + 1] = result;
                cont.RemoveAt(cont.IndexOf("/"));

                SearchForSecondary(cont);
            }
        }
        string resulti = "";
        foreach (object obj in cont)
        {
            resulti += obj.ToString();
        }

        SearchForThird(cont);
    }
    
    private void SearchForThird(List<object> cont)
    {
        if (cont.Contains("+"))
        {
            if ((cont.Contains("-") && cont.IndexOf("-") > cont.IndexOf("+")) ||
                !cont.Contains("-"))
            {
                int middle_index = cont.IndexOf("+");
                result = float.Parse(cont[middle_index - 1].ToString()) +
                    float.Parse(cont[middle_index + 1].ToString());

                cont.RemoveAt(middle_index - 1);
                cont[cont.IndexOf("+") + 1] = result;
                cont.RemoveAt(cont.IndexOf("+"));

                string resultin = "";
                foreach (object obj in cont)
                {
                    resultin += obj.ToString();
                }

                SearchForThird(cont);
            }
        }
        if (cont.Contains("-"))
        {
            if ((cont.Contains("+") && cont.IndexOf("-") < cont.IndexOf("+")) ||
                !cont.Contains("+"))
            {
                int middle_index = cont.IndexOf("-");

                result = float.Parse(cont[middle_index - 1].ToString()) -
                float.Parse(cont[middle_index + 1].ToString());

                cont.RemoveAt(middle_index - 1);
                cont[cont.IndexOf("-") + 1] = result;
                cont.RemoveAt(cont.IndexOf("-"));

                string resultin = "";
                foreach (object obj in cont)
                {
                    resultin += obj.ToString();
                }

                SearchForThird(cont);
            }
        }
    }
}