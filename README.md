# FPS Multiplayer Game Constructed in Unity and Supported with Photon Pun 2 and Docker

This is an FPS game that can be played with friends, not locally, but with everyone using their own computers. It was constructed in Unity, and for data gathering and server management, Docker containers were used as TCP and UDP servers to gather login data or simple information stored within some text files.

## Important!
It is important to note that for this Unity project to work, the following steps are necessary:
  * The Docker servers must be up and running.
  * Change the IP Address in the TCP client script "Cliente.cs".
  * Change the IP Address int the UDP client script "ClienteUDP.cs".

## Configure Docker Servers
 <!-- * **Step 1** </br>
    Here goes step 1
  * **Step 2**
    Here goes step 2 -->

## Disclaimer

This project was made to demonstrate the understanding and usage of TCP and UDP protocols. It was created without any lucrative intentions. The game is mostly based on the Photon Pun FPS tutorials from **bananastutorials** (https://www.youtube.com/@bananastutorials), except for the servers, information gathering, and the use of TCP and UDP. These aspects were primarily used for creating the game and synchronizing players using Photon Pun 2 in Unity.
