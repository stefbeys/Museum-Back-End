

# Museum Back End
Main Branch: [![Build Status](https://travis-ci.com/stefbeys/Museum-Back-End.svg?branch=master)](https://travis-ci.com/stefbeys/Museum-Back-End) 

# Run it
## 1. Docker

Prerequisites: 

 1. Docker (64-bit linux container)
 2. NPM

Setup:
(MuseumBack directory)
 - Add the docker image `docker build -t museum .` (the "." at the end of the command is important!)
 - Add iisexpress-proxy `npm install -g iisexpress-proxy` or in step 4 use`npx iisexpress-proxy 8080 to 80` (port 8080 is the port docker will use see step 3)
 - Launch the Docker image: `docker run -p 8080:5000 museum`
 - start the proxy: `iisexpress-proxy 8080 to 80`or `npx iisexpress-proxy 8080 to 80`
 - you can access the API on [localhost](http://localhost/api/)
 - for endpoints go to [swagger](http://localhost/swagger/index.html)
## Visual Studio 2019 
Prerequisites:
	
 - Visual Studio 2019 with asp.net dependencies
 - [.NET Core 2.2](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-2.2.207-windows-x64-installer) 
 
After this you should be able to open and launch the project from Visual studio.
See screenshot for run settings.
![enter image description here](https://raw.githubusercontent.com/stefbeys/Museum-Back-End/master/vs.PNG)
choose the museumBack and run it. 

# Endpoints
You can find all the endpoints from the swagger endpoint.
http://ipofhost:port/swagger

# Run release
Prerequisites:

 - [.Net Runtime](https://dotnet.microsoft.com/download/thank-you/dotnet-runtime-2.2.7-windows-x64-installer)
 - [.Net Core Runtime](https://dotnet.microsoft.com/download/thank-you/dotnet-runtime-2.2.7-windows-x64-asp.net-core-runtime-installer)
 - Assets directory
Notes!!!!!!
after learning the AI restart the server! The images that are used aren't released and will eventually give a memory error. I didn't found out yet why it keeps all the images in the memory...

# USER MANUAL
## Installation

 1. Install [.Net Runtime](https://dotnet.microsoft.com/download/thank-you/dotnet-runtime-2.2.7-windows-x64-installer)
 2. Install [.Net Core Runtime](https://dotnet.microsoft.com/download/thank-you/dotnet-runtime-2.2.7-windows-x64-asp.net-core-runtime-installer)
 3. Download the MuseumBackend 
 4. Extract the zip to a folder by choice
 5. Download the Assets (images of birds + info birds and AI)
 6. extraxt the assets zip to the folder of MuseumBackend (step 4)
 7. Done

## Starting
To start the app simply run MuseumBack.exe.
You can test it out by going to the [swagger endpoint](http://localhost:5000/swagger)

Steps after you probably want to do:

 - Port Forward the server
 - Reverse proxy

