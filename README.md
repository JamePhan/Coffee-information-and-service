# Coffee Information And Service

1. [Install WSL](https://learn.microsoft.com/en-us/windows/wsl/install)
2. [Install Docker Desktop](https://www.docker.com/)
3. [Install SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)
4. Open a terminal in root folder.
5. Run "docker build -t back ." to build new back end docker image.
6. Run "docker-compose up" to create and start containers.
7. Run SSMS with "localhost,1433" as server name. Password is provided within appsettings.json file.
8. Execute the proviced sql scripts within "Resource" folder (DBSchema first, then DBData).
9. Access APIs via localhost:5000/{api-link-here}.
