# FourInLine
The code is separated into two large blocks, the view and the logic.
In the logic there is InGame that creates a Board and the states that will manage the logic of the game. InGame passes the Board and BoardView reference to its states

The states in inGame are: 
NextTurnState: that handles who is the next player in case there are slots to fill
PlayerTurn: That tells the view that it is going to play and, upon receiving an input of the play, applies it to the board and calls the CheckGameStatusState
AIPlayerState: Ask the Board to tell you if any of the empty slots contain a play that allows you to win (online or in column). If not, place the chip in a random slot. 
(This Query can easily be used to ask the Board if the opponent has a play to win and thus, go ahead and block that slot ... this is not implemented to make the game easier)
CheckGameStatusState: Checks if the victory or draw condition occurs (calling the WInState or the DrawState if so), 
if none of these conditions are met, it invokes the nextTurnState and the cycle begins again

Board
The Board contains the grid of the board and the data of which slots are occupied by which player. 
When performing a query for victory condition or imminent victory condition (the one performed by the AIPlayerState) always use the first empty slot in each column to check horizontally, vertically and diagonally, avoiding having to sweep the entire grid.
The first empty slot in each column is saved as the grid is updated so as not to have to constantly calculate it

The View is limited to representing graphically what happens in the logic, it instantiates the buttons and communicates to InGame if any button was pressed
All this is handled in the BoardView, which in turn has its state pattern to allow animating and displaying in an orderly fashion what is happening in the logic.
A row of Buttons (one for each column) allows the user to select in which column to drop their chip
Inside the BoardView, the logicStates saves the current state of the logic while the currentState saves the state of the view.
When a state of the View finishes performing its actions, the currentView goes to the next state.
In this way, while the state of the view performs its actions, the logic actions are saved in the view, waiting to be represented visually

MatchData saves the initial settings of the game, allowing you to customize the size of the grid and the number of real players and AI. These variants are not implemented but they are contemplated within the structure of the code.

PlayerData contains the player index and if it is AI or not
