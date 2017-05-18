using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
	public InputField ip;

	public void OnHostClicked()
	{
		NetworkManager.singleton.StartHost ();

	}

	public void OnJoinClicked()
	{
		NetworkManager.singleton.networkAddress = ip.text;
		NetworkManager.singleton.StartClient ();
	}

	public void OnCreateClicked()
	{
		SceneManager.LoadScene ("EnterQuizz");
	}
}