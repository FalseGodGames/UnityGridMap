using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Grid
{
    public const int HEAT_MAP_MAX_VALUE=100;
    public const int HEAT_MAP_MIN_VALUE=0;

    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs:EventArgs{
      public int x;
      public int y;
    }

    private int width;
    private int height;
    private float cellSize;
    Vector3 originPosition;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;

    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.originPosition = originPosition;
        this.cellSize = cellSize;
        gridArray = new int[width,height];

        bool showDebug=false;//Switch to True for Grid

        if(showDebug)
        {
          debugTextArray = new TextMesh[width, height];
          for (int x = 0; x < gridArray.GetLength(0); x++)
          {
              for(int y = 0; y < gridArray.GetLength(1); y++)
              {
                  debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 20, Color.white, TextAnchor.MiddleCenter);
                  Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                  Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
              }
          }
          Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
          Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
        }
    }
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    public int GetWidth(){
      return width;
    }

    public float GetCellSize(){
      return cellSize;
    }


    public int GetHeight(){
      return height;
    }

    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = Mathf.Clamp(value,HEAT_MAP_MIN_VALUE,HEAT_MAP_MAX_VALUE);
            if(OnGridValueChanged!=null){
              OnGridValueChanged(this,new OnGridValueChangedEventArgs{x=x,y=y});
            }
        }

    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);

    }

    public void AddValue(int x,int y,int value){
      SetValue(x,y,GetValue(x,y)+value);

    }

    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return -1;
        }
    }
    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);

    }

    public void AddValue(Vector3 worldPosition,int value,int range){
      GetXY(worldPosition,out int originX,out int originY);
      for (int x=0;x<range;x++){
        for (int y=0;y<range-x;y++){
          AddValue(originX+x,originY+y,value);
          if(x!=0){
            AddValue(originX-x,originY+y,value);
          }
          if(y!=0){
            AddValue(originX+x,originY-y,value);
            if(x!=0){
              AddValue(originX-x,originY-y,value);
            }
          }
        }
      }
    }

    public void GradAddValue(Vector3 worldPosition,int value,int fullvaluerange,int totalrange){
      int lowerValueAmount=Mathf.RoundToInt((float)value/(totalrange-fullvaluerange));
      GetXY(worldPosition,out int originX,out int originY);
      for (int x=0;x<totalrange;x++){
        for (int y=0;y<totalrange-x;y++){
          int radius=x+y;
          int addValueAmount=value;
          if(radius>fullvaluerange){
            addValueAmount-=lowerValueAmount*(radius-fullvaluerange);
          }
          AddValue(originX+x,originY+y,addValueAmount);
          if(x!=0){
            AddValue(originX-x,originY+y,addValueAmount);
          }
          if(y!=0){
            AddValue(originX+x,originY-y,addValueAmount);
            if(x!=0){
              AddValue(originX-x,originY-y,addValueAmount);
            }
          }
        }
      }
    }
  }
