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
                GameObject spriteBack = GameObject.Instantiate((GameObject)Resources.Load("Prefab/Tile"));
                newSprite.name = this.name + x.ToString() + "-" + y.ToString();
                spriteBack.name = this.name + x.ToString() + "-" + y.ToString() + "-back";
                Vector3 position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                position.x += (50 * x) / 100f + 25f / 100f;
                position.y += ((20 * 50) - (y * 50)) / 100f - 25f / 100f;
                newSprite.transform.position = position;
                spriteBack.transform.position = position;
                spriteBack.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load<Sprite>("Graphics/tile");
                
                switch (tiles[x, y].type) {
                    case tileType.HILL:
                        newSprite.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load<Sprite>("Graphics/hill");
                        spriteBack.GetComponent<SpriteRenderer>().color = new Color(139f / 255f, 69f / 255f, 19f / 255f);
                        break;
                    case tileType.MOUNTAIN:
                        newSprite.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load<Sprite>("Graphics/mountain");
                        spriteBack.GetComponent<SpriteRenderer>().color = new Color(128f / 255f, 128f / 255f, 128f / 255f);
                        break;
                    case tileType.TREE:
                        newSprite.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load<Sprite>("Graphics/tree");
                        spriteBack.GetComponent<SpriteRenderer>().color = new Color(0, 125f / 255f, 0);
                        break;
                    case tileType.PLAIN:
                        spriteBack.GetComponent<SpriteRenderer>().color = new Color(144f / 255f, 238f / 255f, 144f / 255f);
                        break;
                    
                }
                newSprite.transform.parent = transform;
                spriteBack.transform.parent = newSprite.transform;
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
