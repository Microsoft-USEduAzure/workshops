# Provision Azure Data Lake Gen2

## Pre-requisite task: [Create Azure Resource Group](../azure-resource-group/create-resource-group.md)

## Task: Create Azure Data Lake Gen 2

### We'll be creating this resource to store our raw data files.

1. In the Azure Portal, open Azure Cloud Shell

1. Execute either of the following scripts based on your language preference:

    ### Powershell
    ```
    $resourceGroup = "EDUMDW-Lab"
    $name = "edumdwdatalake+YOURINITIALS"
    $location = "<ENTER_YOUR _PREFERRED_LOCATION>"

    # Install the Az Storage module
    Install-Module Az.Storage -Repository PSGallery -AllowPrerelease -AllowClobber -Force

    New-AzStorageAccount -ResourceGroupName $resourceGroup -Name $name -Location $location -SkuName Standard_LRS -Kind StorageV2 
    ```
    
    ### Bash
    ```
    resourceGroup=EDUMDW-Lab
    name=edumdwdatalake+YOURINITIALS
    location=<ENTER_YOUR _PREFERRED_LOCATION>

    # Enable extension to interact with ADLS Gen2
    az extension add --name storage-preview

    az storage account create 
        --name $name 
        --resource-group $resourceGroup 
        --location $location 
        --sku Standard_LRS 
        --kind StorageV2 
        --hierarchical-namespace true
    ```

## Next task: [Create Azure Service Principal](../azure-ad-service-principal/create-service-principal.md)
