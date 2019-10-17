# Create a Logic App to run prediction model and write to SQL
Azure Logic Apps simplifies how you build automated scalable workflows that integrate apps and data across cloud services and on-premises systems. Learn how to create, design, and deploy logic apps that automate business processes with our quickstarts, tutorials, templates, and APIs. 

We'll use a Logic App to receive IoT data from the Service Bus Queue, analyize it with the Custom Vision API, and finally write the data into the Azure SQL Database table.

1. In the Azure portal, click the **New** button found on the upper left-hand corner of the Azure portal.

1. Click **Web** > **Logic App**, then click **Create**.
 
1. In the Logic App create from, use the suggested values, then click **Create**.

    | Field | Suggested Value  |
    |------|------|
    |Name |```arctic-vision-app```|
    |Subscription |Your Azure subscription.|
    |Resource Group |**Use Existing** and the resource group you're using for this lab.|
    |Location |Use the location nearest you.|

    ![Create Logic App](media/loigc-app-1.png)

1. After the app is created, click your new logic app pinned to the dashboard. Then in the Logic Apps Designer, scroll down and click the **Blank Logic App** template. 

    ![Blank Logic Apps template](media/09-logic-app-create-blank.png)

    You can now use the Logic Apps Designer to add services and triggers to your app.

## Connect to the Azure Service Bus Queue

1. From the *Logic App Designer*, search for *Azure Service Bus* and then choose **When a message is received in a queue (auto-complete)**.

    ![Create Logic App](media/loigc-app-2.png)

1. Give your connection a memorable name, this will be an API connection that you may use at any point in the future for other projects. Then, select the *Service Bus Namespace* you created earlier for this project. 

    ![Create Logic App](media/loigc-app-3.png)

1. Next, select the *RootManageSharedAccessKey* policy and click **Create**.

    ![Create Logic App](media/loigc-app-4.png)

1. Select your queue name from the drop down and change the *Interval* to 20 and the *Frequency* to Second. Then, click **+ New Step**.

    ![Create Logic App](media/loigc-app-5.png)

1. From *Choose an action*, type in *Variable* in the search field and select **Initialize variable**.

    ![Create Logic App](media/loigc-app-6.png)

1. Set the values for the action to the suggested values.

    | Setting      |  Suggested value   | Description                                        |
    | ----------------- | ------------ | ------------- |
    | **Name** | message-content | The variable name to reference later. |
    | **Type** | String | The variable type. |

1. Click in the *Value* field and choose **Content** from the dynamic content pop-up.

   ![Create Logic App](media/loigc-app-7.png)

1. Click **New Step**, search for *Parse JSON* and select the **Parse JSON** action.

   ![Create Logic App](media/loigc-app-8.png)

1. Click in the *Content* text-field, and choose **message-content** from the dynamic content pop-up.

   ![Create Logic App](media/loigc-app-9.png)

1. Copy the following code and paste it into the *Schema* text area, then click **New step**.

    ```json
    {
        "properties": {
            "deviceId": {
                "type": "string"
            },
            "latitude": {
                "type": "number"
            },
            "longitude": {
                "type": "number"
            },
            "timestamp": {
                "type": "string"
            },
            "url": {
                "type": "string"
            }
        },
        "type": "object"
    }
    ```
1. From *Choose an action*, type in *Custom Vision* in the search field and select **Classify an image url (preview)**.

   ![Create Logic App](media/loigc-app-10.png)

1. Set the values for the action to the suggested values, then click **Create**.

    | Setting      |  Suggested value   | Description                                        |
    | ----------------- | ------------ | ------------- |
    | **Name** | ```arctic-vision-connector``` | The name of your API connection. |
    | **Prediction Key** | ```prediction_key_value``` | The value of your custom vision prediction key |
    | **Site URL** | ```site_url``` | The value of your custom vision prediction URL |

   ![Create Logic App](media/loigc-app-11.png)

1. Set the values for the action to the suggested values. 

    | Setting      |  Suggested value   | Description                                        |
    | ----------------- | ------------ | ------------- |
    | **Project ID** | ```project_id``` | The Project ID value that was pasted into a text editor |
    | **Published Name** | ```published_name``` | The Published Name of the model iteration we will be using. |


1. Click inside the *Image URL* field and select the **url** value from *parse JSON* in the dynamic content menu, then click **+ New step**

   ![Create Logic App](media/loigc-app-15.png)

1. In *Choose an action*, search for Variable and select **Initialize Variable**. 

   ![Create Logic App](media/loigc-app-16.png)

1. Set the values to the suggested values below, then click **+ New step**.

    | Setting      |  Suggested value   | Description                                        |
    | ----------------- | ------------ | ------------- |
    | **Name** | Prediction | The variable name to reference later. |
    | **Type** | String | The variable type. |


1. Repeat the last two steps to create another variable with the following suggested values, then click **+ New step**

    | Setting      |  Suggested value   | Description                                        |
    | ----------------- | ------------ | ------------- |
    | **Name** | isPolarBear | The variable name to reference later. |
    | **Type** | Boolean | The variable type. |
    | **Value** | false | Default to not a polar bear. |

    ![Create Logic App](media/loigc-app-17.png)

1. In *Choose an action*, search for *for each* and select **For each**. 

   ![Create Logic App](media/loigc-app-18.png)

1. Click the *Select an output from previous steps* field within the *For each* action, and choose **Predictions** from the Dynamic Content menu.

  ![Create Logic App](media/loigc-app-19.png)

1. Within the *For each* action, click **Add an action**.

  ![Create Logic App](media/loigc-app-20.png)

1. Choose *Parse JSON* as the action, select **Current Item** from the Dynamic Content Menu for the *Content* and paste the following in the *Schema* box. Then click **Add Action**

    ```json
        {
            "properties": {
                "probability": {
                    "type": "number"
                },
                "tagId": {
                    "type": "string"
                },
                "tagName": {
                    "type": "string"
                }
            },
            "type": "object"
        }
    ```

    ![Create Logic App](media/loigc-app-21.png)

1. In *Choose an action*, search for *condition* and select **Condition**. 

    ![Create Logic App](media/loigc-app-22.png)

1. Under *Condition* click on *Choose a value*, select **tagName** from the Dynamic Content menu. Set the condition to **is equal to** and the condition value to *Polar bear*. Next, click **+ Add** and **Add Row** to add another condition. 

    ![Create Logic App](media/loigc-app-23.png)

1. Under *Condition* click on *Choose a value*, select **probability** from the Dynamic Content menu. Set the condition to **is greater than or equal to** and the condition value to *0.8*.

    ![Create Logic App](media/loigc-app-24.png)

1. Under *If True*, click **Add an action** and select *Set Variable*. Under *Name*, choose **isPolarBear** and set the *Value* to **1**. Then, click **+ New Step** *outside of the condition and for each loop.*

    ![Create Logic App](media/loigc-app-27.png)

1. Search for **SQL** in the *Choose an action* box. Select **SQL Server**, and then select **Insert row (V2)**

    ![Create Logic App](media/loigc-app-28.png)

1. Set a memorable connection name for your SQL Server, choose the SQL server and database you created previously, enter the appropriate username and password and finally click **Create**.

    ![Create Logic App](media/loigc-app-29.png)

1. Select the **Use Connectiong String** option for both *Server Name* and *Database Name*, and set the *Table* value to equal **PolarBears.**. 

1. Click **Add new parameter** to add the ```ID```, ```CameraId```, ```Latitude```, ```Longitude```, ```Url```, ```Timestamp```, and ```IsPolarBear``` fields.

    ![Create Logic App](media/loigc-app-30.png)

1. Add the corresponding value from the *Parse JSON* dynamic content into their respective fields and click **Save.**


### Next unit: [Visualize the camera activity with Power BI](visualize-with-power-bi.md)
