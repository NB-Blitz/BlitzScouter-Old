# Blitz Scouter
## What is this?
This is the GitHub repository for New Berlin Blitz's FRC app. It utilizes a data connection to send data from an app to a google sheets in order to compile large amounts of data.
## 2018 Season
During the 2018 Season, scouting data was collected as such:
1. 6 Scouters with the Blitz Scouter App (Downloaded via .apk) scouted robots
2. Data from app was sent to a raspberry pi server which put said data into a spreadsheet.
3. The server then uploaded the spreadsheet into google drive (Via GDriveFS).
4. A google script converted the spreadsheet into a google sheets.
5. The google script then appends a pre-made sheet onto the google sheet that compiles the data

During the 2018 Season, the app only supported Android as it was developed with Android Studio (This will be fixed during the 2019 season)

## 2019 Season
During the 2019 Season, the scouting app will be in-browser so it can support most operating systems. The app will be developed via ASP.NET MVC to send data to a pi server (which will also host the website) which collects and converts data to a google sheet.

## TODO
### Before Kick-Off
- ~~Upload 2018 season onto GitHub.~~
- ~~Redesign the app layout so it's not ugly.~~
- ~~Convert pre-made app into a website (XML to HTML & CSS).~~
- ~~Find better method of uploading data to google drive as a google sheet.~~
- Get desktop to host website correctly
### After Kick-Off
- Figure out what is needed to scout and include it in the app.
- Beta Test the App
