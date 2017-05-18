using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class QuizzLoader : MonoBehaviour
{
	static public string Text = "Text";
	static public string Answers  = "Answers";
	static public string Correct = "Correct";

//	static private bool initted = false;
//
//	private void Awake()
//	{
//		Init();
//	}
//
//	private void Init()
//	{
//		if(!initted)
//		{
//			initted = true;
//
//			TextAsset quizzTextAsset = Resources.Load<TextAsset>(QuizzFilePath);
//
//			QuestionsList.Questions = LoadQuestions(quizzTextAsset.text);
//
//			Resources.UnloadUnusedAssets();
//		}
//	}

	public void LoadQuizz(string path)
	{
		string json;

		if (File.Exists (path))
		{
			using (StreamReader sr = File.OpenText (path))
			{
				json = sr.ReadToEnd ();
			}

			QuestionsList.Questions = LoadQuestions (json);
		}
	}

	private List<Question> LoadQuestions(string input)
	{
		List<Question> questions = new List<Question> ();
		JSONObject json = new JSONObject (input);
		Question q;

		for (int i = 0, n = json.Count; i < n; i++)
		{
			q = LoadQuestions(json[i]);
			questions.Add(q);
		}

		return questions;
	}

	private Question LoadQuestions(JSONObject json)
	{
		Question q = new Question();

		if (json.HasField (Text) && json [Text].IsString)
		{
			q.Text = json [Text].str;
		}

		if (json.HasField (Answers) && json [Answers].IsArray)
		{
			q.Answers = new string[json [Answers].Count];
			for (int i = 0, n = json [Answers].Count; i < n; i++)
			{
				q.Answers[i] = json [Answers][i].str;
			}
		}

		if (json.HasField (Correct) && json [Correct].IsNumber)
		{
			q.Correct = (int)json [Correct].i;
		}

		return q;
	}
}