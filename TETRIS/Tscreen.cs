using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class TETRISSCREEN
{
    //Make a game bord array with list
    protected List<List<string>>BlockList = new List<List<string>>();

    public int Y//This is for let child class know the size of the board
    {
        get
        {
            return BlockList.Count;
        }
    }

    public int X//This is for let child class know the size of the board
    {
        get
        {
            return BlockList[0].Count;
        }
    }
    
    //Put new Tetris instead a game board 
    public void SetBlock(int _y, int _x, string _Type)
    {
        BlockList[_y][_x] = _Type;
    }

    public bool IsBlock(int _y, int _x, string _Type)
    {
       return BlockList[_y][_x] == _Type;
    }
    

    //Make a function to clear the old block path
    public void Clear()
    {   
        for(int y = 0; y<BlockList.Count; ++y)
        {
            for(int x = 0; x<BlockList[y].Count; ++x)
            {
                if(y == 0 || y == BlockList.Count -1)
                {
                    BlockList[y][x] = "▣";//If the block over the game screen, Can't go anymore
                    continue;
                }
                
                BlockList[y][x] = "□";
            }
        }

    }

    //Make a function to print the game board
    public virtual void Render() //virtual is work only this class, not child
    {
        for(int y = 0; y<BlockList.Count; ++y)
        {
            for(int x = 0; x<BlockList[y].Count; ++x)
            {
                
                //Console.Write("▣");//Display tetris block with ▣ shape
                Console.Write(BlockList[y][x]);
                
            }
            Console.WriteLine();
        }
    }

    //Make the actual game board with specific size
    public TETRISSCREEN(int _X, int _Y, bool TopAndBotLine)
    {
        for(int y = 0; y< _Y; ++y)
        {
            BlockList.Add(new List<string>());//put a new list on the list
            for(int x = 0; x< _X; ++x)//Make the space as much X on the list
            {
                BlockList[y].Add("□");//Put □ in the y-th on the list
            }
        }

        if(false == TopAndBotLine)
        {
            return;
        }

        for(int i = 0; i<BlockList[0].Count; i++)
        {
            BlockList[0][i] = "▣"; //In the i-th column, Tetris creates a wall(▣) that cannot escape
        }

        for(int i = 0; i<BlockList[BlockList.Count - 1].Count; i++)
        {
            BlockList[BlockList.Count - 1][i] = "▣"; 
        }
    }
}