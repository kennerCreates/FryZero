extends Node

var squareSize = 80;
#var spaces = {};

enum _file {
	a, b, c, d, e, f, g, h
}

#func _ready() -> void:
	#createSpaces()

func getScreenX(File: _file) -> int:
	var newFile = File + 1
	@warning_ignore("integer_division")
	var screenX = (newFile * squareSize) - (squareSize * 4) - (squareSize/2)
	return screenX
	
func getScreenY(Rank: int) -> int:
	var newRank = Rank
	@warning_ignore("integer_division")
	var screenY = (-1 * (newRank * squareSize) + (squareSize * 4) + (squareSize/2))
	return screenY	
	
func getSpaceStringFromFileRank(file: _file, rank: int):
	var fileName = _file.keys()[file]
	var rankName = str(rank)
	var spaceName = fileName + rankName
	return spaceName
	
func getFile(screenX: int) -> _file:
	@warning_ignore("integer_division")
	var fileInt = (screenX - (squareSize * 4) - (squareSize/2))/squareSize
	var fileEnum = _file.keys()[fileInt]
	return fileEnum

func getRank(screenY: int) -> int:
	@warning_ignore("integer_division")
	var rankInt = ((screenY - (squareSize * 4) - (squareSize/2)) * -1)/squareSize
	return rankInt
	
func getSpaceStringFromLocation(screenX: int, screenY: int) -> String:
	return "pass"

#func createSpaces():
	#for i in range(1,9):
		#for fileIndex in _file.values():
			#spaces[getSpaceString(fileIndex,i)] = Vector2(getScreenX(fileIndex), getScreenY(i))
