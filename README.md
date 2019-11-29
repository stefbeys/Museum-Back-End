
# Museum Back End
Main Branch: [![Build Status](https://travis-ci.com/stefbeys/Museum-Back-End.svg?branch=master)](https://travis-ci.com/stefbeys/Museum-Back-End) 

## Installation

Prerequisites: 

 1. Docker (64-bit linux container)
 2. NPM

Setup:

 1. Add the docker image `docker build -t museum .` (the "." at the end of the command is important!)
 2. Add iisexpress-proxy `npm install -g iisexpress-proxy` or in step 4 use`npx iisexpress-proxy 8080 to 80` (port 8080 is the port docker will use see step 3)
 3. Launch the Docker image: `docker run -p 8080:5000 museum`
 4. start the proxy: `iisexpress-proxy 8080 to 80`or `npx iisexpress-proxy 8080 to 80`
 5. you can access the API on [localhost](http://localhost/api/)
 6. for endpoints go to [swagger](http://localhost/swagger/index.html)
