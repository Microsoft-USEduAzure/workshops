# Provision Azure Storage Account

## Task: Create Azure Blob Storage

### We'll be creating this blob storage resource to temporarily store our data prior to loading into the data warehouse. 

1. In the [Azure Portal](https://portal.azure.com), click **+Create a resource** link at top left of the page

1. In the Azure Marketplace search bar, type **storage** and click on **Storage account** that appears in the drop down list

    ![New](img/1.png)

1. Click the **Create** button.

1. Enter the following and click the **Review + create** button then click **Create** once validation has passed:
    - Resource Group: *Select your resource group*
    - Name: *Enter a storage account name **(NOTE: Must be globally unique, must be lowercase and alphanumeric only)***
    - Location: *Select your location*
    - Performance: *Standard*
    - Account kind: *StorageV2 (general purpose v2)*
    - Replication: *Locally-redundant storage (LRS)*
    - Access tier (default): *Hot*

    ![New data factory](img/2.png)

1. Check the **Notifications** icon in the upper right and wait until you see **Deployment succeeded** then click the **Go to resource** button.

    ![Notifications](img/3.png)

1. In the Storage account blade, click on **Blobs**

    ![Notifications](img/4.png)

1. In the Blobs blade, click on the **+ Container** button, enter a name, then click the **OK** button. Copy the container name to notepad for later use.

    ![Notifications](img/5.png)

1. Navigate to the **Access keys** blade and copy the following values to notepad for later use:

    - Storage URL: **<YOUR_STORAGE_ACCOUNT_NAME>**.blob.core.windows.net
    - Storage Access Key: **<LOCATED_IN_ACCESS_KEYS_BLADE>**

1. In the blob container, click the **Upload** button and upload an Excel file with data from your PC

    ![](img/6.png)

1. Select the blob file from the list and click the **Create snapshot** button

    ![](img/7.png)

1. In the original Excel file on your PC, empty the records and upload the file again, making sure the **Overwrite** checkbox is checked

1. Download the file and open to make sure the records are gone

1. In the blob list, click on **View snapshots**

    ![](img/8.png)

1. Select the snapshot and click the **Promote** button then click **OK**

    ![](img/9.png)

1. Download the file again from the blob file list and now make sure the data is back to its original state