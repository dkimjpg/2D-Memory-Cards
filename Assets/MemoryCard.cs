using UnityEditor.SearchService;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] GameObject cardBack;
    [SerializeField] SceneController controller;

    private int _id;
    public int Id {
        get {return _id;}
    }

    public void SetCard(int id, Sprite image) {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }
    //[SerializeField] Sprite image;

    public void OnMouseDown() {
        //Debug.Log("testing 1 2 3");
        if (cardBack.activeSelf && controller.canReveal) {
            cardBack.SetActive(false);
            controller.CardRevealed(this);
        }
    }

    public void Unreveal() {
        cardBack.SetActive(true);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //GetComponent<SpriteRenderer>().sprite = image;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
