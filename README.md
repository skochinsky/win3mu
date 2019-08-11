Text taken from  https://www.toptensoftware.com/win3mu/
---
# Win3mu
Win3mu is a Windows 3.0 emulator. It includes an 8086 CPU emulation that loads 16-bit Windows executables and maps API calls onto the modern 32 or 64-bit Windows API.

Win3mu was started as a hobby project - mainly as a curiousity to see if it could be done. Unfortunately, once that challenge was met my interest in the project waned and due to other commitments (ie: paying projects) and other interests I no longer have much time to spend on it.
So I'm handing it over to the open source community in the hope someone might take an interest in it and continue to develop it. 


# How to Build Win3mu
It requires Visual Studio 2017 and .NET 4.6.1. 
To build it, clone the repository, init the submodules, open the solution file and build the win3mu project.
- To run a 16-bit program, pass the path to the 16-bit executable program as a commend line argument to win3mu.exe.
- If the 16-bit program uses any APIs that haven't been implemented you'll get an exception. Implement the missing API, rinse and repeat.

# Technical Details
During development of this project I wrote a series of articles that explains how it works. You can [read these here](https://hackernoon.com/win3mu-part-1-why-im-writing-a-16-bit-windows-emulator-2eae946c935d).


# License
Win3mu - Windows 3 Emulator
Copyright (C) 2017 Topten Software.
Win3mu is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
Win3mu is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
You should have received a copy of the GNU General Public License along with Win3mu. If not, see http://www.gnu.org/licenses/.
---
