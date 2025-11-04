extends Node

var squareSize = 80;
var spaces = {}

enum _file {
	a, b, c, d, e, f, g, h
}

func _ready() -> void:
	createSpaces()

func getScreenX(File: _file) -> int:
	var newFile = File + 1
	@warning_ignore("integer_division")
	var screenX = (newFile * squareSize) - (squareSize * 4) - (squareSize/2)
	return screenX
	
func getScreenY(Rank: int) -> int:
	var newRank = Rank + 1
	@warning_ignore("integer_division")
	var screenY = (newRank * squareSize) - (squareSize * 4) - (squareSize/2)
	return screenY
	
func getSpaceString(file: _file, rank: int):
	var fileName = _file.keys()[file]
	var rankName = str(rank)
	var spaceName = fileName + rankName
	return spaceName

func createSpaces():
	for i in range(8):
		for fileIndex in _file.values():
			spaces[getSpaceString(fileIndex,i)] = Vector2(getScreenX(fileIndex), getScreenY(i))
