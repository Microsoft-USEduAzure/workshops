# Create Service Principal for Databricks

## Pre-requisite task: [Create Azure Data Factory V2](../azure-data-factory-v2/provision-azure-data-factory-v2.md)

## Task: Create service principal and add role assignment for ADLS Gen2.

1. In the Azure Portal search box, type "*azure active*" and click on **Azure Active Directory**.

    ![Navigate to Azure AD](media/navigate-to-azure-ad.png)

1. In the Azure Active Directory blade, click on **App registrations**.

    ![App registration](media/app-registration.png)

1. Click on the **+ New application registration** button

1. Enter a name for the app and a sign-on URL (NOTE: The sign-on URL does not need to be an actual URL but needs to be URL encoded)

    ![App registration settings](media/app-registration-settings.png)

1. Click the **Create** button.

1. The registered app blade will be displayed upon creation. Copy the **Application ID** to notepad for later use.

    ![Copy app IDs](media/copy-ids.png)

1. Click on **Settings**, click **Keys** in the settings blade, enter a **key description**, select a **duration**, click the **Save** button, and copy the **value** that is generated to notepad for later use.

    > **NOTE:** YOU ONLY GET ONE CHANCE TO VIEW THE VALUE SO BE SURE TO COPY THIS; OTHERWISE YOU'LL HAVE TO GENERATE A NEW KEY.
    
    ![Copy app IDs](media/generate-secret.png)

1. Navigate back to the Azure Active Directory blade, click **Properties**, and copy the **Directory ID** to notepad for later use.

    ![Copy app IDs](media/copy-ids-2.png)

1. Navigate back to your Azure Data Lake Gen2 resource, click on **Access control (IAM)**, click the **+ Add** button, then click **Add role assignment**.

    ![Copy app IDs](media/add-role-assignment-to-adls.png)

1. In the role assignment blade, be sure to select the **Storage Blob Data Owner** role, and begin typing in your SPN in the **Select** text box. When your role appears in the list, click it to select it, then click **Save**.

    ![Copy app IDs](media/select-role-for-assignment.png)

## Next task: [Create Azure Databricks](../azure-databricks/provision-azure-databricks.md)