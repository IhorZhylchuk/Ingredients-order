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

After entering <b>```email```</b> and <b>```password```</b>, user moves to the main menu in which user should start from creating ```main orders``` or adding materials for the orders in the ```Magazyn```.

<b>1. Creating ```main orders```</b><br/>
<br/>
Select menu item ```Zlecenia```, click on the button ```Dodaj zlecenie```, when the window appear fill all the fields and click ```Save```. 
<br/><br/>
![Create_Order](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/9b31f5e3-e606-4222-8b06-b3d786bb0988)
<br/><br/>
And the new ```main order``` will appear in the table. Here user can manage orders by ```editing``` them or ```deleting```.<br/><br/>
![Order_After_Adding](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/19407ec7-0141-4c26-b85b-d26125fb870a)
<br/><br/>
By clicking on the button ```Szczegóły``` user can see ```order's``` details. And copy by clicking on the number ```material numbers``` which can be used in ordering pallets with materials and affixing them to the specific ```machine``` or ```area```. <br/><br/>
![Details_After_Clicking](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/75b43929-1c3f-4c78-b473-fe98503b61c4)
<br/><br/>

<b>2. Creating pallets with materials</b><br/><br/>
Select menu item ```Magazyn```, click on the button ```Pallet adding```, when the window appear fill all the fields and click ```Submit```. 
<br/><br/>
![Pallets_adding](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/4393dd14-d08c-4ec2-b56c-7d2115955ea3)
<br/><br/>
After appearing in the table, user can see all the details about the pallet, ```quantity``` of the material, ```location``` and the user can also by clickin on ```Konsumpcja``` change ```quantity``` or even ```delete``` the pallet. ```Manipulations``` with the pallet which was not ```submited``` by clicking on the button ```Zapisz``` will have no result. <br/><br/>
![Konsupcja_warehouse](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/21671b15-bd50-48bc-a9cb-a322b490e91e)
<br/><br/>
<b>2. Ordering materials</b><br/><br/>
Select menu item ```Zamówienia```, then secelet from the dropdown menu ```Orders``` the ```main order```, from the dropdown ```Process``` select ```process/localization``` and from the ```Machine``` select specific ```machine/area``` for affixing the pallet with materials. <br/><br/>
![Selecting_Machine](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/a32930d8-e274-4a35-aaa0-98d26364b895)
<br/><br/>
By clicking on the button ```Zamówienie```, user can order ```materials``` from the warehouse and affix pallet to the machine. When the window appear, user should select ```materials``` by clicking on the ```Wybierz z listy```, which shows all the ingredients the ```main order``` contain and select material. There are no possibility for ordering the same material several times by selecting from the list. If material was ordered ```one time``` it will gone from the list. But, the user ```can order``` more material if needed by putting ```material number``` which can be ```copied``` from the ```main order's``` details and ```quantity``` to the inputs. <br/><br/>

![OrderingMaterialsList](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/ac1d3163-a680-491e-b662-323c39c7bc56)  <br/><br/>

And the pallets with ```materials``` will appear in the table. Now, every pallet is affixed for specific ```machine``` and ```main order```. The ```locks``` on the table shows that the pallets are not delivered yet and until that time, user can't do any manipulations with the pallets. <br/><br/>
![MachineSelectAfterOrdering](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/c295f5d9-b8f3-4978-91a8-165f960ae99b) <br/><br/>
For ```unlocking``` the pallets, user should go to ```Magazyn``` and click on ```Zamówienia```. By cliking on ```Zmień status``` and selecting ```Dostarczone```, user can change status of the pallet and ```unlock``` it for manipulations from ```Zamówienia```. The pallets, which are delivered, can be ```removed``` from the orders by clicking ```Usuń```.
<br/><br/>
![UnlockPallet](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/60092cc5-af64-4ec5-9106-585c87adaea0)
<br/><br/>
Now, user can make manipilations with pallets, which were ordered. User can ```consume``` quantity, ```reverse``` or move pallet to the ```warehouse```.
![AfterUnlocking](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/ebdfa420-9a1b-466c-b6d2-084348c8fc43)



