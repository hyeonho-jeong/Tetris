using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum BLOCKDIR
{
    BD_T,
    BD_R,
    BD_B,
    BD_L,
    BD_MAX,
}


enum BLOCKTYPE
{
    BT_I,
    BT_L,
    BT_J,
    BT_Z,
    BT_S,
    BT_T,
    BT_O,
    BT_MAX,

}

partial class Block
{
    int X = 0;
    int Y = 0;

    string[][] Arr = null;
    //Make a List for Block. When the block rotate, the shape will change. 
    //So, It have to be 4x4
    //List<List<string>> BlockData = new List<List<string>>();


    BLOCKTYPE CurBlockType = BLOCKTYPE.BT_T;//This valuable is for rotate the block
    BLOCKDIR CurDirType = BLOCKDIR.BD_T;

    //Block can't be made without gamdde board screen, but the screen already in Program.cs 
    //So just call null, not new
    TETRISSCREEN Screen = null;
    AccScreen NewAccScreen = null;
    Random NewRandom = new Random();

    //This helps to make block only when game screen is existed
    public Block(TETRISSCREEN _Screen, AccScreen _NewAccScreen)
    {
        if(null == _Screen || null == _NewAccScreen)
        {
            return;
        }

        Screen = _Screen;
        NewAccScreen = _NewAccScreen;
        DataInit();
        Reset();
    }

    //Make a randome block type
    public void RandomeBlockType()
    {

        int RandomIndex = NewRandom.Next((int)BLOCKTYPE.BT_I, (int)BLOCKTYPE.BT_MAX);
        CurBlockType = (BLOCKTYPE)RandomIndex;
    }

    private void SettingBlock(BLOCKTYPE _Type, BLOCKDIR _Dir)
    {
        Arr = AllBlock[(int) _Type][(int) _Dir];
    }

    //Make the block stock
    public void SetAccScreen()
    {
        for(int y = 0; y<4; y++)
        {
            for(int x = 0; x<4; x++)
            {
                if("■" == Arr[y][x])//stack only "■" shape block
                {
                    NewAccScreen.SetBlock(Y+y - 1, X+x, Arr[y][x]);
                }
                
            }
        }
    }

    public void Reset()
    {
        RandomeBlockType();//The random shape of block
        X = 0;
        Y = 1;
        SettingBlock(CurBlockType, CurDirType);//Make block

    }

    public bool DownCheck()
    {
        //checking if the block can't go down anymore
        for(int y = 0; y<4; y++)
        {
            for(int x = 0; x<4; x++)
            {
                if("■" == Arr[y][x])
                {
                    if(NewAccScreen.Y ==Y +y || true == NewAccScreen.IsBlock(Y + y, x + X, "■"))
                    //When the block is hit the botton  or when the block is hit the other block
                    {
                        SetAccScreen();
                        Reset();
                        return true;
                    }
                }
                
            }
        }
        return false;
    }

    public void Down()
    {
        if(true == DownCheck())
        {
            return;
        }    

        Y+=1;
    }

    private void Input()
    {   
        //Y +=1;//Fall the block automatically
        // When user click something, the input is working
        if(false == Console.KeyAvailable)
        {
            return;
        }

        
        switch(Console.ReadKey().Key)
        {
            case ConsoleKey.A:
                if(X <= 0)
                {
                    return;
                }
                else 
                {
                    X -= 1;
                }
                
                break;
            case ConsoleKey.D:               
                    X += 1;
                break;
            case ConsoleKey.S:
                Down();
                break;
            case ConsoleKey.Q: //Rotate to left
                --CurDirType; 
                if(0>CurDirType)
                {
                    CurDirType = BLOCKDIR.BD_L;
                }
                SettingBlock(CurBlockType, CurDirType);
                break;
            case ConsoleKey.E://Rotate to right
                ++CurDirType; 
                if(BLOCKDIR.BD_MAX == CurDirType)
                {
                    CurDirType = BLOCKDIR.BD_T;
                }
                SettingBlock(CurBlockType, CurDirType);
                break;
            default:
                break;
        }
    }

   
 public void Move()
    {
        Input();
        Down();
        for(int y  = 0; y<4; ++y)
        {
            for(int x = 0; x<4; ++x)
            {
                if(Arr[y][x] == "□")
                {
                    continue;
                }
                Screen.SetBlock(Y+y, X+x, Arr[y][x]);//Make a BLOCK 
            }

        }
        
    } 
}