# GripCaseStudy
The project is a Rest API project written in .NET Core 7 version. <br>
In general, a simple approach is considered and it serves with 3 controllers. <br>
The user registered in the system must first be identified. <br>
Subsequently, it can perform image upload operations. <br>
Images are kept as Base64 string in the database. <br>
The database is SQLite.<br>
When the project first stands up, it creates the necessary 2 tables and adds 1 user to the user table.

The project runs on Azure with App Service. <br>
This service is Linux based.<br>
The codes are hosted publicly on GitHub. <br>
Also GitHub Actions are used for CI/CD processes.<br>
This integration is done via YAML file.<br>

There are 3 controllers in the project.<br><br>
<b>TokenController</b> is the contoller where the authorization process is done.<br> 
A <b>"token"</b> is obtained with the Post request sent here. <br>
In this way, the user is authorized. The username "<b>admin</b>" and password "<b>password</b>" are predefined.

<b>ImageController</b> is designed to upload the desired file, list all files belonging to the user and obtain both raw and thumbnail versions of the desired file in Base64 string format.

<b>HealthController</b> is written for App Service Health probe on Azure.

Within the project there is a folder called <b>PostmanCollection</b> and a file inside it. <br>
This file can be used to import the necessary requests to Postman. <br>
The requests are added in order.

