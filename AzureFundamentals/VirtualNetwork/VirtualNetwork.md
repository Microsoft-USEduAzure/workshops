# Create a Virtual Network

## Task Completion Options
- [Create a Virtual Network via the Azure Portal](#azure-portal)
- [Create a Virtual Network via Azure CLI](#azure-cli)


## Azure Portal
1. In the [Azure Portal](https://portal.azure.com), click the **+Create a resource** link at the top left of the page.
1. In the Search field, type *Virtual* and click on **Virtual Network** that appears in the drop down list.
![Select Azure Resource Group](media/1.png)
1. Click the **Create** button.
1. In **Create virtual network**, enter or select this information:
    - `Name`	Enter myVirtualNetwork.
    - `Address space`	Enter 10.1.0.0/16.
    - `Subscription`	Select your subscription.
    - `Resource group`	Select AzureLab.
    - `Location`	Select South Central US.
    - `Subnet - Name`	Enter myVirtualSubnet.
    - `Subnet - Address` range	Enter 10.1.0.0/24.

    ![Select Azure Resource Group](media/2.png)
1. Leave the rest of the defaults and select Create.

----

## Azure CLI

You first need to create a network on the same recource group you created on the previous excercise:

```sh
az network vnet create --name myVirtualNetwork --resource-group AzureLab --subnet-name default
```
Further documentation:  [Create Virtual Network Documentation](https://docs.microsoft.com/en-us/azure/virtual-network/quick-create-portal)

### Next: [Create Virtual Machines](../CreateVirtualMachine/CreateVirtualMachine.md) ###
