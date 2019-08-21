# Create a Logic App
Azure Logic Apps simplifies how you build automated scalable workflows that integrate apps and data across cloud services and on-premises systems. Learn how to create, design, and deploy logic apps that automate business processes with our quickstarts, tutorials, templates, and APIs.  
**Documentation: https://docs.microsoft.com/en-us/azure/logic-apps/**
### Prerequisite: [Create a Function App](./create-a-function-app.md) ###

## Tasks
- [Create the Logic App](#Create-the-Logic-App)
- [Connect to Twitter](#Connect-to-Twitter)
- [Initialize and Set Variables](#Initialize-and-Set-Variables)
- [Detect Tweet Language](#Detect-Tweet-Language)
- [Detect Sentiment](#Detect-Sentiment)
- [Connect sentiment output to your function](#Connect-sentiment-output-to-your-function)
- [Translate Tweet](#Translate-Tweet)
- [Email Tweet](#Email-Tweet)
- [Create a Profanity Filter](#Create-a-Profanity-Filter)
- [Retweet](#Retweet)


## Create the Logic App

1. In the Azure portal, click the **New** button found on the upper left-hand corner of the Azure portal.

2. Click **Web** > **Logic App**.
 
3. Then, type a value for **Name** like `TweetSentiment`, and use the settings as specified in the table.

    ![Create logic app in the Azure portal](./media/08-logic-app-create.png)

    | Setting      |  Suggested value   | Description                                        |
    | ----------------- | ------------ | ------------- |
    | **Name** | TweetSentiment | Choose an appropriate name for your app. |
    | **Resource group** | ServerlessWkrshp | Choose the same existing resource group as before. |
    | **Location** | South Central US | Choose a location close to you. |    

4. Once you have entered the proper settings values, click **Create** to create your logic app. 

5. After the app is created, click your new logic app pinned to the dashboard. Then in the Logic Apps Designer, scroll down and click the **Blank Logic App** template. 

    ![Blank Logic Apps template](media/09-logic-app-create-blank.png)

You can now use the Logic Apps Designer to add services and triggers to your app.

## Connect to Twitter
#### [Back to top](#Tasks)
First, create a connection to your Twitter account. The logic app polls for tweets, which trigger the app to run.

1. In the designer, click the **Twitter** service, and click the **When a new tweet is posted** trigger. Sign in to your Twitter account and authorize Logic Apps to use your account.

2. Use the Twitter trigger settings as specified in the table. 

    ![Twitter connector settings](media/10-tweet-settings.png)

    | Setting      |  Suggested value   | Description                                        |
    | ----------------- | ------------ | ------------- |
    | **Search text** | #Azure | Use a hashtag that is popular enough to generate new tweets in the chosen interval. When using the Free tier and your hashtag is too popular, you can quickly use up the transaction quota in your Cognitive Services API. |
    | **Interval** | 15 | The time elapsed between Twitter requests, in frequency units. |
    | **Frequency** | Minute | The frequency unit used for polling Twitter.  |

3.  Click  **Save** to connect to your Twitter account. 

Now your app is connected to Twitter. Next, you connect to text analytics to detect the sentiment of collected tweets.

## Initialize and Set Variables
#### [Back to top](#Tasks)

1. Click **New Step**, and then **Add an action**.

1. In **Choose an action**, type **Variables**, and then click the **Initialize Variable** action.

    ![Create an Initiate Variable action](media/variables-1.png)

1. Set the **Name** and **Type** to the appropriate values, leave **Value** blank
    
    | Setting      |  Suggested value   | Description                                        |
    | ----------------- | ------------ | ------------- |
    | **Name** | tweet_text | The variable name to reference later. |
    | **Type** | String | The variable Type. |

1. Click **New Step**, and then **Add an action**.

1. In **Choose an action**, type **Variables**, and then click the **Initialize Variable** action.

    ![Create an Initiate Variable action](media/variables-1.png)

1. Set the **Name** and **Type** to the appropriate values, leave **Value** blank

    | Setting      |  Suggested value   | Description                                        |
    | ----------------- | ------------ | ------------- |
    | **Name** | translated_text | The variable name to reference later. |
    | **Type** | String | The variable Type. |

    ![Set Initiate Variable values](media/variables-3.png)

1. Click **New Step**, and then **Add an action**.

1. In **Choose an action**, type **Variables**, and then click the **Set Variable** action.

    ![Create a Set Variable action](media/variables-4.png)

1. Set the **Name** to *tweet_text*, Click on the **Value** textfield and choose *Tweet Text* rom the **Dynamic Content** pop-up.
    
    ![Set Variable value](media/variables-2.png)

## Detect Tweet Language
#### [Back to top](#Tasks)

1. Click **New Step**, and then **Add an action**.

1. In **Choose an action**, type **Translator**, and then click the **Detect Langauge** action.
   
    ![Create a Detect Language action](media/detect-language-1.png)

1. Enter *TwtrTranslator* for **Connection Name** and the value of **Key1** of your translator resource, then hit **Create** to create the API connection to your resource.
 
    ![Create a Translator connection](media/detect-language-2.png)

1. Click inside of the **Text** field, and choose **tweet_text** from the **Dynamic Content** pop-up.

    ![Set Text field value](media/detect-language-3.png)

## Detect Sentiment
#### [Back to top](#Tasks)

1. Click **New Step**, and then **Add an action**.

1. In **Choose an action**, type **Text Analytics**, and then click the **Detect sentiment (preview)** action.

    ![Create and Sentiment action](media/11-detect-sentiment.png)

1. Type a connection name such as `TwtrSentiment`, paste the key for your Cognitive Services API and the Cognitive Services endpoint you set aside in a text editor, and click **Create**.

    ![Add Sentiment connection](media/12-connection-settings.png)

1. Click in the **Add new parameter** field and choose *Text*
    
    ![Add Text parameter](media/sentiment-4.png)

1. Click in the **Text** field and choose *tweet_text* from the  **Dynamic Content** pop-up.

    ![Select tweet_text from the Dynamic Content pop-up](media/sentiment-5.png)

1. Click in the **Add new parameter** field and choose *Language*

    ![Select Language Parameter](media/sentiment-6.png)

1. Click in the **Language** field, scroll down and choose *Enter custom value*.

    ![Choose Enter Custom Value for Language](media/sentiment-7.png)

1. Next, choose **Language Code** from the **Dynamic Content** pop-up and enter **tweet_text** in the text box and then click on **New Step**.

    ![Add Language Code and Text Value](media/sentiment-8.png)


Now that sentiment detection is configured, you can add a connection to your function that consumes the sentiment score output.

## Connect sentiment output to your function
#### [Back to top](#Tasks)
1. In the Logic Apps Designer, click **New step** > **Add an action**, filter on **Azure Functions** and click **Choose an Azure function**.

    ![Detect Sentiment](media/14-azure-functions.png)
  
4. Select the function app you created earlier.

    ![Select function](media/15-select-function.png)

5. Select the function you created for this tutorial.

    ![Select function](media/16-select-function.png)

4. In **Request Body**, click **Score** and then **Save**.

    ![Score](media/17-function-input-score.png)

Now, your function is triggered when a sentiment score is sent from the logic app. A color-coded category is returned to the logic app by the function. Next, you add an email notification that is sent when a sentiment value of **RED** is returned from the function. 


## Translate tweet
#### [Back to top](#Tasks)
The last part of the workflow is to trigger an email when the sentiment is scored as _RED_ and post a tweet when the sentiment is scored as _GREEN_. This topic uses an Outlook.com connector. You can perform similar steps to use a Gmail or Office 365 Outlook connector. We will also translate the tweet to English when emailing and add a profanity filter when tweeting. 

1. In the Logic Apps Designer, click **New step** > **Add a condition**. 

    ![Add Condition action](media/18-add-condition.png)

2. Click **Choose a value**, then click **Body**. Select **is equal to**, click **Choose a value** and type `RED`, and click **Save**. 

    ![Set condition value](media/19-condition-settings.png)    

1. In **IF TRUE**, click **Add an action**, search for `Condition` and click on the **Condition** action.

    ![Add Condition action](media/condition-1.png)    

1. In the **Choose a value** field, select **Language Code** from the **Dynamic Content** pop-up. 

    ![Set language](media/translate-1.png)

1. In nested **IF TRUE**, click **Add an action**, search for `Translate` and click on the **Translate text (preview)** action. 

    ![Add Translate Action if True](media/translate-2.png)

1. Set the **Target Language** to *English* and the **Text** field to *tweet_text* from the **Dynamic Content** pop-up.

    ![Set Language and Text value](media/translate-3.png)

1.  Click **Add an action**, search for `Variables` and click on the **Set Variable** action.

    ![Add Set Variable acction](media/translate-4.png)

1. Set the **Name** to *translated_tweet*, Click on the **Value** textfield and choose *Translated Text* rom the **Dynamic Content** pop-up.

    ![Set name and value of translated_tweet variable](media/translate-5.png)

## Email Tweet
#### [Back to top](#Tasks)

1. Click the **Add an action** in the _PARENT_ conditional loop

    ![Add an action after the child conditions](media/condition-2.png)

1. Search for *Outlook* and click on **Office 365 Outlook**, then **Send an Email** 
    
    ![Create and Outlook Send Email action](media/email-1.png)

1. Click on **Sign In** and proceed to sign in to create a connector using your Work/School account.

1. Add yourself to the **To** line

1. Set the **Subject** to *Negative #hookem  Tweet by:* followed by the *Tweeted by* value from the **Dynhamic Content** pop-up.
   
   ![Set the To and Subject of email.](media/email-2.png)

1. In the **Body**, add the following content from the **Dynamic Content** pop-up:

    | Content      |  Source   |
    | ----------------- | ------------ |
    | **translated_tweet** | Variables |
    | **tweet_text** | Variables  |
    | **Language Name** | Detect Language |
    | **Location** | When a new tweet is posted |
    | **Created at** | When a new tweet is posted |
    
    *You copy and paste the code below into the Body to append labels in front of variables*  
    ```ruby
        @{variables('translated_tweet')}
        Tweet: @{variables('tweet_text')}
        Language: @{body('Detect_language')?['Name']}
        Location: @{triggerBody()?['UserDetails']?['Location']}
        Date: @{triggerBody()?['CreatedAtIso']} 
    ```


    ![Fill in Body of email](media/email-3.png)

## Create a Profanity Filter
#### [Back to top](#Tasks)

1. In the parent **IF FALSE** condition, click **Add an action**

    ![Add False path](media/retweet-1.png)

1. Search for *profanity* and choose **Detect profanity and match against custom and shared blacklists (preview)**

    ![Create Profanity Filter](media/retweet-2.png)

1. Enter in the appropriate resource values
Now that the workflow is complete, you can enable the logic app and see the function at work, then click **Create**.

    | Name      |  Description   |
    | ----------------- | ------------ |
    | **Connection Name** | The Name of your Content Moderator resource |
    | **API Key** | Your Content Moderator API Key  |
    | **Site URL** | The regional cognitive services URL (*South Cental US:* https://southcentralus.api.cognitive.microsoft.com, *West US:* https://westus.api.cognitive.microsoft.com) |

    ![Set Content Moderator parameters](media/retweet-3.png)  

1. Select *text/plain* for **Content Type**, click in the **Text Content** field and choose **See more** under **Variables** in the **Dynamic Content** pop-up.  
    
    ![Set Content Type and Text Content](media/retweet-4.png)  

1. Next, choose *tweet_text* from the **Dynamic Content** pop-up.
    
    ![Choose tweet_text](media/retweet-5.png)  

1. Click **Add an action** under the profanity detector action, search for "condition" and choose the "Condition" action.
    
    ![Add Condition Action](media/retweet-6.png)  

1. In the **Condition** action, click in the **Chose a value** field and choose **Detected Profanity Terms** from the **Dynamic Content** pop-up.
    
    ![Choose Detect Profanity Terms](media/retweet-7.png)  

1. Set the **Choose a value** field to *null*
    
    ![Set value to null](media/retweet-8.png)


## Retweet
#### [Back to top](#Tasks)
1. In the child **IF TRUE** condition, select **Add an action**, search for **Twitter** and select **Post a Tweet**

    ![Select Post a Tweet](media/retweet-9.png) 

1. Click on the **Search or filter parameters...** field and choose **Tweet text**

    ![Add Tweet Text content](media/retweet-10.png) 

1. Click in the **Tweet Text** field and choose **tweet_text** from the **Dynamic Content** pop-up.

    ![Set Tweet Text value](media/retweet-11.png) 

1. Click **Save**.

<br>

### Next: [Test the Workflow](./test-the-workflow.md) ###
#### Previous: [Create a Function App](./create-a-function-app.md) ####