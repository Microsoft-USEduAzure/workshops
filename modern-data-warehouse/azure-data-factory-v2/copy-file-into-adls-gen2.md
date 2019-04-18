# Build copy pipeline using Azure Data Factory

## Pre-requisite task: [Create Azure Data Factory V2](azure-data-factory-v2/provision-azure-data-factory-v2.md)

## Task: Copy JSON file to Azure Data Lake Gen 2

1. In Data Factory workspace, click on the **Author** icon, click the **plus** icon, then click **Pipeline**

    ![Create new pipeline](media/pipeline/1.png)

1. In the Pipeline editor, drag and drop the **Copy Data** activity onto the work area

    ![Add copy data activity](media/pipeline/2.png)

1. Click on the **Copy Data1** activity and click **+ New** from the **Source** tab to configure a new linked service

    ![Configure source](media/pipeline/3.png)

1. In the new Dataset pane, find **HTTP** and click **Continue**

    ![Add HTTP dataset](media/pipeline/4.png)

1. In the HTTP Dataset editor, click on the **Connection** tab, click the **+ New** button, and configure the file settings

    - Base URL: *https://raw.githubusercontent.com/Azure/usql/master/Examples/Samples/Data/json/radiowebsite/small_radio_json.json*
    - Authentication type: *Anonymous*

    ![Configure HTTP dataset](media/pipeline/5.png)

1. Click **Test connection** 

1. Click **Finish**

1. Back in the HTTP Dataset editor Connection tab, ensure **File format** is set to **JSON format**

    ![File format](media/pipeline/6.png)

1. You can preview the data

    ![Source file format](media/pipeline/7.png)

1. Back in the pipeline editor, click on the **Copy Data** activity, click on **Sink**, and click **+ New** to configure.

    ![Configure sink](media/pipeline/8.png)

1. 1. In the new Dataset pane, find **Azure Data Lake Storage Gen2** and click **Continue**

    ![Add HTTP dataset](media/pipeline/9.png)

1. In the Azure Data Lake Storage Gen2 Dataset editor, click on the **Connection** tab, and click the **+ New** button.

    ![Configure HTTP dataset](media/pipeline/10.png)

1. In the linked service settings, select your ADLS account from your subscription, test the connection, and click **Finish**.

    ![Configure HTTP dataset](media/pipeline/11.png)

1. In the connection tab, enter the **File path** and filename

    > **NOTE:** The file path you enter will be the name of the file system in ADLS. Make sure you copy this value to notepad for later use.

    ![Configure HTTP dataset](media/pipeline/12.png)

1. Click the **Publish** button to save the pipeline.

    ![Configure HTTP dataset](media/pipeline/13.png)

1. Click the **Debug** button to run the copy activity.

    ![Configure HTTP dataset](media/pipeline/13a.png)

## Next task: [Create Azure Databricks](../azure-databricks/provision-azure-databricks.md)