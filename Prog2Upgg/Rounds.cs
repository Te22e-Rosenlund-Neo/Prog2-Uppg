using Balloons;
namespace Rounds{
class Round{

//redbloon, yellow bloon, black bloon, white bloon, rainbow bloon, blue moab, DDT
private int _redCount = 0;
private int redCount {
    get  => _redCount;
     set{
        _redCount = Math.Max(0, value);
     }
}
private int yellowCount {get; set;}
private int blackCount {get; set;}
private int whiteCount {get; set;}
private int rainbowCount {get; set;}
private int blueMCount {get; set;}
private int DDTCount {get; set;}

    public Round(int red, int yellow, int black, int white, int rainbow, int blueM, int DDT){
        redCount = red;
        yellowCount = Math.Max(0, yellow);
        blackCount = Math.Max(0, black);
        whiteCount = Math.Max(0, white);
        rainbowCount = Math.Max(0, rainbow);
        blueMCount = Math.Max(0, blueM);
        DDTCount = Math.Max(0, DDT);
    }
}
}