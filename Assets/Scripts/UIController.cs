using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Text question;
    public GameObject clientUI;
    public GameObject serverUI;
    public InputField usernameInputField;
	public Text[] buttonTexts;
	public Dropdown quizzList;
	public QuizzLoader quizzLoader;
    
    [HideInInspector]
    public PlayerController pc;

    public string username = "";
    static public UIController Instance = null;

    void Start()
    {
        if(Instance == null)
            Instance = this;

		quizzList.ClearOptions ();
        clientUI.SetActive(false);
        serverUI.SetActive(false);
    }

    public void OnNextQuestionClicked()
    {
		if (quizzList.value >= quizzList.options.Count - 1)
		{
			SceneManager.LoadScene ("EnterQuizz");
		}
		else
		{
			if (QuestionsList.CurrentQuestion == 0)
			{
				quizzLoader.LoadQuizz (Application.persistentDataPath + Definitions.quizzPath + quizzList.options [quizzList.value].text);
			}
		
			Question q = QuestionsList.GetCurrentQuestion ();
			pc.OnQuestionSent (q.Text);
		}
    }

    public void OnAnswerClicked(int id)
    {
        pc.OnAnswerSent(username, buttonTexts[id].text);
    }

    public void SetUsername(string text)
    {
        usernameInputField.text = username = text;
    }

	public void SetAnswerTexts(string[] ans)
	{
		for (int i = 0, n = ans.Length; i < n; i++)
		{
			buttonTexts [i].text = ans [i];
		}
	}

	public void Populate()
	{
        quizzList.ClearOptions();
        List<string> options = new List<string> ();

		if (!File.Exists (Application.persistentDataPath + Definitions.quizzPath))
		{
			Directory.CreateDirectory (Application.persistentDataPath + Definitions.quizzPath);
		}

		string[] files = Directory.GetFiles (Application.persistentDataPath + Definitions.quizzPath);

		for(int i = 0, n = files.Length; i < n; i++)
		{
			string[] split = files[i].Split ('/');
			files[i] = split [split.Length - 1];
		}

		options.AddRange (files);

		options.Add ("New quiz");

		quizzList.AddOptions(options);
	}

	public void OnEditClicked()
	{
		if (quizzList.value < quizzList.options.Count - 1)
		{
			QuizzCreationController.editQuizz = true;
			QuizzCreationController.quizzToEdit = Application.persistentDataPath + Definitions.quizzPath + quizzList.options [quizzList.value].text;
		}

		SceneManager.LoadScene ("EnterQuizz");
	}
}