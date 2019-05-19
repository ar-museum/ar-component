using System;
using System.IO;
using manageQuestions;
using UnityEngine;
using meniu;
using System.Collections.Generic;
using GameRequest;
using System.Collections;

namespace manageQuestions
{
    [System.Serializable]
    public class Question
    {
        public Question()
        {
            
        }
        public string difficult;
        public string question;
        public string[] allAnswers;
        public string rightAnswer;

        public string getDificulty()
        {
            return difficult;
        }
        public string getQuestion()
        {
            return question;
        }

        public string[] getAllAnswers()
        {
            return allAnswers;
        }

        public string getRightAnswer()
        {
            return rightAnswer;
        }

        public void setQuestion(string q)
        {
            question = q;
        }

        public void setAllAnswers(string[] aa)
        {
            allAnswers = aa;
        }

        public void setRightAnswer(string a)
        {
            rightAnswer = a;
        }
        public void setDificulty(string a)
        {
            difficult = a;
        }
    }

    [Serializable]
    public class QuestionArray
    {
        public List<Question> questions;
    }

    [Serializable]
    public class JsonToObject
    {

        public QuestionArray loadJson()
        {
            Debug.Log(Apelare.paths);
            Debug.Log(MenuManager.getDifficulty());
            
            string language = MenuManager.getLanguage();
            Debug.Log(PlayerPrefs.GetInt("Games_Museum"));
            String museum=null;
            if(PlayerPrefs.GetInt("Games_Museum")==1)
            {
                //Eminescu
                museum = "Eminescu";
            }
            else if(PlayerPrefs.GetInt("Games_Museum") == 4)
            {
                //Stiinta
                museum = "Stiinta";
            }
            if (language == "Romanian")
            {
                TextAsset r = (TextAsset)Resources.Load("GAMES_TEAM/"+museum+"/"+museum+"Romana", typeof(TextAsset));
               
                string json = r.text;
                    //Debug.Log(json);
                    //List<Question> deserialized = JsonUtility.FromJson<List<Question>>(json);
                    QuestionArray deserialized = JsonUtility.FromJson<QuestionArray>(json);
                    
                    Debug.Log(deserialized.questions[0].getQuestion());
                    for (int i = 0; i < deserialized.questions.Count; i++)
                    {
                        if (deserialized.questions[i].getDificulty() != MenuManager.getDifficulty())
                        { deserialized.questions.RemoveAt(i); i--; }
                        else
                        {
                            //Debug.Log(deserialized.questions[i].question);
                        }
                    }
                    //Debug.Log(deserialized.questions.Count);
                    return deserialized;
                
            }
            else if (language == "English")
            {
                TextAsset r = (TextAsset)Resources.Load("GAMES_TEAM/"+museum+"/"+museum+"Engleza", typeof(TextAsset));

                string json = r.text;
               
                    //Debug.Log(json);
                    //List<Question> deserialized = JsonUtility.FromJson<List<Question>>(json);
                    QuestionArray deserialized = JsonUtility.FromJson<QuestionArray>(json);

                    //Debug.Log(deserialized.questions[0].getQuestion());
                    for (int i = 0; i < deserialized.questions.Count; i++)
                    {
                        if (deserialized.questions[i].getDificulty() != MenuManager.getDifficulty())
                        { deserialized.questions.RemoveAt(i); i--; }
                        else
                        {
                            //Debug.Log(deserialized.questions[i].question);
                        }
                    }
                    //Debug.Log(deserialized.questions.Count);
                    return deserialized;
                
            }
            else if(language == "French")
            {
                TextAsset r = (TextAsset)Resources.Load("GAMES_TEAM/"+museum+"/"+museum+"Franceza", typeof(TextAsset));

                string json = r.text;
                //Debug.Log(json);
                //List<Question> deserialized = JsonUtility.FromJson<List<Question>>(json);
                QuestionArray deserialized = JsonUtility.FromJson<QuestionArray>(json);

                    //Debug.Log(deserialized.questions[0].getQuestion());
                    for (int i = 0; i < deserialized.questions.Count; i++)
                    {
                        if (deserialized.questions[i].getDificulty() != MenuManager.getDifficulty())
                        { deserialized.questions.RemoveAt(i); i--; }
                        else
                        {
                            //Debug.Log(deserialized.questions[i].question);
                        }
                    }
                    //Debug.Log(deserialized.questions.Count);
                    return deserialized;
                
            }
            return null;
        }
    }
}

