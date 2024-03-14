# CodingChallenge
To dive deeper into your skills and understand the way that how you approach a problem and come to a solution, we lined up a coding challenge for you. You are requested to send GitHub repository of your code/solution in reply of this email.
After submitting your assessment you’ll have a follow up interview to demonstrate your results.
## Requirements
Total Time: 5 Hours
Language: English
Environment: Visual studio, WPF, MVVM, Entity framework, MSSQL Server.
You should not take longer than 5 hours to complete this coding challenge.
## Background Info
Efficient coordination and monitoring of military training exercises are critical for ensuring preparedness and effectiveness in friendly countries armed forces. However, existing system have necessary features for registering participating countries, soldiers, and ranks for comprehensive training. To verify the functional operations of training we need an independent simulation module that will show real-time actions performed by the soldiers in the training.
## Problem Statement
Developing a system that provides evaluators with real-time updates on soldier movements based on sensors data. This system should dynamically update soldier positions on a map based on GPS coordinates as user move from one location to another and save them to database. Additionally, user should be able to click on markers to access supplementary details about soldiers, including their current position (latitude, longitude), rank, country, and training information. Emphasis should be placed on optimizing performance, particularly when handling a substantial volume of markers or frequent updates to marker.
## Tasks
Draw architecture and ERD diagrams of the solution.
Implement application structure and write function to show current position of soldiers on map based on sensor data. You can use any map service to show data on map and show interaction.
Write function that show movement of soldiers using marker with animation effect on movement of markers.
Write unit tests in your preferred framework for your code.
Write end-to-end integration test for the movement of soldier.
## Appendix
Completeness
Focus on the parts of your solution you feel confident in. Remember, you don’t have to deliver a finished solution, but show us how you approach a problem and how to develop a solution.
## Data Format
Soldier data can be used from a sample JSON object.

# Solution: MilitaryTrainingSimulator
The application consists of the following man parts:
* MainApplicaton
* MapView control
* ParticipantEditor control
* PersistenceService
* ParticipantService
* LocationService
![image](https://github.com/MarcellecraM/CodingChallenge/assets/163450625/39f61d2e-98bb-494f-8ee9-e37f298ba6f9)
The diagram presented illustrates the core components of the system and their interdependencies. During the initial implementation phase, our primary focus was directed towards refining the location update mechanism, given its central role in processing high volumes of data. Additional components have been included as illustrative examples, ensuring a comprehensive representation of the software's complexity.
The architectural approach adheres to the Model-View-ViewModel (MVVM) pattern, leveraging Prism.WPF for streamlined development. Dependency Injection shall be adopted to facilitate modular decoupling, facilitate integration and testing of individual components. Internally, sub-components are interconnected through interfaces, promoting flexibility for alternative implementations, emulation, and efficient unit testing.
The Adapter pattern has been employed to disentangle dependency chains, enable an infrastructure-agnostic model and core logic functionalities.
## ERD Diagram
The following Entiy Relationship Diagram shows the database tables and the reference between them. Most of the tabels are not critical concerning indexes, but the TrackedObjectSamples table will get populated with many records. Therefore the fields in this table shall be restricted to the bare minimum. Also special care need to be taken when defining the indexes om this table. Timestamp defnitifly needed becaus ethe table will searched ordered by time. It might be beneficaiial to use a composite index of participant Id and timestamp to efficiently seach for the latest update of each participant before a certan point in time. If the update reate of the recorded entities has a big range, introducing regular snapshot generation at recording time can help improving the total time needed to reconstruct the entire stat of all entities. This is not shown in the this firsst version of the ERD diagram.
![image](https://github.com/MarcellecraM/CodingChallenge/assets/163450625/58c1aa3a-9c2e-4b00-8f3e-39cf6ccf0370)
## LocationService
The Location information is fed into the components in batch mode, here a list of updated tracked objects are updated. This improves performance and also allows to control the ammount context switches needed to process the data stream. LocationService keeps the last update of all tracked objects to be able to return a snapshsot to late joiners. The update stream is fed through a BufferBlock to be able to batch process the updates. The pace at which the bulk updates are generated is defined by the component with the tightest real time requirement. That why it might be still necessary on e.g. PersistenceService to further call multipe bulk updates from LocationService and creating a bulk query to the database.
![image](https://github.com/MarcellecraM/CodingChallenge/assets/163450625/249d083c-1ded-4485-aaf3-0a96f40ca73f)
