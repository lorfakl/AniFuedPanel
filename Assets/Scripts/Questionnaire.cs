using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questionnaire
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public string Question { get; set; }
    public List<Answer> Answers { get; set; }
    public List<Answer> Top8 { get; set; }
}

public class Answer
{
    public string Value { get; set; }
    public int Occurrence { get; set; }
}
