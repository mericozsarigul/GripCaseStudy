# GripCaseStudy
The project is a Rest Api project written in .NET Core 7 version. In general, a simple approach is considered and it serves with 3 methods. The user registered in the system must first be identified. Subsequently, it can perform image upload operations. Images are kept as Base64 string in the database. The database is SQLite. When the project first stands up, it creates the necessary 2 tables and adds 1 user to the user table.

The project runs on Azure with App Service. This service is Linux based.
The codes are hosted publicly on GitHub. 
Also GitHub Actions are used for CI/CD processes.
This integration is done via YAML file.

There are 3 controllers in the project.
TokenController is the contoller where the authorization process is done. A "token" is obtained with the Post request sent here. In this way, the user is authorized. The username "admin" and password "password" are predefined.

ImageController is designed to upload the desired file, list all files belonging to the user and obtain both raw and thumbnail versions of the desired file in Base64 string format.

HealthController is written for App Service Health probe on Azure.


