# uTorrent Network Notifications
uTorrent Network Notifications is a client-server approach to pushing notifications about the status of torrents to multiple computers on a network.
It is designed for headless servers running uTorrent, so you don't have to continually log into the server (WebUI or other means) to check the status of torrents.

## Installation
### On the computer running uTorrent
Place the client executable and settings.ini (in uTorrent Network Notifications Client\bin\Debug) in the uTorrent folder (usually C:\Program Files\uTorrent).
In the uTorrent settings, go to Advanced > Run Program. Paste the following in the "Run this program when a torrent changes state" textbox:
"C:\Program Files\uTorrent\uTorrent Network Notifications Client.exe" "Torrent '%N' is now %M."
Open settings.ini in a text editor, and place a comma-separated list of IP addresses to notify in the "ipaddresses" list. 
The default is to notify only 127.0.0.1, which is of course only useful for testing. 
Example: ipaddresses = 127.0.0.1,192.168.1.7

### On the computers you want to receive notifications
Run the server executable (in uTorrent Network Notifications Server\bin\Debug).
To start it automatically, place the executable in the Startup folder.

## Licence
uTorrent Network Notifications is released under the Simplified BSD Licence.
For more details, see the LICENCE file.