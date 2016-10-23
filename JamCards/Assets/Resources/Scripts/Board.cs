using UnityEngine;
using System.Collections;
using UnityEngine.UI;

enum tileType { PLAIN = 'p', WATER = 'w', TREE = 't', GRASS = 'g', HILL = 'h', MOUNTAIN = 'm' };

class boardTiles
{
    public bool passable;
    public bool solid;
    public bool cover;
    public tileType type;
    public int level;

    public boardTiles()
    {
        passable = true;
        solid = false;
        cover = false;
        level = 0;
        type = new tileType();
    }
};

public class Board : MonoBehaviour {

    boardTiles[,] tiles;
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
        for (int x = 0; x < 1000; x += 50)
        {
            blankBoard.SetPixels(x, 0, 1, 1000, whiteArray);
            blankBoard.SetPixels(0, x, 1000, 1, whiteArray);
        }
        blankBoard.Apply();
        Sprite temp = Sprite.Create(blankBoard, new Rect(0,0, 1000, 1000), new Vector2(0, 0));
        SpriteRenderer sprite = (SpriteRenderer)this.GetComponent("SpriteRenderer");
        sprite.sprite = temp;

        this.transform.Translate(-5, -5, 0);
        makeGrid(20, 20);
        loadGridFromFile(System.IO.Directory.GetCurrentDirectory() + "\\Assets\\Resources\\Levels\\TestLevel");
        setUpBoardSprites();
    }

    void makeGrid(int x, int y)
    {
        tiles = new boardTiles[x, y];
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                tiles[i, j] = new boardTiles();
            }
        }
    }

    void setUpBoardSprites() {
        for (int x = 0; x < 20; x++){
            for (int y = 0; y < 20; y++) {
                GameObject newSprite = GameObject.Instantiate((GameObject)Resources.Load("Prefab/BoardSprite"));
                newSprite.name = this.name + x.ToString() + "-" + y.ToString();
                Vector3 position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                position.x += (50 * x) / 100f + 25f / 100f;
                position.y += ((20 * 50) - (y * 50)) / 100f - 25f / 100f;
                newSprite.transform.position = position;
                switch (tiles[x, y].type) {
                    case tileType.HILL:
                        newSprite.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load<Sprite>("Graphics/hill");
                        break;
                    case tileType.MOUNTAIN:
                        newSprite.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load<Sprite>("Graphics/mountain");
                        break;
                    case tileType.TREE:
                        newSprite.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load<Sprite>("Graphics/tree");
                        break;
                }
                newSprite.transform.parent = transform;
            }
        }
    }

    void loadGridFromFile(string path) {
       string[] lines = System.IO.File.ReadAllLines(path);
        int length = lines.Length;
        for (int i = 0; i < length; i++) {
            int lineLength = lines[i].Length;
            for (int j = 0; j < lineLength; j++) {
                tiles[i,j].type = (tileType)lines[i][j];
                switch (tiles[i, j].type) {
                    case tileType.PLAIN:
                        tiles[i, j].passable = true;
                        tiles[i, j].solid = false;
                        tiles[i, j].cover = false;
                        tiles[i, j].level = 0;
                        break;
                    case tileType.GRASS:
                        tiles[i,j].passable = true;
                        tiles[i, j].solid = false;
                        tiles[i, j].cover = true;
                        tiles[i, j].level = 0;
                        break;
                    case tileType.WATER:
                        tiles[i, j].passable = false;
                        tiles[i, j].solid = false;
                        tiles[i, j].cover = false;
                        tiles[i, j].level = 0;
                        break;
                    case tileType.TREE:
                        tiles[i, j].passable = true;
                        tiles[i, j].solid = true;
                        tiles[i, j].cover = true;
                        tiles[i, j].level = 0;
                        break;
                    case tileType.HILL:
                        tiles[i, j].passable = true;
                        tiles[i, j].solid = true;
                        tiles[i, j].cover = false;
                        tiles[i, j].level = 1;
                        break;
                    case tileType.MOUNTAIN:
                        tiles[i, j].passable = false;
                        tiles[i, j].solid = true;
                        tiles[i, j].cover = false;
                        tiles[i, j].level = 0;
                        break;
                }
            }
        }

    }

	// Update is called once per frame
	void Update () {
        
	
	}
}
