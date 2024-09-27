# FPS Multiplayer Game Constructed in Unity and Supported with Photon Pun 2 and Docker

This is an FPS game that can be played with friends, not locally, but with everyone using their own computers. It was constructed in Unity, and for data gathering and server management, Docker containers were used as TCP and UDP servers to gather login data or simple information stored within some text files.

## Important!
It is important to note that for this Unity project to work, the following steps are necessary:
  * The Docker servers must be up and running.
  * Change the IP Address in the TCP client script "Cliente.cs".
  * Change the IP Address int the UDP client script "ClienteUDP.cs".

## Configure Docker Servers

To configure the servers you can follow the instructions in the Repo that contains the docker servers files:

 [Docker TCP and UDP servers with configuration Instructions](https://github.com/branxz07/Docker-TCP-UDP-servers-in-C.git)

 ## Contributors

This project was made possible through the collaborative efforts of the following individuals:

1. Brandon Avalos
   - GitHub: [@Branxz07](https://github.com/branxz07)

2. Yahwthani Morales
   - GitHub: [@YahwthaniMG](https://github.com/YahwthaniMG)

3. Sebastian Avilez
   - GitHub: [@SebastianAz](https://github.com/0247473)

We appreciate the hard work and dedication of each team member in bringing this project to life. Their diverse skills and perspectives were instrumental in creating Unity multiplayer game.

## License

This software is released into the public domain. Anyone is free to use, modify, distribute, or sell it without restriction.

## Disclaimer

This project was made to demonstrate the understanding and usage of TCP and UDP protocols. It was created without any lucrative intentions. The game is mostly based on the Photon Pun FPS tutorials from **bananastutorials** (https://www.youtube.com/@bananastutorials), except for the servers, information gathering, and the use of TCP and UDP. These aspects were primarily used for creating the game and synchronizing players using Photon Pun 2 in Unity.

This project is a collaborative project created by [@Branxz07](https://github.com/branxz07), [@YahwthaniMG](https://github.com/YahwthaniMG), and [@SebastianAz](https://github.com/0247473). It is provided for free use under the following conditions:

1. FREE USE: This software is available for anyone to use, modify, distribute, or integrate into their own projects at no cost.

2. NO WARRANTY: The software is provided "as is," without warranty of any kind, express or implied. This includes, but is not limited to, warranties of merchantability, fitness for a particular purpose, and non-infringement.

3. NO LIABILITY: In no event shall the authors or copyright holders be liable for any claim, damages, or other liability, whether in an action of contract, tort, or otherwise, arising from, out of, or in connection with the software or the use or other dealings in the software.

4. USE AT YOUR OWN RISK: Any person, entity, or organization choosing to use this software does so at their own risk. The authors are not responsible for any damages, data loss, security issues, or other problems that may occur as a result of using this software.

5. NO SUPPORT OBLIGATION: The authors are under no obligation to provide support, maintenance, updates, or bug fixes for this software.

6. ACKNOWLEDGMENT: By using this software, you acknowledge that you have read this disclaimer, understand its contents, and agree to its terms.

This project is not affiliated with Unity Technologies or any other commercial entity. Any trademarks, service marks, product names, or named features are assumed to be the property of their respective owners and are used only for reference.

Remember: If you're not comfortable with these terms, please do not use this software.

---

## Related Project

This server implementation is designed to work with our Docker containers with the TCP and UDP files in C. Please refer to our Docker files project repository:

[Docker TCP and UDP servers with configuration Instructions](https://github.com/branxz07/Docker-TCP-UDP-servers-in-C.git)

The Docker project contains:
- Docker files for installation and configuration
- TCP and UDP servers

The two projects are designed to work together to create a complete multiplayer gaming experience.

---

Thank you for your interest in our TCP and UDP server project for Unity multiplayer games. If you have any questions or issues, please open an issue in this repository or the Docker project repository as appropriate.
