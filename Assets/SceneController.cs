using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;
    
    private MemoryCard firstRevealed;
    private MemoryCard secondRevealed;
    private int score = 0;

    [SerializeField] MemoryCard originalCard;
    [SerializeField] Sprite[] images;
    [SerializeField] TMP_Text scoreLabel;

    private int[] ShuffleArray(int[] numbers) {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++) {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    public bool canReveal {
        get {return secondRevealed == null;}
    }

    public void CardRevealed(MemoryCard card) {
        if (firstRevealed == null) {
            firstRevealed = card;
        }
        else {
            secondRevealed = card;
            StartCoroutine(CheckMatch());
            //Debug.Log("Match? " + (firstRevealed.Id == secondRevealed.Id));
        }
    }

    private IEnumerator CheckMatch() {
        if (firstRevealed.Id == secondRevealed.Id) {
            score++;
            scoreLabel.text = $"Score: {score}";
            //Debug.Log($"Score: {score}");
        }
        else {
            yield return new WaitForSeconds(.5f);
            //score--;
            //Debug.Log($"Score: {score}");

            firstRevealed.Unreveal();
            secondRevealed.Unreveal();
        }

        firstRevealed = null;
        secondRevealed = null;
    }

    public void Restart() {
        SceneManager.LoadScene("SampleScene");
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //int id = Random.Range(0, images.Length);
        //originalCard.SetCard(id, images[id]);

        Vector3 startPos = originalCard.transform.position;

        int[] numbers = {0, 0, 1, 1, 2, 2, 3, 3};
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++) {
            for (int j = 0; j < gridRows; j++) {
                MemoryCard card;
                if (i == 0 && j == 0) {
                    card = originalCard;
                }
                else {
                    card = Instantiate(originalCard) as MemoryCard;
                }
                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
