using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Utilities;

public class JsonParser : MonoBehaviour
{
    [SerializeField]
    TextAsset questionsJson;

    [SerializeField]
    Transform answerGroupParent; 

    public static List<Questionnaire> Questionnaire { get; private set; }
    

    private void Awake()
    {
        string questionsFile = questionsJson.text;
        Questionnaire = JsonConvert.DeserializeObject<List<Questionnaire>>(questionsFile);
        HelperFunctions.Log(Questionnaire[0].Question);
        
    }

    private void Start()
    {
            
    }


    
}

