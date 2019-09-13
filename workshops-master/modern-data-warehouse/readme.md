# Azure Modern Data Warehouse Workshop

This workshop will aim to get you more familiar with the tools used to build a modern data warehouse. We will use Azure Data Factory to load a file into Azure Data Lake Gen 2. From there we will transform the data inside of Azure Databricks and load into an Azure SQL Data Warehouse. Lastly, we'll use various reporting tools to connect to our data warehouse.

![](media/modern-data-warehouse.png)

### Pre-requisites:
  - Ensure users are able to create App Registrations within Azure Active AD tenant
  
  - Ensure the following Resource providers are registered within subscription
  
    - Microsoft.DataFactory
    
    - Microsoft.Databricks
    
    - Microsoft.Sql

### [Task 1: Create Azure Resource Group](azure-resource-group/create-resource-group.md)

### [Task 2: Create Azure Blob Storage](azure-storage/provision-azure-storage-account.md)

### [Task 3: Create Azure Data Lake Gen 2](azure-data-lake-gen2/provision-azure-datalake-gen2.md)

### [Task 4: Create Azure Service Principal](azure-ad-service-principal/create-service-principal.md)

### [Task 5: Create Azure SQL Data Warehouse](azure-sql-datawarehouse/provision-azure-sql-data-warehouse.md)

### [Task 6: Create Azure Data Factory V2](azure-data-factory-v2/provision-azure-data-factory-v2.md)

### [Task 7: Build copy pipeline using Azure Data Factory](azure-data-factory-v2/copy-file-into-adls-gen2.md)

### [Task 8: Create Azure Databricks](azure-databricks/provision-azure-databricks.md)

### [Task 9: Create Azure Databricks Cluster](azure-databricks/create-spark-cluster.md)

### [Task 10: Create Azure Databricks Workspace](azure-databricks/create-workspace.md)

### [Task 11: Develop Azure Databricks notebook](azure-databricks/develop-databricks-notebook.md)

### [Task 12: Update Azure Data Factory pipeline to transform data using Databricks](azure-data-factory-v2/transform-data-using-databricks.md)

### [Task 13: Verify data](azure-sql-datawarehouse/verify-data.md)

### [Task 14: Visualize data](power-bi/visualize-data.md)
