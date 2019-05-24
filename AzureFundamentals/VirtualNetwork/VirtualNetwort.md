[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

# Create a virtual Network using CLI

In this document you will learn how to:

- Create  Virtual Network
  - Create 2 VM Machines
  - Log into a VM Machine using ssh
- Create a DSN
  - Add a record
- Create file Share & connect to VM

----

## Create a Virtual Network

You first need to create a network on the same recource group you created on the previous excercise:

```sh
az network vnet create --name myVirtualNetwork --resource-group AzureLab --subnet-name default
```
Further documentation:  [Create Virtual Network Documentation]

----

### Create 2 Ubuntu Virtual Machines

```sh
az vm create  --resource-group AzureLab  --name myVm1  --image UbuntuLTS --generate-ssh-keys --no-wait

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
  "publicIpAddress": "40.68.254.142",
  "resourceGroup": "AzureLab"
  "zones": ""
}
~~~~~~~~~~

----
## Connect to myVm2 using the public id using Bash

```sh
ssh  40.68.254.142
```

Now ping myVm1
```sh
ping myVm1 -c 4
```

You'll receive four replies from myVm1

## Create DNS zone

```sh
az network dns zone create -g AzureLab -n adcDemo.xyz
```
----

### Create DNS Record
```sh
az network dns record-set a add-record -g AzureLab -z useduDemo.xyz -n www -a 10.10.10.10
```
Further documentation: [Create DNS Record Documentation]

### List the DNS records in the zone

```sh
az network dns record-set list -g AzureLab -z useduDemo.xyz
```
Take note of one of the servers listed

### Test Name Resolution 
```sh
nslookup www.USEDUDemo.xyz ns1-06.azure-dns.com
```
[Next -> Create a File Share & Mount to VM]

[Create Virtual Network Documentation]: <https://docs.microsoft.com/en-us/azure/virtual-network/quick-create-portal>
[Create DNS Record Documentation]: <https://docs.microsoft.com/en-us/azure/dns/dns-getstarted-cli>
[Next -> Create a File Share & Mount to VM]:<https://github.com/Microsoft-USEduAzure/workshops/blob/master/AzureFundamentals/FileShare/CreateAFileShare.md>

