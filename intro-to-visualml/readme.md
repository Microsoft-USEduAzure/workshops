# Introduction to Azure Machine Learning Workshop
## Visual ML Classification

In this workshop we will use [Azure Machine Learning Studio](https://studio.azureml.net/) to predict and flag students who are at risk of dropping a course. 

Azure Machine Learning Studio is a collaborative, drag-and-drop visual workspace where you can build, test, and deploy machine learning solutions without needing to write code.  

A new [visual interface for Azure Machine Learning](https://docs.microsoft.com/en-us/azure/machine-learning/service/ui-concept-visual-interface) is currently in preview and  supports many of the same drag-and-drop modules as Azure Machine Learning Studio.

### Prerequisites
 - Access to an Azure Subscription
 - Completion of the [Azure Fundamentals Lab](https://aka.ms/edu/Azure101)
 
 
### Syllabus
- Review question to be answered, identify features and label.

    |  |     |
    |------|------|
    |**Question**  | *Which students are at risk for dropping a course before completion?*|
    |**Data**  | Enrollment data by term by subject by class by section by student.  Historical data includes *Dropped* class indicator.|
    |**Features**  |  *Age, Class, Credit Hours, Gender, Mid Term Grade, Subject*|
    |**Label**  | *Dropped*|
    |**Confounding Variables**  | *Drop Date, End Semester Grade, Endrolled*|

- [Create a Guest Workspace (8 hour trial)](create-a-guest-workspace.md)

- [Use Azure Machine Learning Studio](visual-ml-workshop.md) to:
    - Review question to be answered
    - Identify features and label
    - Load data from Azure SQL Server
    - Examine, transform and prepare data
    - Select classification algorithm
    - Train model
    - Score model
    - Evaluate model

- Review report incorporating predictions made by model (OPTIONAL - according to time available)

### References
- [What is Azure Machine Learning Studio?](https://docs.microsoft.com/en-us/azure/machine-learning/studio/what-is-ml-studio)
- [What is Azure Machine Learning?](https://docs.microsoft.com/en-us/azure/machine-learning/service/overview-what-is-azure-ml)
- [What is the visual interface for Azure Machine Learning?](https://docs.microsoft.com/en-us/azure/machine-learning/service/ui-concept-visual-interface)
- [How does Azure Machine Learning differ from Studio?](https://docs.microsoft.com/en-us/azure/machine-learning/service/overview-what-is-azure-ml#how-does-azure-machine-learning-differ-from-studio)