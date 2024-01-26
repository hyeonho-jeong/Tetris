using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tetris
{
    class Program
    {
        static void Main(String[] args)
        {
            //Call a tetrisscreen class
            TETRISSCREEN NewSC = new TETRISSCREEN(10, 15, true);
            //Call a AccScreen class
            AccScreen NewASC = new AccScreen(NewSC);
            //Call a Block class
            Block NewBlock = new Block(NewSC, NewASC);
            
            while(true)
            {
                Thread.Sleep(600);
                
                Console.Clear();//Make a terminal clear
                NewSC.Render();//Showing the block with new area on a game board
                NewSC.Clear();
                
                NewASC.Render();//If the block touch other block or wall, stock
                NewASC.DestroyCheck();
                NewBlock.Move();//If the block is moved
                
            }
        }
    }
}