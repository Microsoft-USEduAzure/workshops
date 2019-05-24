[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

# Create a Resource Group

Azure Resources Groups are logical collections of virtual machines, storage accounts, virtual networks, web apps, databases, and/or database servers. Typically, users will group related resources for an application, divided into groups for production and non-production — but you can subdivide further as needed.

![Resource Group](resourcegroup.png)

## CLI Command to Create Azure Group

az group create --name AzureLab --location eastus2

> We will use the name of your resource group across this lab

Where:

--name --> the name of your group

--location --> region where to launch it


[Next -> Create a Virtual Network]

 [Next -> Create a Virtual Network]:<https://github.com/MarchingBug/AzureFundamentals/blob/master/VirtualNetwork/VirtualNetwort.md>
