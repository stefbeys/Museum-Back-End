
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
 -[.NET Core 2.2](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-2.2.207-windows-x64-installer) 
After this you should be able to open and launch the project from Visual studio.
See screenshot for run settings.
![enter image description here](https://raw.githubusercontent.com/stefbeys/Museum-Back-End/master/vs.PNG)
choose the museumBack and run it. 

# Endpoints
You can find all the endpoints from the swagger endpoint.
http://ipofhost:port/swagger


