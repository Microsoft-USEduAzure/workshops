
# Introduction to Azure Machine Learning Workshop
## AutoML Classification

In this workshop we will use the [Azure Machine Learning SDK for Python](https://docs.microsoft.com/en-us/python/api/overview/azure/ml/intro?view=azure-ml-py) as well as Automated Machine Learning services from Azure to predict and flag students who are at risk of dropping a course. 

### Prerequisites
 - Access to an Azure Subscription
 - Completion of the [Azure Fundamentals Lab](https://aka.ms/edu/Azure101)
 
 
### Task List
- [Create an Azure Machine Learning Services Workspace](#Create-an-Azure-Machine-Learning-Services-Workspace)
- [Connect to the Azure Machine Learning Services Workspace](#Connect-to-the-Azure-Machine-Learning-Services-Workspace)
--[Load data from Azure Blob Storage](#Load-data-from-Azure-Blob-Storage)
- [Transform and prepare data](#Transform-and-prepare-data)
- [Configure AutoML Experiment](#Configure-AutoML-Experiment)
- [Run AutoML Experiment & Explore Results](#Run-AutoML-Experiment-and-Explore-Results)
- [Retreive and Test AutoML model](#Retreive-and-Test-AutoML-model)
- [Apply AutoML model to whole dataset](#Apply-AutoML-model-to-whole-dataset)


## Create an Azure Machine Learning Services Workspace
[Link to Top](#Task-List)  
1. Sign in to the [Azure portal](https://portal.azure.com/).  
1. Click **Create a resource** in the upper left-hand corner of the Azure portal.  
1. In the Search field, type in *Machine Learning* and choose **Machine Learning Service Workspace**
![Create Machine Learning Workspace](media/1.png)  
1. Click on the **Create** button and fill out the Resource Creation form accordingly, then click **Review + Create**, followed by **Create**.  
![Create Machine Learning Workspace](media/2.png)

| Setting | Suggested value   |
|------|------|
|**Wokrspace Name**  | Your Workspace Name|
|**Subscription**  | The subscription associated with your account.|
|**Resource group**  | Create a new resoruce group for this workshop.|
|**Location**  | Use the location nearest you.|

5. Go to the Workspace once it has been deployed and retrieve the:  
    - Subscription ID
    - Resource Group Name
    - Workspace Name
![Retrieve workspace settings](media/3.png)
1. From the same page, click on the **Storage** hyperlink.  
1. From the Storage Account Overview, click on **Access Keys**, and retrieve **key2**  
![Retrieve workspace settings](media/4.png)

## Connect to the Azure Machine Learning Services Workspace
[Link to Top](#Task-List)  

For Azure AutoML, you will need to create an **Experiment**, which is a named object in your Machine Learning Workspace used to run experiments.  

1. First, we will import the Azure Machine Learning Service and Logging libraries and validate SDK version.  


```python
import azureml.core
import logging
```


```python
print("You are currently using version", azureml.core.VERSION, "of the Azure ML SDK")
```

    You are currently using version 1.0.60 of the Azure ML SDK


2. Next, we define variables with the values we retrived for our **Subscription ID**, **Resource Group Name**, and **Machine Learning Workspace Name.** 


```python
subscription_id = '<Your Subscription GUID>'
resource_group  = '<Your Resource Group Name>'
workspace_name  = '<Your Workspace Name>'
```

3. We will now use those variables to ensure that the Machine Learning Workspace exists, and we are able to connect to it.

    If the result of the following cell is 'Workspace not found,' review the variables we defined in the previous step to ensure they match the values from [Create an Azure Machine Learning Services Workspace](#Create-an-Azure-Machine-Learning-Services-Workspace).


```python
from azureml.core import Workspace
try:
    ws = Workspace(subscription_id = subscription_id, resource_group = resource_group, workspace_name = workspace_name)
    ws.write_config()
    print('Library configuration succeeded')
except:
    print('Workspace not found')
```

    Library configuration succeeded


## Load data from Azure Blob Storage
[Link to Top](#Task-List)  

1. Now we will connect to Azure Blob Storage. To do so, we will first import the Azure Storage library.


```python
from azure.storage.blob import BlockBlobService
#import tables
```

2. Next, we will define variables that correspond to our storage account, and the location of the data we'll be creating the model from.


```python
storageaccount_name = '<Your Storage Account Name>'
storageaccount_key = '<Your Storage Account Key>'

local_filename = './MLInput.tsv'
container_name = 'sources'
blob_name = 'MLInput.tsv'
```

3. We'll use the variables from step 2 to create a connection to Blob storage and retrive the data.


```python
#download from blob

blob_service=BlockBlobService(account_name=storageaccount_name,account_key=storageaccount_key)
blob_service.get_blob_to_path(container_name,blob_name,local_filename)

```




    <azure.storage.blob.models.Blob at 0x7fbe28f2bc18>



4. Now we'll load the blob data into a [Pandas dataframe](https://pandas.pydata.org/pandas-docs/stable/reference/api/pandas.DataFrame.html). A Data frame is a two-dimensional data structure, i.e., data is aligned in a tabular fashion in rows and columns. Pandas DataFrame consists of three principal components, the data, rows, and columns.


```python
import pandas as pd

studentDropDF = pd.read_csv(local_filename, sep='\t', index_col=0)

studentDropDF = studentDropDF.dropna(axis=1,how='any')

```

5. Now that the data is stored in a Data Frame, we can display it to explore the data and gain context.


```python
display(studentDropDF)
```


<div>
<table border="1" class="dataframe">
  <thead>
    <tr style="text-align: right;">
      <th></th>
      <th>TermID</th>
      <th>SubjectID</th>
      <th>CatalogID</th>
      <th>ClassID</th>
      <th>SectionID</th>
      <th>EnrollDate</th>
      <th>Gender</th>
      <th>Age</th>
      <th>City</th>
      <th>StateAbbrev</th>
      <th>...</th>
      <th>AdmitTerm</th>
      <th>Term</th>
      <th>SchoolYear</th>
      <th>Subject</th>
      <th>Catalog</th>
      <th>Section</th>
      <th>Class</th>
      <th>CreditHours</th>
      <th>Dropped</th>
      <th>MidTermGrade</th>
    </tr>
    <tr>
      <th>StudentID</th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>18023</th>
      <td>2074</td>
      <td>ACC-UB</td>
      <td>301</td>
      <td>1</td>
      <td>UB2</td>
      <td>20061127</td>
      <td>FEMALE</td>
      <td>28</td>
      <td>Camden</td>
      <td>NJ</td>
      <td>...</td>
      <td>2048</td>
      <td>Spring</td>
      <td>2007</td>
      <td>ACC-UB</td>
      <td>301</td>
      <td>UB2</td>
      <td>Intermediate Accounting I</td>
      <td>3</td>
      <td>0</td>
      <td>-</td>
    </tr>
    <tr>
      <th>5041</th>
      <td>2074</td>
      <td>ACC-UB</td>
      <td>405</td>
      <td>2</td>
      <td>101</td>
      <td>20070117</td>
      <td>MALE</td>
      <td>26</td>
      <td>Rockville</td>
      <td>MD</td>
      <td>...</td>
      <td>2068</td>
      <td>Spring</td>
      <td>2007</td>
      <td>ACC-UB</td>
      <td>405</td>
      <td>101</td>
      <td>Income Taxation</td>
      <td>3</td>
      <td>0</td>
      <td>-</td>
    </tr>
    <tr>
      <th>657</th>
      <td>2054</td>
      <td>ADLT</td>
      <td>473</td>
      <td>3</td>
      <td>1</td>
      <td>20041219</td>
      <td>MALE</td>
      <td>27</td>
      <td>Randallstown</td>
      <td>MD</td>
      <td>...</td>
      <td>2048</td>
      <td>Spring</td>
      <td>2005</td>
      <td>ADLT</td>
      <td>473</td>
      <td>1</td>
      <td>PRC AD HEA&amp;DEV PR</td>
      <td>3</td>
      <td>0</td>
      <td>S</td>
    </tr>
    <tr>
      <th>390</th>
      <td>2064</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20060130</td>
      <td>FEMALE</td>
      <td>34</td>
      <td>Baltimore</td>
      <td>MD</td>
      <td>...</td>
      <td>2048</td>
      <td>Spring</td>
      <td>2006</td>
      <td>ANTH</td>
      <td>207</td>
      <td>1</td>
      <td>CULTURAL ANTH</td>
      <td>3</td>
      <td>1</td>
      <td>-</td>
    </tr>
    <tr>
      <th>700</th>
      <td>2064</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20051208</td>
      <td>MALE</td>
      <td>30</td>
      <td>Baltimore</td>
      <td>MD</td>
      <td>...</td>
      <td>2048</td>
      <td>Spring</td>
      <td>2006</td>
      <td>ANTH</td>
      <td>207</td>
      <td>1</td>
      <td>CULTURAL ANTH</td>
      <td>3</td>
      <td>0</td>
      <td>D</td>
    </tr>
    <tr>
      <th>793</th>
      <td>2074</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20070125</td>
      <td>MALE</td>
      <td>48</td>
      <td>Baltimore</td>
      <td>MD</td>
      <td>...</td>
      <td>2054</td>
      <td>Spring</td>
      <td>2007</td>
      <td>ANTH</td>
      <td>207</td>
      <td>1</td>
      <td>CULTURAL ANTH</td>
      <td>3</td>
      <td>0</td>
      <td>S</td>
    </tr>
    <tr>
      <th>830</th>
      <td>2074</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20070209</td>
      <td>FEMALE</td>
      <td>41</td>
      <td>Baltimore</td>
      <td>MD</td>
      <td>...</td>
      <td>2058</td>
      <td>Spring</td>
      <td>2007</td>
      <td>ANTH</td>
      <td>207</td>
      <td>1</td>
      <td>CULTURAL ANTH</td>
      <td>3</td>
      <td>0</td>
      <td>D</td>
    </tr>
    <tr>
      <th>947</th>
      <td>2074</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20070117</td>
      <td>FEMALE</td>
      <td>64</td>
      <td>Baltimore</td>
      <td>MD</td>
      <td>...</td>
      <td>2058</td>
      <td>Spring</td>
      <td>2007</td>
      <td>ANTH</td>
      <td>207</td>
      <td>1</td>
      <td>CULTURAL ANTH</td>
      <td>3</td>
      <td>0</td>
      <td>S</td>
    </tr>
    <tr>
      <th>962</th>
      <td>2058</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20050823</td>
      <td>FEMALE</td>
      <td>57</td>
      <td>Hanover</td>
      <td>PA</td>
      <td>...</td>
      <td>2064</td>
      <td>Fall</td>
      <td>2005</td>
      <td>ANTH</td>
      <td>207</td>
      <td>1</td>
      <td>CULTURAL ANTH</td>
      <td>3</td>
      <td>0</td>
      <td>D</td>
    </tr>
    <tr>
      <th>976</th>
      <td>2058</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20050826</td>
      <td>FEMALE</td>
      <td>31</td>
      <td>Parkville</td>
      <td>MD</td>
      <td>...</td>
      <td>2068</td>
      <td>Fall</td>
      <td>2005</td>
      <td>ANTH</td>
      <td>207</td>
      <td>1</td>
      <td>CULTURAL ANTH</td>
      <td>3</td>
      <td>0</td>
      <td>F</td>
    </tr>
    <tr>
      <th>999</th>
      <td>2058</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20050722</td>
      <td>FEMALE</td>
      <td>27</td>
      <td>Hampton</td>
      <td>VA</td>
      <td>...</td>
      <td>2058</td>
      <td>Fall</td>
      <td>2005</td>
      <td>ANTH</td>
      <td>207</td>
      <td>1</td>
      <td>CULTURAL ANTH</td>
      <td>3</td>
      <td>0</td>
      <td>S</td>
    </tr>
    <tr>
      <th>1170</th>
      <td>2074</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20061201</td>
      <td>FEMALE</td>
      <td>24</td>
      <td>Laurel</td>
      <td>MD</td>
      <td>...</td>
      <td>2078</td>
      <td>Spring</td>
      <td>2007</td>
      <td>ANTH</td>
      <td>207</td>
      <td>1</td>
      <td>CULTURAL ANTH</td>
      <td>3</td>
      <td>0</td>
      <td>S</td>
    </tr>
    <tr>
      <th>1617</th>
      <td>2074</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20070112</td>
      <td>FEMALE</td>
      <td>40</td>
      <td>Randallstown</td>
      <td>MD</td>
      <td>...</td>
      <td>2064</td>
      <td>Spring</td>
      <td>2007</td>
      <td>ANTH</td>
      <td>207</td>
      <td>1</td>
      <td>CULTURAL ANTH</td>
      <td>3</td>
      <td>0</td>
      <td>S</td>
    </tr>
    <tr>
      <th>1937</th>
      <td>2074</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20070116</td>
      <td>FEMALE</td>
      <td>27</td>
      <td>Frederick</td>
      <td>MD</td>
      <td>...</td>
      <td>2048</td>
      <td>Spring</td>
      <td>2007</td>
      <td>ANTH</td>
      <td>207</td>
      <td>1</td>
      <td>CULTURAL ANTH</td>
      <td>3</td>
      <td>0</td>
      <td>S</td>
    </tr>
    <tr>
      <th>2286</th>
      <td>2064</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20060123</td>
      <td>INVALID SEX</td>
      <td>26</td>
      <td>Columbus</td>
      <td>OH</td>
      <td>...</td>
      <td>2058</td>
      <td>Spring</td>
      <td>2006</td>
      <td>ANTH</td>
      <td>207</td>
      <td>1</td>
      <td>CULTURAL ANTH</td>
      <td>3</td>
      <td>0</td>
      <td>D</td>
    </tr>
    <tr>
      <th>2342</th>
      <td>2068</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20060414</td>
      <td>MALE</td>
      <td>25</td>
      <td>Upper Marlboro</td>
      <td>MD</td>
      <td>...</td>
      <td>2068</td>
      <td>Fall</td>
      <td>2006</td>
      <td>ANTH</td>
      <td>207</td>
      <td>1</td>
      <td>CULTURAL ANTH</td>
      <td>3</td>
      <td>0</td>
      <td>F</td>
    </tr>
    <tr>
      <th>2419</th>
      <td>2074</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20070119</td>
      <td>FEMALE</td>
      <td>24</td>
      <td>Baltimore</td>
      <td>MD</td>
      <td>...</td>
      <td>2078</td>
      <td>Spring</td>
      <td>2007</td>
      <td>ANTH</td>
      <td>207</td>
      <td>1</td>
      <td>CULTURAL ANTH</td>
      <td>3</td>
      <td>0</td>
      <td>S</td>
    </tr>
    <tr>
      <th>2460</th>
      <td>2068</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20060821</td>
      <td>FEMALE</td>
      <td>41</td>
      <td>Laurel</td>
      <td>MD</td>
      <td>...</td>
      <td>2078</td>
      <td>Fall</td>
      <td>2006</td>
      <td>ANTH</td>
      <td>207</td>
      <td>1</td>
      <td>CULTURAL ANTH</td>
      <td>3</td>
      <td>0</td>
      <td>S</td>
    </tr>
    <tr>
      <th>2640</th>
      <td>2058</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20050628</td>
      <td>MALE</td>
      <td>28</td>
      <td>San Francisco</td>
      <td>CA</td>
      <td>...</td>
      <td>2048</td>
      <td>Fall</td>
      <td>2005</td>
      <td>ANTH</td>
      <td>207</td>
      <td>1</td>
      <td>CULTURAL ANTH</td>
      <td>3</td>
      <td>0</td>
      <td>S</td>
    </tr>
    <tr>
      <th>2648</th>
      <td>2048</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20040826</td>
      <td>MALE</td>
      <td>64</td>
      <td>Baltimore</td>
      <td>MD</td>
      <td>...</td>
      <td>2088</td>
      <td>Fall</td>
      <td>2004</td>
      <td>ANTH</td>
      <td>207</td>
      <td>1</td>
      <td>CULTURAL ANTH</td>
      <td>3</td>
      <td>0</td>
      <td>S</td>
    </tr>
    <tr>
      <th>2694</th>
      <td>2068</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20060818</td>
      <td>MALE</td>
      <td>34</td>
      <td>Baltimore</td>
      <td>MD</td>
      <td>...</td>
      <td>2048</td>
      <td>Fall</td>
      <td>2006</td>
      <td>ANTH</td>
      <td>207</td>
      <td>1</td>
      <td>CULTURAL ANTH</td>
      <td>3</td>
      <td>0</td>
      <td>D</td>
    </tr>
    <tr>
      <th>...</th>
      <td>...</td>
      <td>...</td>
      <td>...</td>
      <td>...</td>
      <td>...</td>
      <td>...</td>
      <td>...</td>
      <td>...</td>
      <td>...</td>
    </tr>
  </tbody>
</table>
<p>56106 rows × 21 columns</p>
</div>


## Transform and prepare data
[Link to Top](#Task-List)  

1. First, we will split the data into two data frames. One for indepedent variables (studentDropDF_X) and one for the depedent variable (studentDropDF_y.)

    For our analysis the depdent variable will be **Dropped**, where 1 indicates a course was dropped and 0 indicates the opposite.


```python
studentDropDF_x = studentDropDF.drop("Dropped", axis=1)
studentDropDF_y = studentDropDF.filter(["Dropped"], axis=1)
```

2. Now we will import train_test_split from the sklearn library so that we may segment the dataframes further into two sets of data:

    - **x_train** and **y_train**: Will have 70% of all the data and be used to train our model.
    - **x_test** and **y_test**: Will have 30% of all the data and be used to validate the result of our training.


```python
from sklearn.model_selection import train_test_split

x_train, x_test, y_train, y_test = train_test_split(studentDropDF_x,studentDropDF_y, test_size=0.3, random_state=1234)
```


```python
## Remove comment to view dataframes
#display(x_train)
#display(x_test)
#display(y_train)
#display(y_test)
```

## Configure AutoML Experiment
[Link to Top](#Task-List)  

Begin to configure the AutoML by instantiating an AutoMLConfig object to define the settings and data used to run the expirement ([documentation link](https://docs.microsoft.com/en-us/azure/machine-learning/service/how-to-configure-auto-train#configure-your-experiment-settings)). 

1. We will first define the Experiment Name, which will correspond to generated expirement in your Machine Learning Workspace. 



```python
experiment_name = 'automated-ml-classification'
project_folder = './automated-ml-classification'
```

2. Next, import AutoMLConfig from the AzureML Library, define our automl_settings, and instantiate the AutoMLConfig object.



| Setting | Description   |
|------|------|
|**task**  | classification or regression|
|**primary_metric**  | Determines the metric to be used during model training for optimization.|
|**iterations**  | Number of iterations. In each iteration AutoML trains a specific pipeline with the data.|
|**iteration_timeout_minutes**  | Define a time limit in minutes per each iteration.|
|[**preprocess**](https://docs.microsoft.com/en-us/azure/machine-learning/service/concept-automated-ml#preprocess) | Advanced preprocessing and featurization (e.g. missing value imputation, categorization)|
|**X** | Indepdent variable array |
|**Y** | Depdent variable array|


```python
from azureml.train.automl import AutoMLConfig


automl_settings = {
  "iteration_timeout_minutes" : 5,
  "iterations" : 5,
  "primary_metric" : 'accuracy',
  "preprocess" : True,
  "verbosity" : logging.INFO,
  "n_cross_validations": 5
}

automl_config = AutoMLConfig(
                             task = 'classification',
                             debug_log = 'automated_ml_errors.log',
                             path = project_folder,
                             X = x_train,
                             y = y_train,
                             **automl_settings)
```

## Run AutoML Experiment and Explore Results
[Link to Top](#Task-List)  

1. Now we will instantiate an Expirement object using the AutoMLConfig object we created, and submit the expirement to **run** in Azure. 


```python
from azureml.core.experiment import Experiment

ws = Workspace.from_config()
experiment = Experiment(ws, experiment_name)
```

2. Depending on compute, complexity, and the number of iterations, the expirement may take some time to run; however, we will be able to monitor the progress of the run by turning **show_output** to equal **True**.


```python
run = experiment.submit(automl_config, show_output=True)
```

    Running on local machine
    Parent Run ID: AutoML_963d3fd5-3174-4926-8c06-cff4836d5fc0
    Current status: DatasetFeaturization. Beginning to featurize the dataset.
    Current status: DatasetEvaluation. Gathering dataset statistics.
    Current status: FeaturesGeneration. Generating features for the dataset.
    Current status: DatasetFeaturizationCompleted. Completed featurizing the dataset.
    Current status: DatasetCrossValidationSplit. Generating individually featurized CV splits.
    
    ****************************************************************************************************
    DATA GUARDRAILS SUMMARY:
    For more details, use API: run.get_guardrails()
    
    TYPE:         Missing Values Imputation
    STATUS:       FIXED
    DESCRIPTION:  The training data had the following missing values which were resolved.
    
    Please review your data source for data quality issues and possibly filter out the rows with these missing values.
    
    If the missing values are expected, you can either accept the above imputation, or implement your own custom imputation that may be more appropriate based on the data type and business process.
    
    
    TYPE:         High Cardinality Feature Detection
    STATUS:       FIXED
    DESCRIPTION:  High cardinality inputs were detected in dataset and were featurized as categorical_hash.
    
    ****************************************************************************************************
    Current status: ModelSelection. Beginning model selection.
    
    ****************************************************************************************************
    ITERATION: The iteration being evaluated.
    PIPELINE: A summary description of the pipeline being evaluated.
    DURATION: Time taken for the current iteration.
    METRIC: The result of computing score on the fitted pipeline.
    BEST: The best observed score thus far.
    ****************************************************************************************************
    
     ITERATION   PIPELINE                                       DURATION      METRIC      BEST
             0   MaxAbsScaler SGD                               0:00:14       0.9064    0.9064
             1   MaxAbsScaler SGD                               0:00:14       0.8440    0.9064
             2   MaxAbsScaler ExtremeRandomTrees                0:00:15       0.5813    0.9064
             3   VotingEnsemble                                 0:00:13       0.9064    0.9064
             4   StackEnsemble                                  0:00:16       0.9102    0.9102


3. Explore the result of the expirement by going to the [Azure portal](https://portal.azure.com), or by levarging the **RunDetails** widget.

    After completing the first iteration, an auto-updating graph and table will be shown. The widget will refresh once per minute, so you should see the graph update as child runs complete.


```python
from azureml.widgets import RunDetails
RunDetails(run).show()
```


    A Jupyter Widget


4. You can also use SDK methods to fetch all the child runs and see individual metrics that we log.


```python
children = list(run.get_children())
metricslist = {}
for r in children:
    properties = r.get_properties()
    metrics = {k: v for k, v in r.get_metrics().items() if isinstance(v, float)}
    metricslist[int(properties['iteration'])] = metrics

rundata = pd.DataFrame(metricslist).sort_index(1)
rundata
```




<div>
<table border="1" class="dataframe">
  <thead>
    <tr style="text-align: right;">
      <th></th>
      <th>0</th>
      <th>1</th>
      <th>2</th>
      <th>3</th>
      <th>4</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>AUC_macro</th>
      <td>0.92</td>
      <td>0.94</td>
      <td>0.69</td>
      <td>0.92</td>
      <td>0.92</td>
    </tr>
    <tr>
      <th>AUC_micro</th>
      <td>0.92</td>
      <td>0.94</td>
      <td>0.69</td>
      <td>0.92</td>
      <td>0.92</td>
    </tr>
    <tr>
      <th>AUC_weighted</th>
      <td>0.92</td>
      <td>0.94</td>
      <td>0.69</td>
      <td>0.92</td>
      <td>0.92</td>
    </tr>
    <tr>
      <th>accuracy</th>
      <td>0.91</td>
      <td>0.84</td>
      <td>0.58</td>
      <td>0.91</td>
      <td>0.91</td>
    </tr>
    <tr>
      <th>average_precision_score_macro</th>
      <td>0.63</td>
      <td>0.64</td>
      <td>0.23</td>
      <td>0.63</td>
      <td>0.63</td>
    </tr>
    <tr>
      <th>average_precision_score_micro</th>
      <td>0.63</td>
      <td>0.64</td>
      <td>0.23</td>
      <td>0.63</td>
      <td>0.63</td>
    </tr>
    <tr>
      <th>average_precision_score_weighted</th>
      <td>0.63</td>
      <td>0.64</td>
      <td>0.23</td>
      <td>0.63</td>
      <td>0.63</td>
    </tr>
    <tr>
      <th>balanced_accuracy</th>
      <td>0.78</td>
      <td>0.89</td>
      <td>0.67</td>
      <td>0.78</td>
      <td>0.71</td>
    </tr>
    <tr>
      <th>f1_score_macro</th>
      <td>0.77</td>
      <td>0.74</td>
      <td>0.51</td>
      <td>0.77</td>
      <td>0.74</td>
    </tr>
    <tr>
      <th>f1_score_micro</th>
      <td>0.91</td>
      <td>0.84</td>
      <td>0.58</td>
      <td>0.91</td>
      <td>0.91</td>
    </tr>
    <tr>
      <th>f1_score_weighted</th>
      <td>0.91</td>
      <td>0.87</td>
      <td>0.64</td>
      <td>0.91</td>
      <td>0.90</td>
    </tr>
    <tr>
      <th>log_loss</th>
      <td>0.53</td>
      <td>0.31</td>
      <td>0.69</td>
      <td>0.53</td>
      <td>0.22</td>
    </tr>
    <tr>
      <th>norm_macro_recall</th>
      <td>0.56</td>
      <td>0.77</td>
      <td>0.35</td>
      <td>0.56</td>
      <td>0.43</td>
    </tr>
    <tr>
      <th>precision_score_macro</th>
      <td>0.78</td>
      <td>0.70</td>
      <td>0.59</td>
      <td>0.78</td>
      <td>0.81</td>
    </tr>
    <tr>
      <th>precision_score_micro</th>
      <td>0.91</td>
      <td>0.84</td>
      <td>0.58</td>
      <td>0.91</td>
      <td>0.91</td>
    </tr>
    <tr>
      <th>precision_score_weighted</th>
      <td>0.91</td>
      <td>0.93</td>
      <td>0.86</td>
      <td>0.91</td>
      <td>0.91</td>
    </tr>
    <tr>
      <th>recall_score_macro</th>
      <td>0.78</td>
      <td>0.89</td>
      <td>0.67</td>
      <td>0.78</td>
      <td>0.71</td>
    </tr>
    <tr>
      <th>recall_score_micro</th>
      <td>0.91</td>
      <td>0.84</td>
      <td>0.58</td>
      <td>0.91</td>
      <td>0.91</td>
    </tr>
    <tr>
      <th>recall_score_weighted</th>
      <td>0.91</td>
      <td>0.84</td>
      <td>0.58</td>
      <td>0.91</td>
      <td>0.91</td>
    </tr>
    <tr>
      <th>weighted_accuracy</th>
      <td>0.94</td>
      <td>0.83</td>
      <td>0.56</td>
      <td>0.94</td>
      <td>0.96</td>
    </tr>
  </tbody>
</table>
</div>



## Retreive and Test AutoML model
[Link to Top](#Task-List)  

### Retrieving models

We can now use the **get_output** method to return the best run and the fitted model. The Model includes the pipeline and any pre-processing. Overloads on **get_output** allow you to retrieve the best run and fitted model for **any** logged metric or for a particular iteration.

1. We can now retrieve models based on any metric


```python
lookup_metric = "log_loss"
best_run, fitted_model = run.get_output(metric = lookup_metric)
print(best_run)
print(fitted_model)
```

    Run(Experiment: automated-ml-classification,
    Id: AutoML_963d3fd5-3174-4926-8c06-cff4836d5fc0_4,
    Type: None,
    Status: Completed)
    Pipeline(memory=None,
         steps=[('datatransformer', DataTransformer(enable_feature_sweeping=None, feature_sweeping_timeout=None,
            is_onnx_compatible=None, logger=None, observer=None, task=None)), ('stackensembleclassifier', StackEnsembleClassifier(base_learners=[('0', Pipeline(memory=None,
         steps=[('maxabsscaler'...7fbe1bb10da0>,
               solver='lbfgs', tol=0.0001, verbose=0),
                training_cv_folds=5))])


2. Retrieve a specific iteration


```python
iteration = 3
third_run, third_model = run.get_output(iteration = iteration)
print(third_run)
print(third_model)
```

    Run(Experiment: automated-ml-classification,
    Id: AutoML_963d3fd5-3174-4926-8c06-cff4836d5fc0_3,
    Type: None,
    Status: Completed)
    Pipeline(memory=None,
         steps=[('datatransformer', DataTransformer(enable_feature_sweeping=None, feature_sweeping_timeout=None,
            is_onnx_compatible=None, logger=None, observer=None, task=None)), ('prefittedsoftvotingclassifier', PreFittedSoftVotingClassifier(classification_labels=None,
                   estimators=[('0...          random_state=None, tol=0.01))]))],
                   flatten_transform=None, weights=[1.0]))])


3. Or we select the best pipeline from our iterations. 


```python
best_run, fitted_model = run.get_output()
print(best_run)
print(fitted_model)
```

    Run(Experiment: automated-ml-classification,
    Id: AutoML_963d3fd5-3174-4926-8c06-cff4836d5fc0_4,
    Type: None,
    Status: Completed)
    Pipeline(memory=None,
         steps=[('datatransformer', DataTransformer(enable_feature_sweeping=None, feature_sweeping_timeout=None,
            is_onnx_compatible=None, logger=None, observer=None, task=None)), ('stackensembleclassifier', StackEnsembleClassifier(base_learners=[('0', Pipeline(memory=None,
         steps=[('maxabsscaler'...7fbdbfec2208>,
               solver='lbfgs', tol=0.0001, verbose=0),
                training_cv_folds=5))])


4. We can also explore the best run further


```python
best_run
```




<table style="width:100%"><tr><th>Experiment</th><th>Id</th><th>Type</th><th>Status</th><th>Details Page</th><th>Docs Page</th></tr><tr><td>automated-ml-classification</td><td>AutoML_2b832a4d-7513-4748-8f93-ba745249b64e_4</td><td></td><td>Completed</td><td><a href="https://mlworkspace.azure.ai/portal/subscriptions/ba5f14ba-32b2-4d29-b115-919f9253bfb0/resourceGroups/crice-ML/providers/Microsoft.MachineLearningServices/workspaces/crice-mlwkspace/experiments/automated-ml-classification/runs/AutoML_2b832a4d-7513-4748-8f93-ba745249b64e_4" target="_blank" rel="noopener">Link to Azure Portal</a></td><td><a href="https://docs.microsoft.com/en-us/python/api/azureml-core/azureml.core.run.Run?view=azure-ml-py" target="_blank" rel="noopener">Link to Documentation</a></td></tr></table>



### Testing models

1. We'll test our model by using the **x_test** dataframe and receive our model's prediction and the probability of the prediction. 

    - Prediction will be a one dimensional array of either 1 (predicted to drop) or 0 (predicted to not drop)
    - Since our target is (0,1), probabliy returns a two dimensional array. The first index refers to the probability that the data belong to class 0, and the second refers to the probability that the data belong to class 1. In this scenario, we are only concerned about the second index since it correlates to the risk of dropping a course.


```python
y_predict = fitted_model.predict(x_test)
y_risk = fitted_model.predict_proba(x_test)

## Set y_risk probability of dropping the course
y_risk = y_risk[:,1]
```

2. We can now compare against random class drops from the **y_test** array to the predicted class drops from the **y_predict** and **y_risk** arrays. 


```python
y_actual = y_test.values.flatten().tolist()

import numpy as np
for i in np.random.choice(len(y_actual), 10, replace = False):

    #print(i)
    predicted = y_predict[i]
    probability = y_risk[i]
    actual = y_actual[i]
    
    output = "Actual value = {}  Predicted value = {} (Risk: {:.3f}) ".format(actual, predicted, probability)
    print(output)

```

    Actual value = 0  Predicted value = 0 (Risk: 0.026) 
    Actual value = 1  Predicted value = 1 (Risk: 0.868) 
    Actual value = 0  Predicted value = 0 (Risk: 0.026) 
    Actual value = 0  Predicted value = 0 (Risk: 0.026) 
    Actual value = 0  Predicted value = 0 (Risk: 0.087) 
    Actual value = 0  Predicted value = 0 (Risk: 0.026) 
    Actual value = 0  Predicted value = 0 (Risk: 0.026) 
    Actual value = 0  Predicted value = 0 (Risk: 0.026) 
    Actual value = 0  Predicted value = 0 (Risk: 0.026) 
    Actual value = 0  Predicted value = 0 (Risk: 0.067) 


3. We can also use a confusion matrix to compare true values versus predicted values. The number of correct ('True Positives' or 'True Negatives') and incorrect predictions ('False Positives' or 'False Negatives') are summarized with count values and broken down by each class. This is the key to the confusion matrix.

![Confusion Matrix](media/5.png)


```python
#Generate a confusion matrix to see how many samples from the test set are classified correctly. 
#Notice the misclassified value for the incorrect predictions:

from sklearn.metrics import confusion_matrix
conf_mx = confusion_matrix(y_actual, y_predict)
print(conf_mx)
print('Overall accuracy:', np.average(y_predict == y_actual))
```

    [[14580   275]
     [ 1166   811]]
    Overall accuracy: 0.9143892585551331


## Apply AutoML model to whole dataset
[Link to Top](#Task-List)  

Now that we've verified the model's accuracy, we can begin to apply it.  

For this scenario, we'll simply reset the y_predict and y_risk to the whole **studentDropDF_x** dataframe. However, you can retrive the model as a pickle (.pkl) file and deploy the model to a power cluster, containers, or other compute resource. Follow [this documentation link](https://docs.microsoft.com/en-us/azure/machine-learning/service/how-to-deploy-and-where) for more details.

1. First, we compute the prediction and probability of dropped a course against the **studentDropDF_x** dataframe.


```python
# Predict Entire Dataset

y_predict = fitted_model.predict(studentDropDF_x)
y_risk = fitted_model.predict_proba(studentDropDF_x)
y_risk = y_risk[:,1]
```

2. We can also use the probability of dropping a course to set a flag of Low, Medium, or High correlated to the probability's value.


```python

risk_list = []

for i in y_risk:
    risk = "Medium"
    if i < .25:
        risk = "Low"
    elif i >= .5:
        risk = "High"
    risk_list.append(risk)

risk_array = np.array(risk_list)


```

3. We'll now define a new dataframe to report off of, with de-identified student information


```python
ReportingDF = studentDropDF_x.filter(["StudentID","TermID","SubjectID","CatalogID","ClassID","SectionID","EnrollDate"], axis=1)
```

4. Next, append y_predict as DropoutFlag (0 or 1,) y_risk as DropoutProbability (0.0 - 1.0,) and risk_array as RiskCategory (Low, Medium, High) to the **ReportingDF**


```python
ReportingDF['DropoutFlag'] = y_predict
ReportingDF['DropoutProbability'] = y_risk
ReportingDF['RiskCategory'] = risk_array
```

5. We can now display this dataframe to get a sense of the data at a glance


```python
display(ReportingDF)
```


<div>
<table border="1" class="dataframe">
  <thead>
    <tr style="text-align: right;">
      <th></th>
      <th>TermID</th>
      <th>SubjectID</th>
      <th>CatalogID</th>
      <th>ClassID</th>
      <th>SectionID</th>
      <th>EnrollDate</th>
      <th>DropoutFlag</th>
      <th>DropoutProbability</th>
      <th>RiskCategory</th>
    </tr>
    <tr>
      <th>StudentID</th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
      <th></th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th>18023</th>
      <td>2074</td>
      <td>ACC-UB</td>
      <td>301</td>
      <td>1</td>
      <td>UB2</td>
      <td>20061127</td>
      <td>0</td>
      <td>0.05</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>5041</th>
      <td>2074</td>
      <td>ACC-UB</td>
      <td>405</td>
      <td>2</td>
      <td>101</td>
      <td>20070117</td>
      <td>0</td>
      <td>0.09</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>657</th>
      <td>2054</td>
      <td>ADLT</td>
      <td>473</td>
      <td>3</td>
      <td>1</td>
      <td>20041219</td>
      <td>0</td>
      <td>0.02</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>390</th>
      <td>2064</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20060130</td>
      <td>1</td>
      <td>0.73</td>
      <td>High</td>
    </tr>
    <tr>
      <th>700</th>
      <td>2064</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20051208</td>
      <td>0</td>
      <td>0.03</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>793</th>
      <td>2074</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20070125</td>
      <td>0</td>
      <td>0.02</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>812</th>
      <td>2074</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20070123</td>
      <td>0</td>
      <td>0.03</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>830</th>
      <td>2074</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20070209</td>
      <td>0</td>
      <td>0.03</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>947</th>
      <td>2074</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20070117</td>
      <td>0</td>
      <td>0.02</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>962</th>
      <td>2058</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20050823</td>
      <td>0</td>
      <td>0.03</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>976</th>
      <td>2058</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20050826</td>
      <td>0</td>
      <td>0.03</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>999</th>
      <td>2058</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20050722</td>
      <td>0</td>
      <td>0.02</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>1170</th>
      <td>2074</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20061201</td>
      <td>0</td>
      <td>0.02</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>1617</th>
      <td>2074</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20070112</td>
      <td>0</td>
      <td>0.02</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>1937</th>
      <td>2074</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20070116</td>
      <td>0</td>
      <td>0.02</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>2286</th>
      <td>2064</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20060123</td>
      <td>0</td>
      <td>0.04</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>2342</th>
      <td>2068</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20060414</td>
      <td>0</td>
      <td>0.03</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>2419</th>
      <td>2074</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20070119</td>
      <td>0</td>
      <td>0.02</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>2460</th>
      <td>2068</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20060821</td>
      <td>0</td>
      <td>0.02</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>2640</th>
      <td>2058</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20050628</td>
      <td>0</td>
      <td>0.02</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>2648</th>
      <td>2048</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20040826</td>
      <td>0</td>
      <td>0.02</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>2694</th>
      <td>2068</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20060818</td>
      <td>0</td>
      <td>0.03</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>3048</th>
      <td>2068</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20060424</td>
      <td>0</td>
      <td>0.02</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>3097</th>
      <td>2058</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20050825</td>
      <td>0</td>
      <td>0.02</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>3136</th>
      <td>2074</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20070124</td>
      <td>0</td>
      <td>0.03</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>3352</th>
      <td>2054</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20050203</td>
      <td>0</td>
      <td>0.03</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>3513</th>
      <td>2054</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20050126</td>
      <td>0</td>
      <td>0.03</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>3779</th>
      <td>2068</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20060810</td>
      <td>0</td>
      <td>0.03</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>3885</th>
      <td>2064</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20051130</td>
      <td>0</td>
      <td>0.02</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>3928</th>
      <td>2064</td>
      <td>ANTH</td>
      <td>207</td>
      <td>4</td>
      <td>1</td>
      <td>20060130</td>
      <td>0</td>
      <td>0.02</td>
      <td>Low</td>
    </tr>
    <tr>
      <th>...</th>
      <td>...</td>
      <td>...</td>
      <td>...</td>
      <td>...</td>
      <td>...</td>
      <td>...</td>
      <td>...</td>
      <td>...</td>
      <td>...</td>
    </tr>
  </tbody>
</table>
<p>56106 rows × 9 columns</p>
</div>


6. This dataframe can then be directly insterted into a database for reporting and analytics, or we can do further analysis with visualizations by using the matplotlib and seaborn libraries. 


```python
import seaborn as sns
import matplotlib.pyplot as plt
from scipy import stats

sns.distplot(y_risk)

```




    <matplotlib.axes._subplots.AxesSubplot at 0x7fbe16e4a438>




![png](media/output_63_1.png)



```python
sns.pairplot(ReportingDF)
```




    <seaborn.axisgrid.PairGrid at 0x7fbe1b0d88d0>




![png](media/output_64_1.png)

