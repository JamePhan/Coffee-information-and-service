# Coffee Information And Service
## A. Install Backend:
1. Install WSL : Run `wsl --install` or `wsl --install Ubuntu`.
2. Install Docker Desktop: Run `winget install -e --id Docker.DockerDesktop`
3. Install SQL Server Management Studio: Run `winget install -e --id Microsoft.SQLServerManagementStudio`
4. Install Git : Run `winget install -e --id Git.Git`
5. Clone the repository with `git clone https://github.com/JamePhan/Coffee-information-and-service.git`
6. Open a terminal in root folder by hold  Shift + Right-click the "Coffee-information-and-service" folder and choose "Open Terminal / Command Prompt / Powershell here".
7. Run `docker-compose build` to build new back end docker image.
8. Run `docker-compose up` to create and start containers.
9. Run SSMS with "localhost,1433" as server name. Password is provided within appsettings.json file.
10. Execute the proviced sql script within "Resource" folder (DBFull.script).
11. Access APIs via localhost:5000/{api-link-here}.

### Note: 
- Whenever the source code is updated, run step 7 and 8 again.
- To remove deprecated built images, run `docker image prune`

## B. Install Frontend
1. [Install node.js](https://nodejs.org/en/download)
2. [Install Visual Studio Code](https://code.visualstudio.com/download)
3. Install NPM : Open terminal/command prompt, type in `npm install -g npm` then press "ENTER".
4. Open terminal/command prompt, type in `npm i` then press "ENTER".
5. In Folder Frond-End Create New File ".env" and Paste this content to file and save:

`NEXT_PUBLIC_APP_NAME= coffee_information_service`

`NEXT_PUBLIC_API= http://localhost:5000/api`

6. Open terminal from "Front-end" folder, type in `npm run dev` to run the application.
7. Open browser and access from "localhost:3000".

## C. Deployment Limitation
1. Backend can't be deployed to [AWS](https://aws.amazon.com/) free tier due to SQL Server Docker Image 2GB of RAM requirement.
2. With [ngrok](https://ngrok.com/), Front-End code need to add [ngrok-skip-browser-warning](https://ngrok.com/abuse) to every `GET` request header.
3. `<img>` tag image loading doesn't send a header, so any image load by this HTML tag will be broken.
