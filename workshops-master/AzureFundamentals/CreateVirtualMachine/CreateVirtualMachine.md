## Task Completion Options
- Create a Virtual Machine
    - [Create a Virtual Network via the Azure Portal](#azure-portal)
    - [Create a Virtual Network via Azure CLI](#azure-cli)
- [Connect to your virtual machines](#connect-to-your-virtual-machines)



## Azure Portal
### Create a Windows Server Virtual Machine
1. In the [Azure Portal](https://portal.azure.com), click the **+Create a resource** link at the top left of the page.
1. In the Search field, type *Windows Server* and click on **Windows Server** that appears in the drop down list.
![Select Azure Resource Group](media/1.png)
1. From **Select a software plan** choose **Windows Server 2019 Datacenter** and click **Create**
![Select Azure Resource Group](media/2.png)
1. In **Create a virtual machine**, enter or select this information:
    - `Instance details - Subscription`	Select your subscription.
    - `Instance details - Resource group`	Select **AzureLab**.
    - `Instance details - Virtual Machine name`	myVm1
    - `Instance details - Region`	Select South Central US.
    
    - `Administrator account - Username`	Enter your Local administrator account name.
    - `Administrator account - Password` and `Confirm password` Enter your Local adminsitrator account's password

    - `Inbound Port Rules - Public inbound ports` Choose **Allow selected ports** and then select **RDP (3389)** and from the drop-down.

   
    ![Select Azure Resource Group](media/3.png)
    ![Select Azure Resource Group](media/4.png)
1. Click **Next: Disks>**, leave all as default and click on **Next: Networking>**
1. Ensure **Virtual Network** is set to *myVirtualNetwork* and leave all else as default.

1. Click **Review + Create**, and then **Create** once validation has passed.
    ![Select Azure Resource Group](media/5.png)
1. Check the **Notifications** icon in the upper right and wait unitl you see **Resource group created**, then click the **Go to resource** button.
1. Make note of myVm1's Public IP Address
    ![Select Azure Resource Group](media/9.png)







### Create an Ubuntu Virtual Machine
1. In the [Azure Portal](https://portal.azure.com), click the **+Create a resource** link at the top left of the page.
1. In the Search field, type *Ubuntu* and click on **Ubuntu 18.04 lTS** that appears in the drop down list, then click the **Create** button.
![Select Azure Resource Group](media/6.png)

1. In **Create a virtual machine**, enter or select this information:
    - `Instance details - Subscription`	Select your subscription.
    - `Instance details - Resource group`	Select **AzureLab**.
    - `Instance details - Virtual Machine name`	myVm2
    - `Instance details - Region`	Select South Central US.
    
    - `Administrator account - Authentication type`	Select *Password*
    - `Administrator account - Username`	Enter your Local administrator account name.
    - `Administrator account - Password` and `Confirm password` Enter your Local adminsitrator account's password

    - `Inbound Port Rules - Public inbound ports` Choose **Allow selected ports** and then select **SSH (22)** and from the drop-down.
![Select Azure Resource Group](media/7.png)

1. Click **Next: Disks>**, leave all as default and click on **Next: Networking>**
1. Ensure **Virtual Network** is set to *myVirtualNetwork* and leave all else as default.
1. Click **Review + Create**, and then **Create** once validation has passed.
1. Check the **Notifications** icon in the upper right and wait unitl you see **Resource group created**, then click the **Go to resource** button.
1. Make note of myVm2's Public IP address
![Select Azure Resource Group](media/8.png)


## Azure CLI

### Create a Windows Server Virtual Machine


```azurecli-interactive
az vm create --resource-group AzureLab --name myVm1 --image Win2019Datacenter --admin-username '<LocalAdminUser>' --admin-password '<LocalAdminPassword>' --no-wait
```


### Create an Ubuntu Virtual Machine
```azurecli-interactive
az vm create  --resource-group AzureLab  --name myVm2  --image UbuntuLTS --generate-ssh-keys
```

The following output will be displayed for VM2 - Note the Public IP Address
~~~~~~~~~~
{
  "fqdns": "",
  "id": "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/myResourceGroup/providers/Microsoft.Compute/virtualMachines/Vm2",
  "location": "eastus",
  "macAddress": "00-0D-3A-23-9A-49",
  "powerState": "VM running",
  "privateIpAddress": "10.0.0.5",
  "publicIpAddress": "13.66.35.3",
  "resourceGroup": "AzureLab"
  "zones": ""
}
~~~~~~~~~~

## Connect to your Virtual Machines ##
----
### Windows Virtual Machine
1. Open the Remote Desktop Connection client on your workstation and click **More Options**
1. Input myVm1's Public IP address in the **Computer** field and your Local Administrator username as the **User name**, then click **Connect** and input your Local Administrator Password in the **Password** field. 
![Select Azure Resource Group](media/10.png)
1. Once logged in, open the **Command Prompt** and type *ping myVm2* followed by **Enter**, you will now receive replies from the Ubuntu virtual machine.
![Select Azure Resource Group](media/11.png)

### Ubuntu Virtual Machine
#### Connect to myVm2 using the public id using Bash
*If created following CLI instructions*
```sh
ssh  13.66.35.3
```

*If created following Azure Portal instructions*
```sh
ssh  username@13.66.35.3
```

You'll receive four replies from myVm1


[Next -> Create a File Share & Mount to VM]

[Create Virtual Network Documentation]: <https://docs.microsoft.com/en-us/azure/virtual-network/quick-create-portal>
[Create DNS Record Documentation]: <https://docs.microsoft.com/en-us/azure/dns/dns-getstarted-cli>
[Next -> Create a File Share & Mount to VM]:<https://github.com/Microsoft-USEduAzure/workshops/blob/master/AzureFundamentals/FileShare/CreateAFileShare.md>

