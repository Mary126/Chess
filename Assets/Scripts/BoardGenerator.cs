using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardGenerator : MonoBehaviour
{
    public bool WhiteFiguresCloserToPlayer = true;
    public GameObject WhiteSquare;
    public GameObject PinkSquare;
    private Instances instances;
    public GameObject Field;
    public GameObject Figures;
    [System.NonSerialized] public char[] letters;
    [Header("Text")]
    public List<Text> lettersBottom;
    public List<Text> lettersTop;
    public List<Text> numbersLeft;
    public List<Text> numbersRight;
    [Header("White Figures")]
    public GameObject WhitePawn;
    public GameObject WhiteRook;
    public GameObject WhiteKnight;
    public GameObject WhiteBishop;
    public GameObject WhiteKing;
    public GameObject WhiteQueen;
    [Header("Black Figures")]
    public GameObject BlackPawn;
    public GameObject BlackRook;
    public GameObject BlackKnight;
    public GameObject BlackBishop;
    public GameObject BlackKing;
    public GameObject BlackQueen;

    void GenerateSquare(int y, int row, int column, int x)
    {
        if (row % 2 == 1)
        {
            if (column % 2 == 1)
            {
                instances.field[row, column] = Instantiate(WhiteSquare);
            }
            else if (column % 2 == 0)
            {
                instances.field[row, column] = Instantiate(PinkSquare);
            }
        }
        else if (row % 2 == 0)
        {
            if (column % 2 == 0)
            {
                instances.field[row, column] = Instantiate(WhiteSquare);
            }
            else if (column % 2 == 1)
            {
                instances.field[row, column] = Instantiate(PinkSquare);
            }
        }
        instances.field[row, column].transform.position = new Vector3(x, y, 0);
        instances.field[row, column].name = "Field " + lettersTop[column].text.ToString() + " " + System.Int32.Parse(numbersRight[row].text);
        instances.field[row, column].transform.SetParent(Field.transform);
        instances.field[row, column].GetComponent<FieldInfo>().positionX = lettersTop[column].text.ToString();
        instances.field[row, column].GetComponent<FieldInfo>().positionY = System.Int32.Parse(numbersRight[row].text);
    }
    void GenerateText()
    {
        if (WhiteFiguresCloserToPlayer)
        {
            int number = 8;
            for (int i = 0; i < 8; i++)
            {
                numbersLeft[i].text = (number).ToString();
                numbersRight[i].text = (number).ToString();
                lettersTop[i].text = letters[i].ToString();
                lettersBottom[i].text = letters[i].ToString();
                number--;
            }
        }
        else
        {
            int numberOfLetter = 0;
            for (int i = 7; i >= 0; i--)
            {
                numbersLeft[i].text = (i + 1).ToString();
                numbersRight[i].text = (i + 1).ToString();
                lettersTop[i].text = letters[numberOfLetter].ToString();
                lettersBottom[i].text = letters[numberOfLetter].ToString();
                numberOfLetter++;
            }
        }
        
    }
    void GenerateField()
    {
        GenerateText();
        if (WhiteFiguresCloserToPlayer == true)
        {
            int y = -7;
            for (int row = 7; row >= 0; row--)
            {
                int x = -7;
                for (int column = 0; column < 8; column++)
                {
                    GenerateSquare(y, row, column, x);
                    x += 2;
                }
                y += 2;
            }
        }
        else
        {
            int y = 7;
            for (int row = 0; row < 8; row++)
            {
                int x = 7;
                for (int column = 7; column >= 0; column--)
                {
                    GenerateSquare(y, row, column, x);
                    x -= 2;
                }
                y -= 2;
            }
        }
    }
    void PlaceFigure(GameObject figure, int positionX, int positionY, string type, string color)
    {
        GameObject figureToPlace = Instantiate(figure);
        figureToPlace.transform.position = new Vector3(instances.field[positionX, positionY].transform.position.x,
                                              instances.field[positionX, positionY].transform.position.y, -1);
        figureToPlace.transform.SetParent(Figures.transform);
        figureToPlace.name = figure.name + " " + letters[positionX] + " " + positionY;
        figureToPlace.GetComponent<FigureInfo>().field = instances.field[positionX, positionY];
        figureToPlace.GetComponent<FigureInfo>().type = type;
        figureToPlace.GetComponent<FigureInfo>().color = color;
        figureToPlace.GetComponent<FigureInfo>().fieldRow = positionX;
        figureToPlace.GetComponent<FigureInfo>().fieldColumn = positionY;
        instances.field[positionX, positionY].GetComponent<FieldInfo>().figure = figureToPlace;

    }
    void PlacePawns()
    {
        for (int column = 0; column < 8; column++)
        {
            int blackX, whiteX;
            if (WhiteFiguresCloserToPlayer)
            {
                blackX = 1;
                whiteX = 6;
            }
            else
            {
                blackX = 6;
                whiteX = 1;
            }
            PlaceFigure(WhitePawn, whiteX, column, "pawn", "white");
            PlaceFigure(BlackPawn, blackX, column, "pawn", "black");
        }
    }
    void PlaceWhiteFigures()
    {
        int whiteX;
        if (WhiteFiguresCloserToPlayer)
        {
            whiteX = 7;
        }
        else
        {
            whiteX = 0;
        }
        PlaceFigure(WhiteRook, whiteX, 0, "rook", "white");
        PlaceFigure(WhiteKnight, whiteX, 1, "knight", "white");
        PlaceFigure(WhiteBishop, whiteX, 2, "bishop", "white");
        PlaceFigure(WhiteKing, whiteX, 3, "king", "white");
        PlaceFigure(WhiteQueen, whiteX, 4, "queen", "white");
        PlaceFigure(WhiteBishop, whiteX, 5, "bishop", "white");
        PlaceFigure(WhiteKnight, whiteX, 6, "knight", "white");
        PlaceFigure(WhiteRook, whiteX, 7, "rook", "white");
    }
    void PlaceBlackFigures()
    {
        int blackX;
        if (WhiteFiguresCloserToPlayer)
        {
            blackX = 0;
        }
        else
        {
            blackX = 7;
        }
        PlaceFigure(BlackRook, blackX, 0, "rook", "black");
        PlaceFigure(BlackKnight, blackX, 1, "knight", "black");
        PlaceFigure(BlackBishop, blackX, 2, "bishop", "black");
        PlaceFigure(BlackKing, blackX, 3, "king", "black");
        PlaceFigure(BlackQueen, blackX, 4, "queen", "black");
        PlaceFigure(BlackBishop, blackX, 5, "bishop", "black");
        PlaceFigure(BlackKnight, blackX, 6, "knight", "black");
        PlaceFigure(BlackRook, blackX, 7, "rook", "black");
    }
    void PlaceFigures()
    {
        PlacePawns();
        PlaceWhiteFigures();
        PlaceBlackFigures();
    }
    void Start()
    {
        instances = GetComponent<Instances>();
        letters = new char[8] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
        GenerateField();
        PlaceFigures();
    }
}
