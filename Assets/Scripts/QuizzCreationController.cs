using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class QuizzCreationController : MonoBehaviour
{
	
	public InputField question;
	public InputField[] answers;
	public InputField correct;
	public InputField filename;

	[HideInInspector]
	public List<Question> questions = new List<Question>();

	public QuizzLoader quizzLoader;

	static public string quizzToEdit = null;
	static public bool editQuizz= false;

	private int index = 0;

	void Start()
	{
		if (editQuizz)
		{
			quizzLoader.LoadQuizz (quizzToEdit);
			questions = QuestionsList.Questions;

			string[] split = quizzToEdit.Split ("/.".ToCharArray());
			filename.text = split [split.Length - 2];

			editQuizz = false;
			quizzToEdit = null;
		}
		else
		{
			Question q = new Question ();
			questions.Add (q);
		}

		index = 0;
		LoadQuestion ();
	}

	void LoadQuestion()
	{
		Question q = questions [index];

		question.text = q.Text;
		for (int i = 0, n = answers.Length; i < n; i++)
		{
			answers[i].text = q.Answers[i];
		}
		correct.text = q.Correct.ToString();
	}

	void SaveQuestion()
	{
		Question q = questions [index];
		q.Text = question.text;

		q.Answers = new string[answers.Length];
		for (int i = 0, n = answers.Length; i < n; i++)
		{
			q.Answers[i] = answers[i].text;
		}

		//TODO switch with the correct input box depending on radio group
		q.Correct = correct.text;
	}

	public void OnFinishClicked()
	{
		SaveQuestion ();

		JSONObject quizz = new JSONObject ();

		for(int i = 0, n = questions.Count; i < n; i++)
		{
			JSONObject json = new JSONObject ();
			json.AddField (QuizzLoader.Text, questions[i].Text);

			JSONObject arr = new JSONObject (JSONObject.Type.ARRAY);
			for(int j = 0, m = questions[i].Answers.Length; j < m; j++)
			{
				arr.Add (questions[i].Answers[j]);
			}

			json.AddField (QuizzLoader.Answers, arr);
			json.AddField (QuizzLoader.Correct, questions [i].Correct);

			quizz.AddField("Question" + i, json);
		}

		if (!File.Exists (Application.persistentDataPath + Definitions.quizzPath))
		{
			Directory.CreateDirectory (Application.persistentDataPath + Definitions.quizzPath);
		}
			
		Debug.Log (Application.persistentDataPath + Definitions.quizzPath + filename.text);
		using (StreamWriter sw = File.CreateText(Application.persistentDataPath + Definitions.quizzPath + filename.text + ".json"))
		{
			sw.Write (quizz.Print ());
		}

		//TODO return to main menu
	}

	public void OnForthClicked()
	{
		if (index >= questions.Count - 1)
		{
			SaveQuestion ();
			index++;
			Question q = new Question ();
			questions.Add (q);

			LoadQuestion ();
		}
		else
		{
			SaveQuestion ();
			index++;
			LoadQuestion ();
		}
	}

	public void OnBackClicked()
	{
		if (index == 0)
		{
			SaveQuestion ();
			index = 0;
			Question q = new Question ();
			questions.Insert (0, q);

			LoadQuestion ();
		}
		else
		{
			SaveQuestion ();
			index--;
			LoadQuestion ();
		}
	}
}