# Children Data Entry Application

This is a web application to enter and manage data for children. The application allows for data validation, and export to CSV.

## Requirements
- .NET 6.0 SDK or later
- Visual Studio 2022 or later

## Setup Instructions

1. Clone the repository:
   git clone https://github.com/shabnamtabatabaian/childrenData.git
   cd ChildrenWebApp

2. Open the solution file ChildrenWebApp.sln in Visual Studio 2022.

3. Build the project to restore the necessary packages.

4. Run the application.

5. Open your browser and navigate to https://localhost:44388 (or the URL provided by Visual Studio).


## Usage
- Click on the menu point "Children" to see the list of children
  - Click on the link "Add a new child" to add a new child
	- Enter the child's name, gender, favorite animals, and email address and then click on Creat button to save the entry.
	- Click on the link "Back to List" to back to the list of the children.
  - In the table of the children click on "Edit" link to edit the coresponding child.
	- Change any field and then click on Save button to save the changes.
	- Click on the link "Back to List" to back to the list of the children.
  - In the table of the children click on "Delete" link to delete the coresponding child.
  - Click "Export All" to download all data.
  - Click "Export Only Edited" to download only the data changed since the last export.