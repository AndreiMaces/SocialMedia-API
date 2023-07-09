# SocialMedia-API
## Functionality
Project allows the user to create acounts, find peoples, send/accept/decline friend requests, create/update/delete posts and groups, get feed(posts) from user's friends and groups.

As security i used a basic JWT aproach, in the token i saved the id of the user that connects so that later on the request to track what user performed a action.
#### Caching and pagination
  - `getGroup` endpoint uses caching so that it will lower the requests made to the server.
  - `getFeed`, `getGroups`, `getPeoples` endpoints allows pagination to a maximum of 100 objects.

## Next step
Right now the project is still in developing, i want to add more features and improve the present code.
