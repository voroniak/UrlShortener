## UrlShortener
URL shortening is used to create shorter aliases for long URLs. We call these shortened aliases “short links.” Users are redirected to the original URL when they hit these short links.

### How to start?

To start the app make sure you have Docker Desktop installed.

You can install Docker Desktop here: https://www.docker.com/products/docker-desktop/

Clone the repo:
```
git clone https://github.com/voroniak/UrlShortener.git
```
Open the cloned app in the terminal and run following command:
```
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```
Open your Browser and go to the:

http://localhost:8000/

### Notes

The main work was done on the backend side and basic unit tests have been written.

Added containerization of an application and database with docker.

I didn't pay much attention to the UI.
