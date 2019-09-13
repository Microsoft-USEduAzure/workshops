# Provision Azure Resource Group

## Task: Create resource group using Azure CLI

We will create a Resource Group called **EDUMDW-Lab** - please select the nearest data center


1. In Azure Portal, open Cloud Shell

1. Execute the following command using Bash

    ```
    az group create -n EDUMDW-Lab -l <ENTER_LOCATION_NAME>
    ```

## Next task: [Create Azure Blob Storage](../azure-storage/provision-azure-storage-account.md)