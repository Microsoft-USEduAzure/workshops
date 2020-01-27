# Develop Azure Databricks Notebook for Big Data

## Pre-requisite tasks: 
 
 - [Create SQL Data Warehouse](../azure-sql-datawarehouse/provision-azure-sql-data-warehouse.md)

 - [Create workspace](create-workspace.md)

## Task: Transform data using Azure Databricks notebook

> **NOTE:** See reference links below for notebook and markdown syntax guidance.


1. On the Azure Databricks portal, click the **Home** button on the left-hand side menu. 
2. On the **Workspace** blade, click the down arrow next to your user name and then click **Create > Notebook**.
3. On the **Create Notebook** pop-up window type “NYCTaxiData” in the Name field.
4. Ensure you have the **Language** field set to **Python** and the **Cluster** field is set to **NYCTAxiLookup**.
5. Click **Create**.

    ![](media/notebook/NYCTaxiData-notebook.png
)

1. In the notebook, enter the following bits of code into new cells and click the play button and **Run Cell** to execute the command against your Spark cluster.

    > **NOTE:** You'll need to be sure your notebook is attached to your cluster. You can check in the upper left corner of the notebook pane directly under the notebook name. If the notebook states **Detached** you can click the drop down to select a cluster to run against. 

    ![](media/notebook/2.png)

1. For each command listed below, you can enter them into new cells. To create a new cell, hover your mouse over the bottom edge of the cell to enable the **plus** icon. Clicking this will allow you to add a new cell.

    ![](media/notebook/3.png)

1. You can also add **markdown** to your cells to document the data transformation steps taken along the way. To add markdown, simply begin the cell contents with `%md` and enter your markdown.

    ![](media/notebook/4.png)

    ![](media/notebook/5.png)

1. Proceed to enter the following code in new cells and run along the way to view the data:

    - ### Configure Spark to authenticate against ALDS using SPN and Tenant details. 
    
        > These are values you documented in [Task 4: Create Azure Service Principal](../azure-ad-service-principal/create-service-principal.md)

        ```
        spark.conf.set("fs.azure.account.auth.type.<ENTER_YOUR_ALDS_STORAGE_ACCOUNT_NAME>.dfs.core.windows.net", "OAuth")
        spark.conf.set("fs.azure.account.oauth.provider.type.<ENTER_YOUR_ALDS_STORAGE_ACCOUNT_NAME>.dfs.core.windows.net", "org.apache.hadoop.fs.azurebfs.oauth2.ClientCredsTokenProvider")
        spark.conf.set("fs.azure.account.oauth2.client.id.<ENTER_YOUR_ALDS_STORAGE_ACCOUNT_NAME>.dfs.core.windows.net", "<ENTER_YOUR_SPN_APPLICATION_ID>")
        spark.conf.set("fs.azure.account.oauth2.client.secret.<ENTER_YOUR_ALDS_STORAGE_ACCOUNT_NAME>.dfs.core.windows.net", "<ENTER_YOUR_SPN_APPLICATION_SECRET>")
        spark.conf.set("fs.azure.account.oauth2.client.endpoint.<ENTER_YOUR_ALDS_STORAGE_ACCOUNT_NAME>.dfs.core.windows.net", "https://login.microsoftonline.com/<ENTER_YOUR_TENANT_ID>/oauth2/token")
        ```

    - ### Read JSON file using Azure Blob Filesystem (ABFS) driver. 
    
        > File system is what you entered in the file path text box while configuring the sink in [Task 7: Build copy pipeline using Azure Data Factory](../azure-data-factory-v2/copy-file-into-adls-gen2.md)

        ```
        val df = spark.read.json("abfss://nyclocationlookup@<ENTER_YOUR_ALDS_STORAGE_ACCOUNT_NAME>.dfs.core.windows.net/small_radio_json.json")
        ```

    - ### Display the data.

        ```
        df.show()
        ```

    - ### Load only specific columns.
      
        For the next command, click on the command option show title

        ![](media/notebook/show-title.png)

		In the title window type: **Load only specific columns sample**

        ```
        val specificColumnsDf = df.select("Borough", "Zone")
        
        specificColumnsDf.show()
        ```

    - ### Rename a column.      

		In the title window type: **Rename column example**

        ```
        val renamedColumnsDF = specificColumnsDf.withColumnRenamed("Borough", "Borough Name")
        
        renamedColumnsDF.show()
        ```
    - ### Configure access to storage account for temporary storage. 
    
        > These are values you documented in [Task 2: Create Azure Blob Storage](../azure-storage/provision-azure-storage-account.md)

        ```
        val blobStorage = "<ENTER_YOUR_BLOB_STORAGE_ACCOUNT_NAME>.blob.core.windows.net"
        val blobContainer = "nyclocationlookup"
        val blobAccessKey =  "<ENTER_YOUR_BLOB_STORAGE_ACCOUNT_KEY>"
        ``` 

    - ### Configure temporary directory using Windows Azure Storage Blob (WASB) driver.

        ```
        val tempDir = "wasbs://" + blobContainer + "@" + blobStorage +"/tempDirs"
        ```
    
    - ### Configure account access.

        ```
        val acntInfo = "fs.azure.account.key."+ blobStorage
        
        sc.hadoopConfiguration.set(acntInfo, blobAccessKey)
        ```

    - ### Configure access to SQL Datawarehouse. 
    
        > These are the values you documented in [Task 5: Create Azure SQL Data Warehouse](../azure-sql-datawarehouse/provision-azure-sql-data-warehouse.md)

        ```
        val dwDatabase = "EDUMDWDataWarehouse"
        val dwServer = "<ENTER_YOUR_DW_SERVER_NAME>.database.windows.net"
        val dwUser = "EduMdwAdmin"
        val dwPass = "P@$$word123"
        val dwJdbcPort =  "1433"
        val dwJdbcExtraOptions = "encrypt=true;trustServerCertificate=true;hostNameInCertificate=*.database.windows.net;loginTimeout=30;"
        val sqlDwUrl = "jdbc:sqlserver://" + dwServer + ":" + dwJdbcPort + ";database=" + dwDatabase + ";user=" + dwUser+";password=" + dwPass + ";$dwJdbcExtraOptions"
        val sqlDwUrlSmall = "jdbc:sqlserver://" + dwServer + ":" + dwJdbcPort + ";database=" + dwDatabase + ";user=" + dwUser+";password=" + dwPass
        ```

    - ### Load transformed data into SQL Datawarehouse.

        > **NOTE**: Make sure your data warehouse is running before executing this command.

        ```
        spark.conf.set(
            "spark.sql.parquet.writeLegacyFormat",
            "true")

        df.write
            .format("com.databricks.spark.sqldw")
            .option("url", sqlDwUrlSmall) 
            .option("dbtable", "Staging.NYCTaxiLocationLookup")
            .option( "forward_spark_azure_storage_credentials","True")
            .option("tempdir", tempDir)
            .mode("overwrite")
            .save()
        ```

### Reference: https://docs.databricks.com/user-guide/notebooks/notebook-use.html
### Reference: https://www.markdownguide.org/basic-syntax
### Reference: https://docs.azuredatabricks.net/user-guide/secrets/secret-scopes.html#akv-ss

## Next task: [Update Azure Data Factory pipeline to transform data using Databricks](../azure-data-factory-v2/transform-data-using-databricks.md)

## Tasks: 
1. Build an Azure Databricks notebook to explore the data files you saved in your data lake in the previous exercise. You will use Python and SQL commands to open a connection to your data lake and query data from data files.
1. Integrate datasets from Azure SQL Data Warehouse to your big data processing pipeline. Databricks becomes the bridge between your relational and non-relational data stores.

## Provision 

1. In Azure Portal, open Cloud Shell

1. Execute the following command using Bash

    ```
    az group create -n EDUMDW-Lab -l <ENTER_LOCATION_NAME>
    ```

## Next task: [Create Azure Blob Storage](../azure-storage/provision-azure-storage-account.md)
