# Provision Azure Data Factory V2

## Pre-requisite task: [Create Azure Resource Group]()

## Task: Create Azure Data Factory V2

1. In Azure Portal, click **+Create a resource** link at top left of the page

1. In the Azure Marketplace search bar, type **data factory** and click on **Data Factory** that appears in the drop down list

    ![New](media/provision/1.png)

1. Click the **Create** button.

1. Enter the following and click **Create**:
    - Name: *adfv2demo0419* **(NOTE: Must be globally unique)**
    - Resource Group: Select **Create new** and enter *adfv2demo-rg*
    - Version: *V2*
    - Location: *West US 2*
    - Skip Git integration for now

        ![New data factory](media/provision/2.png)

1. Check the **Notifications** icon in the upper right and wait until you see **Deployment succeeded** then click the **Go to resource** button.

    ![Notifications](media/provision/3.png)

1. Click on **Author & Monitor** to navigate to the Azure Data Factory web workspace. This is where ADF development will take place.

## Optional task: [Configure Git repository](configure-git-repo.md)

## Next task: [Build copy pipeline using Azure Data Factory](copy-file-into-adls-gen2.md)