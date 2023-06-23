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

After entering <b>```email```</b> and <b>```password```</b>, user moves to the main menu. After clicking on the ```button```
![Main_Menu](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/ddf81ce3-3540-4586-9e15-7b3e6b2ffba2) <br/><br/>
and filling ```all the fields``` <br/><br/>
![Create_Order](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/b7bb9f38-1405-4e19-ae3b-1f5b6db61be2)
<br/><br/>
user creates ```main order```, which can be ```changed``` or ```deleted```. <br/>
![Order_After_Adding](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/da00fcfe-7d17-446b-989a-8b2d521a9996)
<br/><br/>
If the user wants to see ```details``` of the order, he/she should click on the specific ```button```<br/><br/>![Details_Before_Clicking](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/e37ecc18-55b4-44fc-aaba-fe95406bd5ca)
 <br/><br/>
and the window with ```details``` will appear. User can see details and copying ```material numbers``` by clicking<br/><br/>
![Details_Coping](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/d6f13604-8b9e-42f2-a202-7f4ac9b3ea35) <br/><br/>
The next step is creating pallets with materials 



