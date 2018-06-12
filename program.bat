@echo off
path=C:\Windows\Microsoft.NET\Framework\v4.0.30319
csc /out:Program.exe Program.cs board.cs AI.cs  Player.cs Search.cs Tile.cs /platform:x86
Program