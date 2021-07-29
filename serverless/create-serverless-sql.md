# Create Serverless SQL Database

Azure SQL Database serverless (preview) is a compute tier for single databases that automatically scales compute based on workload demand and bills for the amount of compute used per second. The serverless compute tier also automatically pauses databases during inactive periods when only storage is billed and automatically resumes databases when activity returns.

![Banner](media/azure-sql-banner.png)
<br>**Documentation: https://docs.microsoft.com/en-us/azure/sql-database/sql-database-serverless**
### Prerequisite: [Syllabus](./readme.md)

## Create an Azure SQL Database

1. Sign in to the [Azure portal](https://portal.azure.com/).
1. Click **Create a resource** in the upper left-hand corner of the Azure portal.
1. In the Search field, type in "Azure SQL" and choose **Azure SQL**, then click the **Create** button 
![Search for Azure SQL](media/24-search-azure-sql.png)
1. The following options will be presented, select **Single Database** from the **SQL databases** column
![Single Database](media/25-sql-server-options.png) 
1. Click the **Create** button and fill out the resource creation with the following parameters:

    | Setting      |  Suggested value   | Description                                        |
    | --- | --- | --- |
    | **Resource group** | ServerlessWkrshp | Use the same resource group for all services in this tutorial.|
    | **Name** | twitterdatabase | Enter the name of the database. |
    | **Server** | svrlessdemo | Enter a unique name for the server. |
    | **Server Admin Login** | mainuser | This is the main user name.|
    | **Password** | #Welcome1023# | Write this down you will not be able to recover it.|
    | **Location** | South Central US | Use the location nearest you. |
    
1. Follow the form flow to fill out the information
![Configure Database Server](media/27-configure-database-server.png)
1. Configure database
![Configure Database](media/28-configure-database.png)
1. Select serverless and click apply
![Select Serverless Computing](media/29-select-serverless.png)
1. Select Review and Create
![Select Review and Create](media/30-sql-review-create.png)

<br>

### Next: [Configure Access & Database](./sql-database-access.md) ###
#### Previous: [Syllabus](./readme.md) ####

