
# Ultimate Basketball Sim

Ultimate Baskeball Sim puts you into the General Manager's seat and lets you build and run your own basketball team.
***
## Links
[Trello](https://trello.com/b/16tscyk6/jarae-tyler-gavin)

[Planning Document](https://docs.google.com/document/d/1MsSisWmLi0Nt0GPCoAsLpXtJMezCui2cUBTcrS2oiJ8/edit)
## Sections:
The following sections will cover functions and endpoints for all of Entities/Models that are incorporated into the game.

* [ Player](#Player)
* [Team](#Team)
* [Game](#Game)
* [League](#League)
* [Season](#Season)



***
### Player:

Endpoints:

**POST api/Player/CreatAPlayer**
***
This allows a user to create a new player. 
The required property to create a player are:
* [Key] int PlayerID
* string FullName
* enum Position
* int PlayerNumber
* double PlayerHeight
* double PlayerRating

Optional property(ies):
* [ForeignKey]TeamId

**GET api/Player/GetAllPlayers**
***
* [Key] int PlayerID
* string FullName
* enum Position
* int PlayerNumber
* double PlayerHeight
* double PlayerRating
*[ForeignKey] int TeamID

This endpoint allows a user to retrive a list of all players.


**GET api/Player/GetPlayerByID**
***
* [Key] int PlayerID
* string FullName
* enum Position
* int PlayerNumber
* double PlayerHeight
* double PlayerRating
* [ForeignKey]int TeamID

This endpoint allows a user to pull a specific player from the database.

The id can be replaced with the corresponding players PlayerID.

**PUT api/Player/EditAPlayer**
***
* [ForeignKey]int TeamID


This endpoint allows a user to change a players ForeignKey TeamID when the player is added to a team using the UpdateTeamIdForPlayer method or to Update all attributes of a player using the UpdatePlayer Method.

**DELETE api/Player/DeletePlayerById/{id}**
***
* [Key] int PlayerID

This endpoint allows a user to delete a player with the corresponding ID entered into the URL.

**Post api/Player/AssignFreeAgentsToTeam?teamId={teamId}**
***
* [Key] int PlayerID

This endpoint allows the user to run a function that will assign 5 players to a team that matches the given TeamID. This function will first check to see whether or not there are at least 5 free agents available, then assign 1 player of each position to the team.
***

### Team


**POST api/Team/CreateATeam**
***
* [Key] int TeamID
* string TeamName
* int WLRecord
* int GamesPlayed

This endpoint allows a user to create a new team with a name of their choosing. Once created 5 players may be assigned to the team.

**PUT api/Team/UpdateTeamById?teamId={teamId}**
*** 
* [Key] int TeamID
* string TeamName
* int WLRecord
* int GamesPlayed

This enpoint allows a user to update a players TeamID when they are assigned to a team. This endpoint is called upon by the AssignPlayerToTeam function.

**DELETE api/Team/DeleteTeamById?teamId={teamId}**
***
* [Key] int TeamID

This endpoint allows a user to delete a team with the corresponding TeamID.

**GET api/Team/GetTeamByTeamId?teamId={teamId}**
***
* [Key] int TeamID
* string TeamName
* int WLRecord
* int GamesPlayed

This endpoint allows a user select and view a team with the corresponding TeamID.

**GET api/Team/GetAllTeams**
***
* [Key] int TeamID
* string TeamName
* int WLRecord
* int GamesPlayed

This endpoint allows a user to get an array containing all teams that have been created.

### League

**POST api/League/CreateALeague**
***

* [Key] int LeagueID
* bool IsActive

This endpoint allows a user to create a league. A league acts as a save file as it encapsulates all of the other elements such as season, team, etc. A league must be created in order to create a season.

**GET api/League/GetListOfLeagues**
***

* [Key] int LeagueID
* bool IsActive

This endpoint allows a user to get a list of all of the leagues that have been created.

**GET api/League/GetLeagueByID?leagueId={leagueId}**
***

* [Key] int LeagueID
* bool IsActive

This endpoint allows a user to select and view a league with a corresponding LeagueID.

**PUT api/League/UpdateALeague?leaugeId={leaugeId}**
***

* bool IsActive

This endpoint will allow a user to update a league and change its status from active to inactive.

**DELETE api/League/DeleteALeague?leaugeId={leaugeId}**
***

* [Key] int LeagueID

This endpoint allows a user to delete an existing league with a corresponding LeagueID.

### Season

**POST api/Season/CreateSeason**
***
* [Key] int SeasonId
* [ForeignKey] int LeagueID
* int SeasonNumber

This endpoint allows a user to create a season which encompasses all of the games, teams, and players. **THERE MUST BE AN ACTIVE LEAGUE IN ORDER TO CREATE A SEASON.**

**GET api/Season/GetAllSeasons**
***
* [Key] int SeasonId
* [ForeignKey] int LeagueID
* int SeasonNumber

This endpoint will allow a user to view all seasons that have been created.
