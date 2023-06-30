## Description

<b>Ingredients order</b> - is a sample web application for managing orders, creating, editing, and deleting them. In the application, you can create orders for the necessary materials, create and manage pallets with materials. Every pallet you can affix to a specific machine or area. In the "Bins" section, you can create bins for waste, generate Bar codes for each, and manage their status by affixing them to machines or areas. [This video is a demonstration of how an application works.](https://youtu.be/c3QdAG8lOrQ)


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

Open a project in IDE, in <b>```appsettings.json```</b> enter your server name, run the following commands:<br/>
<b>```add-migration MIGRATION_NAME```</b> then run <br/>
<b>```update-database```</b><br/>

You can run the app by pressing the "Play" button in your IDE or by running the following command in the terminal:
<br/>
<b>```dotnet run```</b>
<br/>

- Email - <b>```sara@gmail.com```</b>(can be changed if needed)
- Password - <b>```demo```</b>

After entering <b>```email```</b> and <b>```password```</b>, user moves to the main menu, in where user should start by creating ```main orders``` or adding materials for the orders in the ```Magazyn```.

<b>1. Creating ```main orders```</b><br/>
<br/>
Select the menu item ```Zlecenia```, click on the button ```Dodaj zlecenie```, when the window appears fill in all the fields and click ```Save```. 
<br/><br/>
![Create_Order](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/9b31f5e3-e606-4222-8b06-b3d786bb0988)
<br/><br/>
And the new ```main order``` will appear in the table. Here, user can manage orders by ```editing``` them or ```deleting```.<br/><br/>
![Order_After_Adding](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/19407ec7-0141-4c26-b85b-d26125fb870a)
<br/><br/>
By clicking on the button ```Szczegóły``` user can see ```order's``` details. And copy by clicking on the number ```material numbers``` which can be used in ordering pallets with materials and affixing them to the specific ```machine``` or ```area```. <br/><br/>
![Details_After_Clicking](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/75b43929-1c3f-4c78-b473-fe98503b61c4)
<br/><br/>

<b>2. Creating pallets with materials</b><br/><br/>
Select the menu item ```Magazyn```, click on the button ```Pallet adding```, when the window appears fill in all the fields and click ```Submit```. 
<br/><br/>
![Pallets_adding](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/4393dd14-d08c-4ec2-b56c-7d2115955ea3)
<br/><br/>
After appearing in the table, the user can see all the details about the pallet, including ```quantity``` of the material, ```location``` and the user can also by clicking on ```Konsumpcja``` change ```quantity``` or even ```delete``` the pallet. ```Manipulations``` with the pallet which was not ```submited``` by clicking on the button ```Zapisz``` will have no result. <br/><br/>
![Konsupcja_warehouse](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/21671b15-bd50-48bc-a9cb-a322b490e91e)
<br/><br/>
<b>3. Ordering materials</b><br/><br/>
Select the menu item ```Zamówienia```, then select from the dropdown menu ```Orders``` the ```main order```, from the dropdown ```Process``` select ```process/localization``` and from the ```Machine``` select a specific ```machine/area``` for affixing the pallet with materials. <br/><br/>
![Selecting_Machine](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/a32930d8-e274-4a35-aaa0-98d26364b895)
<br/><br/>
By clicking on the button ```Zamówienie```, the user can order ```materials``` from the warehouse and affix a pallet to the machine. When the window appears, the user should select ```materials``` by clicking on the ```Wybierz z listy```, which shows all the ingredients the ```main order``` contains and select material. There is no possibility of ordering the same material several times for the same ```main order``` by selecting from the list. If material was ordered ```one time``` it will be removed from the list. But, the user ```can order``` more material if needed by putting ```material number``` which can be ```copied``` from the ```main order's``` details, and ```quantity``` to the inputs. After submitting, a ```dialog window``` for comment will appear, in which the user should write why he/she is ordering the material that was ```ordered``` early for the same ```main order```. <br/><br/>

![OrderingMaterialsList](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/ac1d3163-a680-491e-b662-323c39c7bc56)  <br/><br/>

And the pallets with ```materials``` will appear in the table. Now, every pallet is affixed to a specific ```machine``` and ```main order```. The ```locks``` on the table show that the pallets have not been delivered yet, and until that time, the user can't do any manipulations with the pallets. <br/><br/>
![MachineSelectAfterOrdering](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/c295f5d9-b8f3-4978-91a8-165f960ae99b) <br/><br/>
For ```unlocking``` the pallets, the user should go to ```Magazyn``` and click on ```Zamówienia```. By clicking on ```Zmień status``` and selecting ```Dostarczone```, the user can change the status of the pallet and ```unlock``` it for manipulations from ```Zamówienia```. The pallets, which are delivered, can be ```removed``` from the orders by clicking ```Usuń```.
<br/><br/>
![UnlockPallet](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/60092cc5-af64-4ec5-9106-585c87adaea0)
<br/><br/>
Now, the user can make manipulations with pallets that have been ordered. The user can ```consume``` quantity, ```reverse``` or move pallet to the ```warehouse```.
![AfterUnlocking](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/ebdfa420-9a1b-466c-b6d2-084348c8fc43)<br/><br/>

<b>4. Waste</b><br/><br/>
Select the menu item ```Odpad```. In this section, the user can create ```bins``` for waste and manage them, like changing  ```status```, generating ```barcode``` or ```attaching/detaching```.
All ```bins``` should have a unique ```specific``` number in the format ```00-00-00-000```. Click on ```Bin adding``` and in the window write ```unique``` bin's number, then click ```Submit```. <br/><br/>
![BinAdding](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/0893ca7e-e849-413e-9deb-580883b17618) <br/><br/>
And a new ```bin``` will appear in the table. <br/><br/>
![BinAfterAdding](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/eb505423-908b-48b3-8ebd-9b9870f75e91) <br/><br/>
By clicking on the button ```Bar code```, user can generate ```barcode```for the selecting ```bin```. The user can also generate ```barcode``` by clicking on the button ```Generate bar code``` and entering the bin's number. The result will be the same. <br/><br/>
![BarCode](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/36b70845-2704-4f7e-9221-c376659641b1) <br/><br/>
For ```attaching/detaching``` bins to the machine or area, the user should click on the button ```Attach/Detach```. In the window that will appear, the user should select from the list ```process/area``` and enter ```bin's number```. If the entered bin is ```Free to use```, the user can attach it to the selected area.<br/><br/>
![BinFree](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/47167b5c-c710-40ec-8307-4b11d41c09a6)<br/><br/>
Otherwise, a warning will appear. <br/><br/>
![BinUsing](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/193c9cf1-9d92-4fdc-ab36-39748e5e0fcf)<br/><br/>
There are two ways of ```detaching``` bins. The first one - click on ```Attach/Detach```, select ```Process/Area``` where the bin has been ```attached``` and click ```Detach```.<br/><br/>
![Detaching](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/7ed6721a-4124-4e00-a4a7-5b14797926c7)<br/><br/>
The other way is from the table - find which bin you want ```detach```, click on ```Details``` and then click on ```Detach```. <br/><br/>
![DetachingSecond](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/3690e333-a99b-4ce0-813d-a459af749b4d)<br/><br/>
```Working area``` is a representation of ```processes/areas```. Here, user can also ```attach/detach``` bins. For ```attaching```, click on the selected empty ```area``` and enter ```bin's number```. For ```detaching```, click on the selected  ```area``` and ```confirm``` detaching.<br/><br/>

![WorkingAreaDetaching](https://github.com/IhorZhylchuk/Ingredients-order/assets/57155768/d89ece00-ee5f-4091-8685-20c9585ce73c)










