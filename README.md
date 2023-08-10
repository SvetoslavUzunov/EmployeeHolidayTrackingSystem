# EmployeeHolidayTrackingSystem

## IMPORTANT!</br>
Admin email: admin@user.com </br>
Admin password: Pass123# </br> </br>
Supervisor email: supervisor@user.com </br>
Supervisor password: Pass123#Supervisor </br>

## App overview</br>
The platform is developed so that automatically first in it are entered one admin and one supervisor, and to each new registered user, a role Employee is given. 
A user with a role Employee has automatically attached to himself a supervisor. 
- Employee has the opportunity to make new requests to his supervisor, by choosing a start and end date of the request from a calendar.
  The request is sent to his supervisor for approval or rejection. Employee can also see how many remaining vacation days he has.
  He can see which employees are absent today (if there are any). He can also see the history of his requests, and if his request is rejected, he will see a comment from his supervisor for the reason.
  Employee can also see his data in the system, such as his username, email address, the name of his supervisor, as well as delete his profile, if he decides.
  
- After the Supervisor enters the system, he has the opportunity to see all requests from employees, and has the option to approve or reject them.
  If he decides to reject a request, he is obliged to enter a comment for the reason.
  The supervisor can also see all registered users in the system, as well as their remaining vacation days.
  
- When we enter as an Admin, we have the opportunity to see the users in the system, as well as we can delete them (Soft Delete).

## App includes:</br>
BackEnd: </br>
- Repository pattern
- Unit of work pattern
- Authentication(Register, Login, Logout)
- Handle errors functionality
- JWT Token
- Roles
- Unit Tests </br>

FrontEnd: </br>
- React Hooks
- Bootstrap
- Material UI
- Form validations
- Confirm dialogs popups
- Notifications
- Search box
