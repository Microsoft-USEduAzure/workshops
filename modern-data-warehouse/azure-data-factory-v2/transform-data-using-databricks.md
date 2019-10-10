# Add Databricks notebook to pipeline

## Pre-requisite tasks: 
 
 - [Create Azure Data Factory V2 pipeline](copy-file-into-adls-gen2.md)

 - [Develop Azure Databricks notebook](../azure-databricks/develop-databricks-notebook.md)

## Task: Add Azure Databricks notebook activity to pipeline

1. In Azure Databricks workspace, click the user icon, then click **User settings**.

    ![](media/pipeline/14.png)

1. Click **Generate New Token**, enter a comment, and click **Generate**.

    ![](media/pipeline/15.png)

1. Copy the new token to notepad for later use.

    > **NOTE:** YOU ONLY HAVE ONE OPPORTUNITY TO SAVE THIS VALUE.

    ![](media/pipeline/16.png)

1. In the Azure Data Factory pipeline workspace, drag and drop the **Databricks Notebook** activity.

    ![](media/pipeline/17.png)

1. Name the activity **Copy Taxi Lookup Data** 
   
	![](media/pipeline/databricks-name.png)

1. Click on the notebook activity, click the **Azure Databricks** tab, then **+ New** to configure the linked service.

    ![](media/pipeline/18.png)

1. Select **From subscription**, choose your subscription, select **Access token**, select **New job cluster**, enter your Databricks access token, configure the Spark cluster, click **Test connection**, then click **Finish**. 

    - Cluster version: *Select **5.5 LTS (includes Apache Spark 2.4.3, Scala 2.11)***
    
    - Cluster node type: *Select **Standard_DS3_V2***
    
    - Python Version: *3*
    
    - Worker options: *1*

    > **NOTE:** New job cluster will provision new clusters for the pipeline activity then terminate as soon as the activity is complete.

    ![](media/pipeline/19.png)
    ![](media/pipeline/19a.png)

1. Click on the **Settings** tab and configure the notebook path

    ![](media/pipeline/20.png)

1. Connect the copy and notebook activities.

    ![](media/pipeline/21.png)

1. Publish the pipeline

    > **NOTE**: Make sure your data warehouse is running before executing this command.

1.	Publish your pipeline changes by clicking the **Publish all** button.

    ![](./Media/Lab2-Image33.png)

1.	To execute the pipeline, click on **Add trigger** menu and then **Trigger Now**.
1.	On the **Pipeline Run** blade, click **Finish**.

    ![](./Media/Lab2-Image34.png)

1.	To monitor the execution of your pipeline, click on the **Monitor** menu on the left-hand side panel.
1.	You should be able to see the Status of your pipeline execution on the right-hand side panel.

    ![](./Media/Lab2-Image35.png)

1.	Click the **View Activity Runs** button for detailed information about each activity execution in the pipeline. The whole execution should last between 7-8 minutes.

    ![](./Media/Lab2-Image36.png)
    ![](./Media/Lab2-Image37.png)


## Task: Visualize Data Using Power BI    
 1.  If you have not done so, [download Power BI Desktop]
   <br> [Download sample report](media/PowerBi/MDWDataVisualization.pbit)
   <br> Open the downloaded report

	 ![](media/powerbi/open-report.png)

   <br> - **Server name**: edumdwsqlserver+YOURINITIALS.database.windows.net

![](media/powerbi/enter-server-name.png)

   <br> - **Authentication**: Database
   <br> - **User Name**: EduMdwAdmin
   <br> - **Password**: P@$$word123

![](media/powerbi/enter-credentials.png)

    This the report view

   ![](media/powerbi/report-view.png)
       
    

## Next task: [Stream Cognitive Services Real Time](../azure-logic-app/steam-ai-tweeter.md)

[download Power BI Desktop]:https://www.microsoft.com/en-us/download/details.aspx?id=45331

