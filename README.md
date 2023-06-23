## Description

<b>Ingredients order</b> - is a sample of web application for managing orders, creating, editing and deleting them. In the application you can create orders for warehouse for the necessary materials, creat and manage paletts with materials. Every palett you can affix to specific machine or area. In "Bin's" section, you can create bin's for waste, generate Bar code for each and manage their status by affixing to machines or areas. [On the video you can see how the application works.](https://youtu.be/c3QdAG8lOrQ)


#### Prerequirements
- Visual Studio 2019
- SQL Server

#### Technologies and Frameworks
- ASP.NET Core 3.1
- JavaScript,
- jQuery,
- Entity Framework Core 5.0,
- Bootstrap 5

#### How To Run
Clone this repository to your local machine:<br/>
<b>````git clone https://github.com/IhorZhylchuk/Ingredients-order.git````</b>
<br/>
<br/>
Navigate to the project directory and restore dependencies:
<br/>
<b>```cd YOUR_REPOSITORY```</b><br/>
<b>```dotnet restore```</b><br/>

Open project in IDE, in <b>```appsettings.json```</b> enter your server name, run the following commands:<br/>
<b>```add-migration MIGRATION_NAME```</b> then run <br/>
<b>```update-database```</b><br/>

You can run the app by pressing the "Play" button in your IDE or by running the following command in the terminal:
<br/>
<b>```dotnet run```</b>
<br/>

- Email - <b>```sara@gmail.com```</b>(can be changed if needed)
- Password - <b>```demo```</b>

