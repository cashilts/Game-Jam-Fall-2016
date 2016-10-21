using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {

	// Use this for initialization
	void Start () {

        CreateGrid(1000, 1000);


    }

    void CreateGrid(int width, int height){
        Texture2D blankBoard = Texture2D.blackTexture;
        blankBoard.Resize(width, height);
        Color[] blacks = blankBoard.GetPixels();
        for(int i = 0; i<blacks.Length; i++)
        {
            blacks[i] = Color.black;
        }
        blankBoard.SetPixels(blacks);
        blankBoard.Apply();
        Color[] whiteArray = new Color[1000];
        for (int i = 0; i < 1000; i++)
        {
            whiteArray[i] = Color.white;
        }
        for (int x = 0; x < 1000; x += 100)
        {
            blankBoard.SetPixels(x, 0, 1, 1000, whiteArray);
            blankBoard.SetPixels(0, x, 1000, 1, whiteArray);
        }
        blankBoard.Apply();
        Sprite temp = Sprite.Create(blankBoard, new Rect(0,0, 1000, 1000), new Vector2(0, 0));
        SpriteRenderer sprite = (SpriteRenderer)this.GetComponent("SpriteRenderer");
        sprite.sprite = temp;

        this.transform.Translate(-5, -5, 0);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
