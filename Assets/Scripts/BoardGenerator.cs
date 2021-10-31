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

    void GenerateField(int y, int row, int column, int x)
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
            instances.field[row, column].name = "Field " + letters[column] + " " + (8 - row);
            instances.field[row, column].transform.SetParent(Field.transform);
            instances.field[row, column].GetComponent<FieldInfo>().positionX = column;
            instances.field[row, column].GetComponent<FieldInfo>().positionY = row;
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
        if (WhiteFiguresCloserToPlayer == true)
        {
            int y = -7;
            for (int row = 7; row >= 0; row--)
            {
                int x = -7;
                for (int column = 0; column < 8; column++)
                {
                    GenerateField(y, row, column, x);
                    x += 2;
                }
                y += 2;
            }
        }
        else
        {
            int y = -7;
            for (int row = 0; row < 8; row++)
            {
                int x = -7;
                for (int column = 7; column >= 0; column--)
                {
                    GenerateField(y, row, column, x);
                    x += 2;
                }
                y += 2;
            }
        }
        GenerateText();
    }
    void PlaceFigure(GameObject figure, int positionX, int positionY)
    {
        GameObject figureToPlace = Instantiate(figure);
        figureToPlace.transform.position = new Vector3(instances.field[positionX, positionY].transform.position.x,
                                              instances.field[positionX, positionY].transform.position.y, -1);
        figureToPlace.transform.SetParent(Figures.transform);
        figureToPlace.name = figure.name + " " + letters[positionX] + " " + positionY;
        instances.field[positionX, positionY].GetComponent<FieldInfo>().figure = figureToPlace;
    }
    void PlacePawns()
    {
        for (int column = 0; column < 8; column++)
        {
            PlaceFigure(WhitePawn, 6, column);
            PlaceFigure(BlackPawn, 1, column);
        }
    }
    void PlaceWhiteFigures()
    {
        PlaceFigure(WhiteRook, 7, 0);
        PlaceFigure(WhiteKnight, 7, 1);
        PlaceFigure(WhiteBishop, 7, 2);
        PlaceFigure(WhiteKing, 7, 3);
        PlaceFigure(WhiteQueen, 7, 4);
        PlaceFigure(WhiteBishop, 7, 5);
        PlaceFigure(WhiteKnight, 7, 6);
        PlaceFigure(WhiteRook, 7, 7);
    }
    void PlaceBlackFigures()
    {
        PlaceFigure(BlackRook, 0, 0);
        PlaceFigure(BlackKnight, 0, 1);
        PlaceFigure(BlackBishop, 0, 2);
        PlaceFigure(BlackKing, 0, 3);
        PlaceFigure(BlackQueen, 0, 4);
        PlaceFigure(BlackBishop, 0, 5);
        PlaceFigure(BlackKnight, 0, 6);
        PlaceFigure(BlackRook, 0, 7);
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
