using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Question class !
public class Question
{
    public string questionName { get; set; }
    public string correctAnswer { get; set; }
    public string UncorrectAnswer1 { get; set; }
    public string UncorrectAnswer2 { get; set; }
    public string UncorrectAnswer3 { get; set; }

    //constructur w/ arguments !
    public Question(string qName, string cAns, string ans1, string ans2, string ans3)
    {
        this.questionName = qName;
        this.correctAnswer = cAns;
        this.UncorrectAnswer1 = ans1;
        this.UncorrectAnswer2 = ans2;
        this.UncorrectAnswer3 = ans3;
    }

}
