# WPF-ExternalAPI
Simple program used to manage objects (CRUD) via an external API, written in WPF (.NET)

TODO:
Popup window while waiting for API operations to finish - popup window is there, but shows up only when the update task gets to start, so that's too late because most of the waiting is actually that time period before the task gets to start sending requests. So I disabled the popup, it's there in the XAML though.
Speed up operations (ExecuteAsync work really fast but sometimes fails, either use that and add a repeat mechanism, or use something else)

Quick guide:
On the left is a list of people (http://dummy.restapiexample.com/). On the right you have the modify options (written in Polish, sorry about that).

Dodaj - add new person

Usuń - delete selected person

Update - send all changes to server (I have a crappy internet connection and editing data is done through binding - OnPropertyChanged, so doing real tie updates would force sending requests to the server with each character changed). It does a comparison of a list of people that came from the API (last GET method) and the list of people in the program. Any changes are sent to the server, after that the API list is the same as the program list, so that changes aren't repeated (deleting the same employee every time the update is sent). Normally I would just sent the update and do another GET method, but the data in the dummy example is not changed (it's only for testing and I do not have my own server to do this kind of stuff).

Lista operacji do wykonania na serwerze - under that label there's a list of operations from the last update button click.
