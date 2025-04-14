using UnityEngine.SceneManagement;
using UnityEngine;

public class LivesSystem : MonoBehaviour
{
	public static LivesSystem instances;
	public const int maxLives = 3;
	private int lives;
	[SerializeField] private GameObject[] livesUI;
	[SerializeField] private CanvasGroup gameOverScreen;

	private void Awake()
	{
		lives = maxLives;
		instances = this;
		for (int i = 0; i < maxLives; i++)
			livesUI[i].SetActive(true);

		gameOverScreen.alpha = 0;
		Time.timeScale = 1;
	}

	public void DecreaseLives()
	{ 
		lives--; 
		livesUI[lives].SetActive(false);
		if (lives <= 0)
		{
			gameOverScreen.alpha = 1;
			Time.timeScale = 0;
		}
	}

	public void Restart()
		=> SceneManager.LoadScene(0);

	public void Quit()
		=> Application.Quit();
}
